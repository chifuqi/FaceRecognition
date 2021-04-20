using ArcSoftFace.Entity;
using ArcSoftFace.SDKModels;
using ArcSoftFace.SDKUtil;
using ArcSoftFace.Utils;
using FaceRecognition.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaceRecognition
{
    /// <summary>
    /// 识别人脸的代理类，封装了ArcFace类
    /// </summary>
    class RecongniseProxy
    {
        //引擎Handle
        private IntPtr pImageEngine = IntPtr.Zero;
        private FaceFrom _faceForm;

        public RecongniseProxy(FaceFrom form)
        {
            _faceForm = form;
        }
        /// <summary>
        /// 初始化引擎
        /// </summary>
        public void InitEngines()
        {
            //读取配置文件
            AppSettingsReader reader = new AppSettingsReader();
            string appId = (string)reader.GetValue("APP_ID", typeof(string));
            string sdkKey64 = (string)reader.GetValue("SDKKEY64", typeof(string));
            string sdkKey32 = (string)reader.GetValue("SDKKEY32", typeof(string));

            var is64CPU = Environment.Is64BitProcess;
            if (is64CPU)
            {
                if (string.IsNullOrWhiteSpace(appId) || string.IsNullOrWhiteSpace(sdkKey64))
                {
                    MessageBox.Show("请在App.config配置文件中先配置APP_ID和SDKKEY64!");
                    return;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(appId) || string.IsNullOrWhiteSpace(sdkKey32))
                {
                    MessageBox.Show("请在App.config配置文件中先配置APP_ID和SDKKEY32!");
                    return;
                }
            }

            //激活引擎    如出现错误，1.请先确认从官网下载的sdk库已放到对应的bin中，2.当前选择的CPU为x86或者x64
            int retCode = 0;

            try
            {
                retCode = ASFFunctions.ASFActivation(appId, is64CPU ? sdkKey64 : sdkKey32);
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("无法加载 DLL") > -1)
                {
                    MessageBox.Show("请将sdk相关DLL放入bin对应的x86或x64下的文件夹中!");
                }
                else
                {
                    MessageBox.Show("激活引擎失败!");
                }
                return;
            }
            LogHelper.WriteLog(typeof(RecongniseProxy), ("Activate Result:" + retCode));

            //初始化引擎
            uint detectMode = DetectionMode.ASF_DETECT_MODE_IMAGE;
            //检测脸部的角度优先值
            int detectFaceOrientPriority = ASF_OrientPriority.ASF_OP_0_HIGHER_EXT;
            //人脸在图片中所占比例，如果需要调整检测人脸尺寸请修改此值，有效数值为2-32
            int detectFaceScaleVal = 16;
            //最大需要检测的人脸个数
            int detectFaceMaxNum = 5;
            //引擎初始化时需要初始化的检测功能组合
            int combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_AGE | FaceEngineMask.ASF_GENDER | FaceEngineMask.ASF_FACE3DANGLE;
            //初始化引擎，正常值为0，其他返回值请参考http://ai.arcsoft.com.cn/bbs/forum.php?mod=viewthread&tid=19&_dsign=dbad527e
            retCode = ASFFunctions.ASFInitEngine(detectMode, detectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, ref pImageEngine);
            AppendText("InitEngine Result:" + retCode + "    ");
            AppendText((retCode == 0) ? "引擎初始化成功!\r\n" : string.Format("引擎初始化失败!错误码为:{0}\r\n", retCode));
            AppendText("--------------------------以下为识别出的图像-------------------------- \r\n" );

            //LogHelper.WriteLog(typeof(RecongniseProxy), "ThreadName: " + Thread.CurrentThread.GetHashCode() + "\r\n");
            //if (retCode != 0)
            //{
            //}


            //初始化视频模式下人脸检测引擎
            //uint detectModeVideo = DetectionMode.ASF_DETECT_MODE_VIDEO;
            //int combinedMaskVideo = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION;
            //retCode = ASFFunctions.ASFInitEngine(detectModeVideo, detectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMaskVideo, ref pVideoEngine);

            //视频专用FR引擎
            //detectFaceMaxNum = 1;
            //combinedMask = FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_FACE3DANGLE;
            //retCode = ASFFunctions.ASFInitEngine(detectMode, detectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, ref pVideoImageEngine);
            //LogHelper.WriteLog(typeof(RecongniseProxy),"InitVideoEngine Result:" + retCode);

        }


        public Boolean execute(Image srcImage)
        {
            if (pImageEngine == IntPtr.Zero)
            {
                MessageBox.Show("请先初始化引擎!\r\n");
                return false;
            }

            //调整图像宽度，需要宽度为4的倍数
            if (srcImage.Width % 4 != 0)
            {
                LogHelper.WriteLog(typeof(RecongniseProxy), "图片调整开始");
                //srcImage = ImageUtil.ScaleImage(srcImage, picImageCompare.Width, picImageCompare.Height);
                srcImage = ImageUtil.ScaleImage(srcImage, srcImage.Width - (srcImage.Width % 4), srcImage.Height);
                LogHelper.WriteLog(typeof(RecongniseProxy), "图片调整结束");
            }

            LogHelper.WriteLog(typeof(RecongniseProxy), "  ...人脸检测开始...");
            //调整图片数据，非常重要
            ImageInfo imageInfo = ImageUtil.ReadBMP(srcImage);
            //人脸检测            
            ASF_MultiFaceInfo multiFaceInfo = FaceUtil.DetectFace(pImageEngine, imageInfo);
            LogHelper.WriteLog(typeof(RecongniseProxy), "  ...人脸检测结束...");
       
            LogHelper.WriteLog(typeof(RecongniseProxy), "  ...人脸个数： " + multiFaceInfo.faceNum);

            if (multiFaceInfo.faceNum < 1)
            {
                //srcImage = ImageUtil.ScaleImage(srcImage, picImageCompare.Width, picImageCompare.Height);
                //image1Feature = IntPtr.Zero;
                //picImageCompare.Image = srcImage;
                //AppendText(string.Format("{0} - 未检测出人脸!\n\n", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")));
                //AppendText(string.Format("------------------------------检测结束，时间:{0}------------------------------\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ms")));
                //AppendText("\n");
                return false;
            }
            return true;
        }

        private void AppendText(string message)
        {
            _faceForm.appendText(message);
        }

    }
}
