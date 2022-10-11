using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProject.Data
{
    public class JSONconstructor
    {
        public class Rootobject
        {
            public Root[] Property1 { get; set; }
        }

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
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

        public class RawOcr
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
            public int page { get; set; }
            public string label_id { get; set; }
        }

        public class Root
        {
            public int moderated_images_count { get; set; }
            public int unmoderated_images_count { get; set; }
            public List<object> moderated_images { get; set; }
            public List<UnmoderatedImage> unmoderated_images { get; set; }
            public SignedUrls signed_urls { get; set; }
        }

        public class SignedUrls
        {
            [JsonProperty("uploadedfiles/856a88e1-8fae-4bf9-be3e-cd68e90051b0/PredictionImages/3edf0150-16d0-435f-b9ee-1b6062e31422.jpeg")]
            public Uploadedfiles856a88e18fae4bf9Be3eCd68e90051b0PredictionImages3edf015016d0435fB9ee1b6062e31422Jpeg Uploadedfiles856a88e18fae4bf9Be3eCd68e90051b0PredictionImages3edf015016d0435fB9ee1b6062e31422Jpeg { get; set; }

            [JsonProperty("uploadedfiles/856a88e1-8fae-4bf9-be3e-cd68e90051b0/RawPredictions/Test7-2022-10-04T09-46-56.676.jpg")]
            public Uploadedfiles856a88e18fae4bf9Be3eCd68e90051b0RawPredictionsTest720221004T094656676Jpg Uploadedfiles856a88e18fae4bf9Be3eCd68e90051b0RawPredictionsTest720221004T094656676Jpg { get; set; }
        }

        public class Size
        {
            public int width { get; set; }
            public int height { get; set; }
        }

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

        public class Uploadedfiles856a88e18fae4bf9Be3eCd68e90051b0PredictionImages3edf015016d0435fB9ee1b6062e31422Jpeg
        {
            public string original { get; set; }
            public string original_compressed { get; set; }
            public string thumbnail { get; set; }
            public string acw_rotate_90 { get; set; }
            public string acw_rotate_180 { get; set; }
            public string acw_rotate_270 { get; set; }
            public string original_with_long_expiry { get; set; }
        }

        public class Uploadedfiles856a88e18fae4bf9Be3eCd68e90051b0RawPredictionsTest720221004T094656676Jpg
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
}