using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebProject.Business_logic
{
    public class ResultValidation
    {
        public static Dictionary<string, List<string>> ValidateResult(Dictionary<string, List<string>> data, int amountOfResults, int amountOfFields)
        {

            // create a regex, that will get the value, for example: 0.112 , 1.985
            var regex = new Regex(@"[0-9]\.[0-9]{3}");
            int fieldIndex = 0;
            int resultIndex = 0;
            // create an empty Dictionary to store the results
            Dictionary<string, List<string>> validatedData = new Dictionary<string, List<string>>();
            foreach (KeyValuePair<string, List<string>> entry in data)
            {
                // now lets add labels ONLY
                validatedData.Add(entry.Key, new List<string>());

                foreach (string resultEntry in entry.Value)
                {
                    // check does the regex match
                    if (regex.IsMatch(resultEntry))
                    {
                        Match match = regex.Match(resultEntry);
                        // if it does match, lets add the result to the dictionary
                        validatedData[entry.Key].Add(match.Value);
                    }
                    else
                    {
                        // check if it does not contain a dot
                        if (!resultEntry.Contains("."))
                        {
                            // it does not contain a dot, it means it is missing in the string
                            string value = resultEntry;
                            string fieldName = data.ElementAt(fieldIndex).Key;
                            // check does value starts with the field name
                            if (value.StartsWith(fieldName))
                            {
                                // if it does, remove it
                                value = value.Remove(0, fieldName.Length);
                            }
                            value = Regex.Replace(value, @"[a-zA-Z]", "");
                            value = Regex.Replace(value, @"\s+", "");
                            // lets check does it consist of 4 numbers!
                            if (value.Length == 4)
                            {
                                // lets add a dot after the 1st number
                                value = value.Insert(1, ".");
                                if (regex.IsMatch(value))
                                {
                                    Match match = regex.Match(value);
                                    // if it does match, lets add the result to the dictionary
                                    validatedData[entry.Key].Add(match.Value);
                                }
                            }
                            goto JumpToNextField;
                        }
                        // if it doesnt match, lets try out another regex
                        var regex2 = new Regex(@"[0-9]\.");
                        if (regex2.IsMatch(resultEntry))
                        {
                            // this means our result is not full, is seperated with some characters
                            string value = resultEntry;
                            value = Regex.Replace(value, @"[a-zA-Z]", "");
                            // get the field name
                            string fieldName = data.ElementAt(fieldIndex).Key;
                            // check does value starts with the field name
                            if (value.StartsWith(fieldName))
                            {
                                // if it does, remove it
                                value = value.Remove(0, fieldName.Length);
                            }
                            // remove all white space characters
                            value = Regex.Replace(value, @"\s+", "");
                            // check the regex
                            if (regex.IsMatch(value))
                            {
                                Match match = regex.Match(value);
                                // if it does match, lets add the result to the dictionary
                                validatedData[entry.Key].Add(match.Value);
                            }
                            goto JumpToNextField;
                        }
                    }
                JumpToNextField:
                    resultIndex++;
                }
                fieldIndex++;
            }


            // check if some fields are empty, creating a function...
            validatedData = CheckValidation.CheckIfEmpty(validatedData, data); // if its empty, this function may fix it
            return validatedData;
            // we are getting CheckIfEmpty error , because we
            // cannot see the function, its located in file: CheckValidation.cs
            // to make it visible 
        }

    }
}