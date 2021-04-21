using FaceRecognition.Utils;
using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaceRecognition
{
    class Context
    {
        ILog logger = LogManager.GetLogger(typeof(Context));
        //private readonly int LINE_PRINT = 2;//每10行打印一次

        //源被检索路径
        private String _sourcePath;
        //检索后存放路径
        private String _desPath;
        private PictureCutter _cutter;
        private RecongniseProxy _recongnise;
        private FaceFrom _faceForm;


        public Context(string sourcePath, string desPath, PictureCutter cutter, RecongniseProxy recongnise, FaceFrom faceForm)
        {
            _sourcePath = sourcePath;
            _desPath = desPath;
            _cutter = cutter;
            _recongnise = recongnise;
            _recongnise.InitEngines();
            _faceForm = faceForm;
        }

        public void execute()
        {
            DirectoryInfo dir = new DirectoryInfo(_sourcePath);
            LogHelper.WriteLog(typeof(Context), "Context-ThreadName: " + Thread.CurrentThread.Name);
            //StringBuilder sb = new StringBuilder();

            int i = 0, sbi = 0 ;
            foreach (FileInfo file in dir.GetFiles())
            {
                LogHelper.WriteLog(typeof(Context), ("i: " + i));
                i++;

                //只识别jpg格式
                if (file.Extension.Equals(".jpg"))
                {
                    
                    Bitmap bitmap = (Bitmap)Image.FromFile(file.FullName);
                    LogHelper.WriteLog(typeof(Context), "cut前: " + bitmap.Width);
                    Bitmap desBitmap = _cutter.Cut(bitmap);

                    LogHelper.WriteLog(typeof(Context), "人脸识别开始: " + desBitmap.Width);
                    Boolean searchedFace = _recongnise.execute(desBitmap);
                    LogHelper.WriteLog(typeof(Context), "人脸识别结束");
                    if (searchedFace)
                    {
                        //识别出了人像
                        sbi++;

                        //sb.AppendLine(file.Name);
                        //if (sbi >= LINE_PRINT)
                        //{
                        //    sbi = 0;
                        //    _form.appendText(file.Name + "\r\n");
                        //    sb.Clear();
                        //}
                        _faceForm.appendText(file.Name + "\r\n");

                        File.Copy(file.FullName, _desPath + "\\" + file.Name, true);

                    }
                }
            }
            LogHelper.WriteLog(typeof(Context), "！！！！！！！！已跑完所有图片！！！！！！！！");
            //_form.appendText(sb.ToString());
            //return sb.ToString();

        }
    }
}
