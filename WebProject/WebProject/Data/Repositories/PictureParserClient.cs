using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace WebProject.Data.Repositories
{
    public class PictureParserClient
    {
        private IPictureParserService _parserService = null;

        [InjectionConstructor]
        public PictureParserClient(IPictureParserService parserService)
        {
            _parserService = parserService;
        }

        public string Post(string model_id, string path)
        {
            return _parserService.RestPost(model_id, path);
        }

        public string Get(string model_id, string postOutput)
        {
            return _parserService.RestGet(model_id, postOutput);
        }

        public Dictionary<string, List<string>> Construct(string output)
        {
            return _parserService.ConstructDictionaryFromJson(output);
        }
    }
}