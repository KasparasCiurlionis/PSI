using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebProject.Data
{
    public class ExceptionLogger
    {
        public static void log<T>(T text)
        {
            string file = System.AppDomain.CurrentDomain.BaseDirectory + "/App_Data/ExceptionLog.txt";
            if (!File.Exists(file))
            {
                File.CreateText(file);
            }
            File.AppendText(file).WriteLine(text);
        }
    }
}