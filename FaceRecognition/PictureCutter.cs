using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition
{
    /// <summary>
    /// 图片切割手，将原图片切割留下1/4
    /// </summary>
    class PictureCutter
    {
        public Bitmap Cut(Bitmap srcImage)
        {
            int w = srcImage.Width;
            int h = srcImage.Height / 4;
            int y = srcImage.Height - h; //切割的起始位置
            
            //调整图像宽度，需要宽度为4的倍数
            w = w - w % 4;
            Bitmap destBitMap = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(destBitMap))
            {
                g.Clear(Color.Transparent);
                g.DrawImage(srcImage, new Rectangle(0, 0, w, h), 0, y, w, h, GraphicsUnit.Pixel);
            }
            return destBitMap;
        }
    }
}
