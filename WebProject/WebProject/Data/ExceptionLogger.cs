using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace WebProject.Data
{
    public class ExceptionLogger
    {
        public static async Task log<T>(T text) {
            ProcessLogger<T> bl = new ProcessLogger<T>();
            bl.ProcessCompleted += bl_ProcessCompleted; // register with an event
            bl.StartProcess(text);
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

        public static async void bl_ProcessCompleted<T>(object sender, T e)
        {
            await logAsync<T>(e);
        }
    }

    public class ProcessLogger<T>
    {
        // declaring an event using built-in EventHandler
        public event EventHandler<T> ProcessCompleted;

        public void StartProcess(T text)
        {
                OnProcessCompleted(text);
        }

        protected virtual void OnProcessCompleted(T text)
        {
            ProcessCompleted?.Invoke(this, text);
        }
    }
}