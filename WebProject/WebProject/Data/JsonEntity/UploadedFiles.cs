using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProject.Data.JsonEntity
{
    public class UploadedFiles
    {
        public string original { get; set; }
        public string original_compressed { get; set; }
        public string thumbnail { get; set; }
        public string acw_rotate_90 { get; set; }
        public string acw_rotate_180 { get; set; }
        public string acw_rotate_270 { get; set; }
        public string original_with_long_expiry { get; set; }
    }
}