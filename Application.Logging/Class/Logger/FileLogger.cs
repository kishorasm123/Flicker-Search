using Application.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logging
{
    public class FileLogger : ILogger
    {
        StreamWriter streamWriter = null;
        public void Open()
        {
            try
            {
                string logFilePath = Path.Combine(Path.GetTempPath(), "FlickrSearch_Logs_" + DateTime.Now.ToString("yyyy_dd_MM_HH_mm_ss") + ".txt");
                streamWriter = new StreamWriter(logFilePath, true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Log(ImageSearchContext imageSearchContext)
        {
            if (streamWriter == null)
            {
                throw new NullReferenceException("Stream writer is null. So file logging cannot be processed.");
            }

            streamWriter.WriteLine(imageSearchContext.Message);
            streamWriter.Flush();
        }

        public void Close()
        {
            if (streamWriter != null)
            {
                streamWriter.Close();
                streamWriter.Dispose();
            }
        }
    }
}
