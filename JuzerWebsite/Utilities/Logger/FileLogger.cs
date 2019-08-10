using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace JuzerWebsite.Utilities.Logger
{
    public class FileLogger
    {
        public void LogExceptionToFile(Exception e)
        {
            File.WriteAllLines("C://Error//" + DateTime.Now.ToString("dd-MM-yyyy mm hh ss") + ".txt",
                new string[]
                {
                    "Message:"+e.Message,
                    "Stacktrace:"+e.StackTrace
                });
        }
    }
}