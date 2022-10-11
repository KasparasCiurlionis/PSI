using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WebProject.Data;
using RestSharp;

namespace WebProject.Business_logic
{
    public class AutoInsert
    {
        public static string RestPost(string model_id, string path)
        {
            var client = new RestClient("https://app.nanonets.com/api/v2/OCR/Model/" + model_id + "/LabelFile/");
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("authorization", "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes("7Nq5U2-_Rior7MudPd0L8Dbwzwm8LPKx:")));
            request.AddHeader("accept", "Multipart/form-data");
            request.AddFile("file", path);
            RestResponse response = client.Execute(request);

            return response.Content;
        }

        public static string RestGet(string model_id, string postOutput)
        {
            string request_file_id = "";
            List<string> ssplit = new List<string>(
                postOutput.Split(new string[] { "request_file_id" }, StringSplitOptions.None));
            List<string> ssplit2 = new List<string>(
                ssplit[1].Split(new string[] { "\"" }, StringSplitOptions.None));
            request_file_id = ssplit2[2];
            var client2 = new RestClient("https://app.nanonets.com/api/v2/Inferences/Model/" + model_id + "/InferenceRequestFiles/GetPredictions/" + request_file_id);
            var request2 = new RestRequest();
            request2.Method = Method.Get;
            request2.AddHeader("authorization", "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes("7Nq5U2-_Rior7MudPd0L8Dbwzwm8LPKx:")));
            RestResponse response2 = client2.Execute(request2);
            string output = response2.Content;

            return output;
        }

        public static Dictionary<string, List<string>> returnValues(string output)
        {
            var json = JsonConvert.DeserializeObject(output);

            JSONconstructor.Root root = JsonConvert.DeserializeObject<JSONconstructor.Root>(output);
            // print unmoderated images
            // make the string result array, that will consist of the results of each image
            int amountOfFields = root.unmoderated_images[0].no_of_fields;

            int amountOfResults = 0;
            // we need to browse out json response object
            // and count the amount of labels
            try
            {
                while (root.unmoderated_images[0].predicted_boxes[amountOfResults].label != null)
                {
                    amountOfResults++;
                }
            }
            catch (System.ArgumentOutOfRangeException)
            {
                Console.WriteLine("Exception caught");
            }

            // lets create a list, this list should have an array of labels
            // also each label should have an array of results
            // lets do the same but with a dictionary
            Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();

            // lets fill the dictionary
            for (int i = 0; i < amountOfResults; i++)
            {
                // lets check if the dictionary already has the label
                if (data.ContainsKey(root.unmoderated_images[0].predicted_boxes[i].label))
                {
                    // if it does, lets add the result to the list
                    data[root.unmoderated_images[0].predicted_boxes[i].label].Add(root.unmoderated_images[0].predicted_boxes[i].ocr_text);
                }
                else
                {
                    // if it doesnt, lets create a new list and add the result to it
                    data.Add(root.unmoderated_images[0].predicted_boxes[i].label, new List<string>());
                    data[root.unmoderated_images[0].predicted_boxes[i].label].Add(root.unmoderated_images[0].predicted_boxes[i].ocr_text);
                }
            }


            // the result is printed, but not validated
            // lets validate the result
            data = ResultValidation.ValidateResult(data, amountOfResults, amountOfFields);
            return data;
        }
    }
}