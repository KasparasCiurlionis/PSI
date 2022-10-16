using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProject.Data.JsonEntity
{
    public class Root
    {
            public int moderated_images_count { get; set; }
            public int unmoderated_images_count { get; set; }
            public List<object> moderated_images { get; set; }
            public List<UnmoderatedImage> unmoderated_images { get; set; }
            public SignedUrls signed_urls { get; set; }
        
    }
}