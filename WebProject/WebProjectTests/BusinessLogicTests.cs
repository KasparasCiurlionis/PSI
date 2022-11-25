using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject;
using WebProject.Data.Repositories;

namespace WebProjectTests
{
    [TestClass()]
    public class BusinessLogicTests
    {
        private ResultValidation _resultValidation { get; set; } = null;
        
        [TestInitialize]
        public void Setup()
        {
            _resultValidation = new ResultValidation(); // for this we'll create a wrapper in future
        }


        // we know that: public static Dictionary<string, List<string>> ValidateResult(Dictionary<string, List<string>> data, int amountOfResults, int amountOfFields)
        // it returns: Dictionary<string, List<string>>
        
        [TestMethod()]
        public void ResultValidationTest_SimpleInput_CorrectOutput()
        {
            //Assign
            // 1 stly, lets create a dictionary with the data we want to test

            Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();
            // it should consist of this:

            data.Add("95", new List<string>() { "1.079 Futura\n95"});
            data.Add("D", new List<string>() { "$ 0.909 Futura\nD" });
            data.Add("98", new List<string>() { "PRO DIESEL" });
            data.Add("PRO_D", new List<string>() { "PRO Di D 1.124" });

            // 2ndly, lets create a dictionary with the expected output
            Dictionary<string, List<string>> expected = new Dictionary<string, List<string>>();

            expected.Add("95", new List<string>() { "1.079" });
            expected.Add("D", new List<string>() { "0.909" });
            expected.Add("98", new List<string>() { "" });
            expected.Add("PRO_D", new List<string>() { "1.124" });

            //Act
            //var result = _resultValidation.ValidateResult(data, 4, 4);
            data = ResultValidation.ValidateResult(data, 4, 4);


            //Assert

            // lets call a comparer to return true or false
            bool result = CompareDictionaries(data, expected);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void ResultValidationTest_DifficultInput_CorrectOutput()
        {
            //Assign
            Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();
            // it should consist of this:

            data.Add("95", new List<string>() { "1.269\n1.309\nFutura\n95\nFutura" });
            data.Add("D", new List<string>() { "1. 199 Futura\n-" });
            data.Add("98", new List<string>() { "PRO DIESEL", "98" });
            data.Add("PRO_D", new List<string>() { "1.2 64 PRO Di\nD" });

            // 2ndly, lets create a dictionary with the expected output
            Dictionary<string, List<string>> expected = new Dictionary<string, List<string>>();
            //"95" "1.269"
            //          "D" "1.199"
            //"98" "1.309"
            //"PRO_D""1.264"

            expected.Add("95", new List<string>() { "1.269" });
            expected.Add("D", new List<string>() { "1.199" });
            expected.Add("98", new List<string>() { "1.309" });
            expected.Add("PRO_D", new List<string>() { "1.264" });

            //Act
            //var result = _resultValidation.ValidateResult(data, 4, 4);
            data = ResultValidation.ValidateResult(data, 4, 5);


            //Assert
            bool result = CompareDictionaries(data, expected);
            Assert.IsTrue(result);

        }
        public bool CompareDictionaries(Dictionary<string, List<string>> data, Dictionary<string, List<string>> Expected)
        {

            // lets check if the dictionaries have the same amount of keys
            if (data.Keys.Count != Expected.Keys.Count)
            {
                return false;
            }

            // lets check for values
            foreach (var key in data.Keys)
            {
                // lets check if the keys are the same
                if (!Expected.ContainsKey(key))
                {
                    return false;
                }
                // lets check if the values are the same
                if (!data[key].SequenceEqual(Expected[key]))
                {
                    return false;
                }
                // lets check for amount of values
                if (data[key].Count != Expected[key].Count)
                {
                    return false;
                }
            }

            return true;
        }
    
        
    }
}
