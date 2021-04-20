using System;
using System.Drawing;
using System.Drawing.Imaging;

class ImageCutterConfig
{
    /// <summary>
    /// MIME类型，如果用“image/jpeg”切割后的图片会模糊，目前还不知道为啥
    /// </summary>
    public const string mimeType = @"image/png"; //@"image/jpeg";//
}

/// <summary>
/// 图形切割配置
/// </summary>
public class GridData
{
    /// <summary>
    /// 原始图片的文件
    /// </summary>
    public string SrcFileName = @"D:\0_lugsProjects\MapCutter\SourceMap.jpg";
    /// <summary>
    /// 切割后的图片输出目录
    /// </summary>
    public string OutDirectory = @"D:\0_lugsProjects\MapCutter\Res\";
    /// <summary>
    /// 图片宽度
    /// </summary>
    public int ImageWidth = 3000;
    /// <summary>
    /// 图片高度
    /// </summary>
    public int ImageHeight = 1900;
    /// <summary>
    /// 格子宽度
    /// </summary>
    public int GridWidth = 256;
    /// <summary>
    /// 格子高度
    /// </summary>
    public int GridHeight = 256;
    /// <summary>
    /// 切片文件前缀
    /// </summary>
    public string titlePrefixName = "WorldMap_";
    /// <summary>
    /// 切片文件扩展名
    /// </summary>
    public string expandedName = @".jpg";
}

/// <summary>
/// 图片切割工具参数
/// </summary>
class ToolParam
{
    public Image srcImage;
    public GridData imageData;
    public ImageCodecInfo codecInfo;
    public EncoderParameters encoderParams;
    public int columnNum;
    public int rowNum;

    public int columnIndex;
    public int rowIndex;

    public ToolParam(Image srcImage, GridData imageData, ImageCodecInfo codecInfo, EncoderParameters encoderParams, int columnNum, int rowNum)
    {
        this.srcImage = srcImage;
        this.imageData = imageData;
        this.codecInfo = codecInfo;
        this.encoderParams = encoderParams;
        this.columnNum = columnNum;
        this.rowNum = rowNum;
        RefreshIndex(0, 0);
    }

    public void RefreshIndex(int columnIndex, int rowIndex)
    {
        this.columnIndex = columnIndex;
        this.rowIndex = rowIndex;
    }
}

class ImageCutterTool
{
    /// <summary>
    /// 切割图片并保存
    /// </summary>
    /// <param name="data"></param>
    public static void SaveSlice(ToolParam data)
    {
        int index = data.rowIndex * data.columnNum + data.columnIndex + 1;
        int w = data.imageData.GridWidth;
        int h = data.imageData.GridHeight;
        int x = data.columnIndex * data.imageData.GridWidth;
        int y = data.rowIndex * data.imageData.GridHeight;
        if ((data.columnIndex + 1) * data.imageData.GridWidth > data.srcImage.Width)
        {
            w = data.srcImage.Width - data.columnIndex * data.imageData.GridWidth;
        }
        if ((data.rowIndex + 1) * data.imageData.GridHeight > data.srcImage.Height)
        {
            h = data.srcImage.Height - data.rowIndex * data.imageData.GridHeight;
        }

        Bitmap destBitMap = new Bitmap(w, h, PixelFormat.Format24bppRgb);
        using (Graphics g = Graphics.FromImage(destBitMap))
        {
            g.Clear(Color.Transparent);
            g.DrawImage(data.srcImage, new Rectangle(0, 0, w, h), x, y, w, h, GraphicsUnit.Pixel);
            string name = data.imageData.OutDirectory + data.imageData.titlePrefixName + index.ToString("D2") + data.imageData.expandedName;
            destBitMap.Save(name, data.codecInfo, data.encoderParams);
        }
    }

    /// <summary>
    /// 获取图像编解码器
    /// </summary>
    /// <param name="mimeType">MIME类型</param>
    /// <returns></returns>
    public static ImageCodecInfo GetEncodeInfo(string mimeType)
    {
        ImageCodecInfo[] infos = ImageCodecInfo.GetImageEncoders();
        for (int i = 0, iMax = infos.Length; i < iMax; ++i)
        {
            ImageCodecInfo info = infos[i];
            if (mimeType == info.MimeType)
            {
                return info;
            }
        }
        return null;
    }

    //static void Main()
    //{
    //    GridData data = new GridData();
    //    Image srcImage = Image.FromFile(data.SrcFileName);
    //    int width = srcImage.Width;
    //    int height = srcImage.Height;
    //    //列数
    //    int columnNum = (int)Math.Ceiling(width * 1.0f / data.GridWidth);
    //    //行数
    //    int rowNum = (int)Math.Ceiling(height * 1.0f / data.GridHeight);

    //    ImageCodecInfo codecInfo = ImageCutterTool.GetEncodeInfo(ImageCutterConfig.mimeType);
    //    EncoderParameters encoderParams = new EncoderParameters(2);
    //    encoderParams.Param[0] = new EncoderParameter(Encoder.ColorDepth, 1L);
    //    encoderParams.Param[1] = new EncoderParameter(Encoder.Quality, 1L);

    //    ToolParam toolParam = new ToolParam(srcImage, data, codecInfo, encoderParams, columnNum, rowNum);
    //    for (int i = 0; i < rowNum; ++i)
    //    {
    //        for (int j = 0; j < columnNum; ++j)
    //        {
    //            toolParam.RefreshIndex(j, i);
    //            ImageCutterTool.SaveSlice(toolParam);
    //        }
    //    }

    //    MessageBox.Show("图片切割完毕");
    //}
}