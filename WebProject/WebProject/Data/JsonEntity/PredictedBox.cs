using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProject.Data.JsonEntity
{
    public class PredictedBox
    {
            public string id { get; set; }
            public string label { get; set; }
            public int xmin { get; set; }
            public int ymin { get; set; }
            public int xmax { get; set; }
            public int ymax { get; set; }
            public double score { get; set; }
            public string ocr_text { get; set; }
            public string status { get; set; }
            public string type { get; set; }
            public int page { get; set; }
            public string label_id { get; set; }
    }
}