using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProject.Data.JsonEntity
{
    public class UnmoderatedImage
    {
        public string model_id { get; set; }
        public int day_since_epoch { get; set; }
        public bool is_moderated { get; set; }
        public int hour_of_day { get; set; }
        public string id { get; set; }
        public string url { get; set; }
        public List<PredictedBox> predicted_boxes { get; set; }
        public List<object> moderated_boxes { get; set; }
        public Size size { get; set; }
        public int page { get; set; }
        public string request_file_id { get; set; }
        public string original_file_name { get; set; }
        public object custom_response { get; set; }
        public string assigned_member { get; set; }
        public bool is_deleted { get; set; }
        public string source { get; set; }
        public int no_of_fields { get; set; }
        public int cost { get; set; }
        public int payable_cost { get; set; }
        public string status { get; set; }
        public string export_status { get; set; }
        public int retries { get; set; }
        public int rotation { get; set; }
        public string updated_at { get; set; }
        public string verified_at { get; set; }
        public string verified_by { get; set; }
        public string current_stage_id { get; set; }
        public string uploaded_by { get; set; }
        public string upload_channel { get; set; }
        public string file_url { get; set; }
        public string request_metadata { get; set; }
        public List<RawOcr> raw_ocr { get; set; }
        public bool delay_post_prediction_tasks { get; set; }
        public string approval_status { get; set; }
    }
}