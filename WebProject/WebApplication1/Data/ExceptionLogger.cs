using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication1.Data
{
    public class ExceptionLogger
    {
        public static async Task log<T>(T text) {
            await logAsync<T>(text);
        }
        static Task logAsync<T>(T text)
        {
            return Task.Run(() => dologging<T>(text));
        }

        static void dologging<T>(T text)
        {
            
            string file = System.AppDomain.CurrentDomain.BaseDirectory + "/App_Data/ExceptionLog.txt";
            if (!File.Exists(file))
            {
                File.CreateText(file);
            }
            using (StreamWriter sw = File.AppendText(file))
            {
                sw.WriteLine(text.ToString());

            }
        } 
    }
}