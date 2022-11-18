using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace WebProject.Data
{
    public class UploadEventArgs : EventArgs
    {
        public string FileName { get; set; }
        public string Path { get; set; }
    }
    
    public class UploadHandling
    {
        // 1. Define the Delegate
        // 2 . Define the Event based on that delegate
        // 3. Raise the event

        public delegate void FileUploadedEventHandler(object source, UploadEventArgs args);

        public event FileUploadedEventHandler FileUploaded;
        
        public void Upload(string fileName, string path, string GasStationName, HttpPostedFile postedFile) // turetu registruoti?
        {
            // do the upload
            Debug.WriteLine("File uploaded: " + fileName + ", path: " + path);
            if (fileName == null)
            {
                Debug.WriteLine("File name is Empty!");
            }
            else
            {
                // try to save the postedFile
                try
                {
                    postedFile.SaveAs(path);
                    Debug.WriteLine("File saved!");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("File not saved: " + ex.Message);
                }
            }


            // raise the event
            OnFileUploaded(fileName);
        }

        public void OnFileUploaded(string fileName)
        {
            if (FileUploaded != null)
            {
                FileUploaded(this, new UploadEventArgs() { FileName = fileName });
            }
        }
    }
}