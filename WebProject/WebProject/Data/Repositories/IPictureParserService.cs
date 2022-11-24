using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProject.Data.Repositories
{
    public interface IPictureParserService
    {
        string RestPost(string model_id, string path);

        // this is a method that sends data into server
        // it recieves a string
        // and returns a string
        string RestGet(string model_id, string postOutput);

        // this is a method that retrieves data from server
        // it recieves a string
        // and returns a string
        Dictionary<string, List<string>> ConstructDictionaryFromJson(string output);
        // this is a method that serialises data from json object (string 1st, then serialise, then return dictionary)
        // it recieves a string
        // and returns a dictionary
    }
}