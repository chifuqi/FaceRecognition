using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition.Utils
{
	
    public class LogHelper
	{
		public static void WriteLog(Type t, Exception ex)
		{
			ILog logger = LogManager.GetLogger(t);
			logger.Error("Error", ex);
		}

		public static void WriteLog(Type t, string msg)
		{
			ILog logger = LogManager.GetLogger(t);
			logger.Info(msg);
			Console.WriteLine(msg);
		}
	}
}
