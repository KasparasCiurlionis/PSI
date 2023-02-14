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

            //string file = System.AppDomain.CurrentDomain.BaseDirectory + "/App_Data/ExceptionLog.txt";
            // system gets wrong path to that file, it goes to the bin
            // to the bin/debug/net6.0
            // lets create a normal path// the full path is:C:\Users\olgie\Desktop\New folder (2)\WebProject\WebApplication1\App_Data\ExceptionLog.txt
            string file = "C:\\Users\\olgie\\Desktop\\New folder (2)\\WebProject\\WebApplication1\\App_Data\\ExceptionLog.txt";
            if (!File.Exists(file))
            {
                //File.CreateText(file);
            }
            //using (StreamWriter sw = File.AppendText(file))
            {
                //sw.WriteLine(text.ToString());

            }
        } 
    }
}