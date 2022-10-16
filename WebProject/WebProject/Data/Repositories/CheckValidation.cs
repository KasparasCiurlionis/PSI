using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebProject.Data.Repositories
{
    public class CheckValidation
    {
        public static Dictionary<string, List<string>> CheckIfEmpty(Dictionary<string, List<string>> data, Dictionary<string, List<string>> originalData)
        {
            // lets check if some fields are empty in data
            string[] tempRez = new string[data.Count];
            foreach (KeyValuePair<string, List<string>> entry in data)
            {
                if (data[entry.Key].Count == 0)
                {
                    // if its empty, this may be hiding in originalData
                    foreach (KeyValuePair<string, List<string>> originalEntry in originalData)
                    {
                        foreach (string originalResultEntry in originalEntry.Value)
                        {
                            // lets analyze every single line:
                            string currentValue = originalResultEntry;
                            // check does it contain the field name
                            if (currentValue.Contains(originalEntry.Key))
                            {
                                // if it does, lets remove it
                                currentValue = currentValue.Remove(0, originalEntry.Key.Length);
                            }
                            // check is that 1.012 regex
                            var regex = new Regex(@"[0-9]\.[0-9]{3}");

                            // remove all white space characters and letters
                            currentValue = Regex.Replace(currentValue, @"[a-zA-Z]", "");
                            currentValue = Regex.Replace(currentValue, @"\s+", "");
                            // check is the length of the currentValue 3
                            if (currentValue.Length == 3)
                            {
                                // lets add a dot after the 1st number
                                currentValue = currentValue.Insert(0, ".");
                                currentValue = currentValue.Insert(0, "1");
                                // check does it match the regex
                                if (regex.IsMatch(currentValue))
                                {
                                    Match match = regex.Match(currentValue);
                                    // if it does match, lets add the result to the dictionary
                                    data[entry.Key].Add(match.Value);
                                }
                            }
                        }
                    }
                }
            }

            // check are there any regexes in data
            var regexMain = new Regex(@"[0-9]\.[0-9]{3}");

            //run the loop of original data and add every single value to the tempRez

            foreach (KeyValuePair<string, List<string>> entry in originalData)
            {
                foreach (string resultEntry in entry.Value)
                {
                    // check does the regex match
                    // run the loop, because the match can be not only one
                    foreach (Match match in regexMain.Matches(resultEntry))
                    {
                        // lets add the match to the tempRez
                        tempRez[Array.IndexOf(tempRez, null)] = match.Value;
                    }
                }
            }


            foreach (KeyValuePair<string, List<string>> entry in data)
            {
                if (entry.Value.Count != 0)
                {

                    try
                    {
                        tempRez[Array.IndexOf(tempRez, entry.Value[0])] = null;
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            foreach (KeyValuePair<string, List<string>> entry in data)
            {
                // check if the value of the entry is empty
                if (entry.Value.Count == 0)
                {
                    // if it is, we need to add the value from tempRez which is not NULL
                    string notNull = "";
                    foreach (string value in tempRez)
                    {
                        if (value != null)
                        {
                            notNull = value;
                            // remove the value from tempRez
                            tempRez[Array.IndexOf(tempRez, value)] = null;
                            break;
                        }
                    }
                    // add the value to the entry
                    data[entry.Key].Add(notNull);
                }
            }
            return data;
        }
    }
}