using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using WebProject.Data.Repositories;

namespace WebProjectTests
{
    [TestClass()]
    public class PictureParserServiceTests
    {
        private PictureParserService _pictureParserService { get; set; } = null;
        private string str = "{\"moderated_images_count\":0,\"unmoderated_images_count\":1,\"moderated_images\":[],\"unmoderated_images\":[{\"model_id\":\"856a88e1-8fae-4bf9-be3e-cd68e90051b0\",\"day_since_epoch\":19320,\"is_moderated\":false,\"hour_of_day\":21,\"id\":\"452a5b40-6c3d-11ed-bd9c-6ed0bad48bd5\",\"url\":\"uploadedfiles/856a88e1-8fae-4bf9-be3e-cd68e90051b0/PredictionImages/f859fff3-ff39-4f17-970f-bd22e8f909e2.jpeg\",\"predicted_boxes\":[{\"id\":\"cbe0d4d7-3f9c-4173-8fa4-23ea3d8421f6\",\"label\":\"95\",\"xmin\":170,\"ymin\":187,\"xmax\":232,\"ymax\":201,\"score\":0.90808916,\"ocr_text\":\"95 1.899 mer\",\"status\":\"correctly_predicted\",\"type\":\"field\",\"page\":0,\"label_id\":\"b9415864-7a0c-44db-bd94-c2b39cc1f8b9\"},{\"id\":\"f859acd7-e57d-4c9c-880c-9b5b00ec9c06\",\"label\":\"D\",\"xmin\":171,\"ymin\":209,\"xmax\":233,\"ymax\":225,\"score\":0.89611876,\"ocr_text\":\"D 1.989\",\"status\":\"correctly_predicted\",\"type\":\"field\",\"page\":0,\"label_id\":\"ded434ee-f37e-44a2-a280-1040e18e3a95\"},{\"id\":\"b5eeccc7-6bee-4bb0-8b75-b1ac8151edd7\",\"label\":\"LPG\",\"xmin\":168,\"ymin\":234,\"xmax\":231,\"ymax\":266,\"score\":0.969199,\"ocr_text\":\"LPG 0.769\\nRM\",\"status\":\"correctly_predicted\",\"type\":\"field\",\"page\":0,\"label_id\":\"a008cc66-3987-45b8-8bd5-8d1aefbfcf38\"},{\"id\":\"f4f0d407-1538-4ffc-85d2-0cf4f820a11e\",\"label\":\"95\",\"xmin\":757,\"ymin\":504,\"xmax\":767,\"ymax\":512,\"score\":0.395042,\"ocr_text\":\"0\",\"status\":\"correctly_predicted\",\"type\":\"field\",\"page\":0,\"label_id\":\"b9415864-7a0c-44db-bd94-c2b39cc1f8b9\"}],\"moderated_boxes\":[],\"size\":{\"width\":880,\"height\":587},\"page\":0,\"request_file_id\":\"a6a43444-75e7-4056-9f55-9ab4134243ea\",\"original_file_name\":\"05.jpg\",\"custom_response\":null,\"assigned_member\":\"\",\"is_deleted\":false,\"source\":\"api\",\"no_of_fields\":3,\"cost\":0,\"payable_cost\":0,\"status\":\"success\",\"export_status\":\"\",\"retries\":0,\"rotation\":0,\"updated_at\":\"452a5b40-6c3d-11ed-bd9c-6ed0bad48bd5\",\"verified_at\":\"452a5b40-6c3d-11ed-bd9c-6ed0bad48bd5\",\"verified_by\":\"\",\"current_stage_id\":\"ffffffff-ffff-ffff-ffff-ffffffffffff\",\"uploaded_by\":\"olgierdjankovski@gmail.com\",\"upload_channel\":\"api\",\"file_url\":\"uploadedfiles/856a88e1-8fae-4bf9-be3e-cd68e90051b0/RawPredictions/05-2022-11-24T21-16-32.770.jpg\",\"request_metadata\":\"\",\"raw_ocr\":[{\"id\":\"cbe0d4d7-3f9c-4173-8fa4-23ea3d8421f6\",\"label\":\"95\",\"xmin\":170,\"ymin\":187,\"xmax\":232,\"ymax\":201,\"score\":0.90808916,\"ocr_text\":\"95 1.899 mer\",\"status\":\"correctly_predicted\",\"page\":0,\"label_id\":\"b9415864-7a0c-44db-bd94-c2b39cc1f8b9\"},{\"id\":\"f859acd7-e57d-4c9c-880c-9b5b00ec9c06\",\"label\":\"D\",\"xmin\":171,\"ymin\":209,\"xmax\":233,\"ymax\":225,\"score\":0.89611876,\"ocr_text\":\"D 1.989\",\"status\":\"correctly_predicted\",\"page\":0,\"label_id\":\"ded434ee-f37e-44a2-a280-1040e18e3a95\"},{\"id\":\"b5eeccc7-6bee-4bb0-8b75-b1ac8151edd7\",\"label\":\"LPG\",\"xmin\":168,\"ymin\":234,\"xmax\":231,\"ymax\":266,\"score\":0.969199,\"ocr_text\":\"LPG 0.769\\nRM\",\"status\":\"correctly_predicted\",\"page\":0,\"label_id\":\"a008cc66-3987-45b8-8bd5-8d1aefbfcf38\"},{\"id\":\"f4f0d407-1538-4ffc-85d2-0cf4f820a11e\",\"label\":\"95\",\"xmin\":757,\"ymin\":504,\"xmax\":767,\"ymax\":512,\"score\":0.395042,\"ocr_text\":\"0\",\"status\":\"correctly_predicted\",\"page\":0,\"label_id\":\"b9415864-7a0c-44db-bd94-c2b39cc1f8b9\"}],\"delay_post_prediction_tasks\":false,\"approval_status\":\"\",\"file_metadata\":{\"duplicate\":false}}],\"signed_urls\":{\"uploadedfiles/856a88e1-8fae-4bf9-be3e-cd68e90051b0/PredictionImages/f859fff3-ff39-4f17-970f-bd22e8f909e2.jpeg\":{\"original\":\"https://nnts.imgix.net/uploadedfiles/856a88e1-8fae-4bf9-be3e-cd68e90051b0/PredictionImages/f859fff3-ff39-4f17-970f-bd22e8f909e2.jpeg?expires=1669338995\\u0026or=0\\u0026s=58bae02fdb727e115d2e4f782caec287\",\"original_compressed\":\"https://nnts.imgix.net/uploadedfiles/856a88e1-8fae-4bf9-be3e-cd68e90051b0/PredictionImages/f859fff3-ff39-4f17-970f-bd22e8f909e2.jpeg?auto=compress\\u0026expires=1669338995\\u0026or=0\\u0026s=a94e79dc64bd081c90aa8c7a54003a98\",\"thumbnail\":\"https://nnts.imgix.net/uploadedfiles/856a88e1-8fae-4bf9-be3e-cd68e90051b0/PredictionImages/f859fff3-ff39-4f17-970f-bd22e8f909e2.jpeg?auto=compress\\u0026expires=1669338995\\u0026w=240\\u0026s=96a71362084617663264bb36a8485001\",\"acw_rotate_90\":\"https://nnts.imgix.net/uploadedfiles/856a88e1-8fae-4bf9-be3e-cd68e90051b0/PredictionImages/f859fff3-ff39-4f17-970f-bd22e8f909e2.jpeg?auto=compress\\u0026expires=1669338995\\u0026or=270\\u0026s=32addf80df884c54b130e0037d48496f\",\"acw_rotate_180\":\"https://nnts.imgix.net/uploadedfiles/856a88e1-8fae-4bf9-be3e-cd68e90051b0/PredictionImages/f859fff3-ff39-4f17-970f-bd22e8f909e2.jpeg?auto=compress\\u0026expires=1669338995\\u0026or=180\\u0026s=ad3b1e0914cab85a263620fe739e868d\",\"acw_rotate_270\":\"https://nnts.imgix.net/uploadedfiles/856a88e1-8fae-4bf9-be3e-cd68e90051b0/PredictionImages/f859fff3-ff39-4f17-970f-bd22e8f909e2.jpeg?auto=compress\\u0026expires=1669338995\\u0026or=90\\u0026s=7bb5d7e6cc0fe14d457dcfe22e9a146a\",\"original_with_long_expiry\":\"https://nnts.imgix.net/uploadedfiles/856a88e1-8fae-4bf9-be3e-cd68e90051b0/PredictionImages/f859fff3-ff39-4f17-970f-bd22e8f909e2.jpeg?expires=1684876595\\u0026or=0\\u0026s=4190f996a6e2e6789c405421d23233d8\"},\"uploadedfiles/856a88e1-8fae-4bf9-be3e-cd68e90051b0/RawPredictions/05-2022-11-24T21-16-32.770.jpg\":{\"original\":\"https://nanonets.s3.us-west-2.amazonaws.com/uploadedfiles/856a88e1-8fae-4bf9-be3e-cd68e90051b0/RawPredictions/05-2022-11-24T21-16-32.770.jpg?X-Amz-Algorithm=AWS4-HMAC-SHA256\\u0026X-Amz-Credential=AKIA5F4WPNNTLX3QHN4W%2F20221124%2Fus-west-2%2Fs3%2Faws4_request\\u0026X-Amz-Date=20221124T211635Z\\u0026X-Amz-Expires=604800\\u0026X-Amz-SignedHeaders=host\\u0026response-cache-control=no-cache\\u0026X-Amz-Signature=8fa4914c5075ed6b83d121743945814aa301f622eeffd5184e69b6e2a97f8cec\",\"original_compressed\":\"\",\"thumbnail\":\"\",\"acw_rotate_90\":\"\",\"acw_rotate_180\":\"\",\"acw_rotate_270\":\"\",\"original_with_long_expiry\":\"\"}}}";
        

        [TestInitialize]
        public void Setup()
        {
            var container = new UnityContainer();
            container.RegisterType<IPictureParserService, PictureParserService>();
            container.RegisterType<PictureParserClient>();
            _pictureParserService = container.Resolve<PictureParserService>();
        }

        [TestMethod()]
        public void Construct_GetStringThenSerializeAndValidate_RecieveDictionary()
        {
            // Assign
            Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();
            Dictionary<string, List<string>> expected = new Dictionary<string, List<string>>();

            // Act
            data = _pictureParserService.ConstructDictionaryFromJson(str);

            expected.Add("95", new List<string>() { "1.899" });
            expected.Add("D", new List<string>() { "1.989" });
            expected.Add("LPG", new List<string>() { "0.769" });

            // Assert
            // we know that Compare...
            BusinessLogicTests  b = new BusinessLogicTests();
            bool result = b.CompareDictionaries(data, expected);

            Assert.IsTrue(result);
        }

    }
}
