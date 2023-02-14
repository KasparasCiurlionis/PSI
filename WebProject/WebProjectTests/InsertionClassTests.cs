using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebProjectUnit.Tests
{
    [TestClass()]
    public class InsertionClassTests
    {
        // private Insertion _insertionClass { get; set; } = null!; // = null! is a C# 8.0 feature that allows us to initialize the property to null, but then we can be sure that it will be initialized before it is used.
        private Insertion _insertionClass { get; set; } = null;

        [TestInitialize]
        public void Setup()
        {
            _insertionClass = new Insertion();
        }



        [TestMethod()]
        public void PriceValidationTest_PriceMatchesRegex_PriceIsAddedToTheList()
        {
            //Assign
            string gasPrice = "1.999";
            List<string> gasInfo = new List<string>();
            
            //Act
            var result = _insertionClass.PriceValidation(gasPrice, gasInfo);
            var last = result.Last();

            //Assert
            Assert.IsTrue(last.Equals(gasPrice));
        }

        [TestMethod()]
        public void PriceValidationTest_UserInputEmpty_DashIsAddedToTheList()
        {
            //Assign
            string gasPrice = "";
            List<string> gasInfo = new List<string>();

            //Act
            var result = _insertionClass.PriceValidation(gasPrice, gasInfo);
            var last = result.Last();

            //Assert
            Assert.IsTrue(last.Equals("-"));
        }

        [TestMethod()]
        public void PriceValidationTest_UserInputNotValid_EmptyListIsReturned()
        {
            //Assign
            string gasPrice = "1234";
            List<string> gasInfo = new List<string>();

            // we know that if we will pass gasPrice string we will get an empty list

            //Act
            var result = _insertionClass.PriceValidation(gasPrice, gasInfo);

            //Assert
            // check is null
            Assert.IsTrue(result == null);
        }


        // lets make some tests for Dictionaries
        // Dictionary<string, List<string>> data
        [TestMethod()]
        public void ReturnDataKeyTest_EmptyInput_EmptyStringIsReturned()
        {
            //Assign
            Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();

            //Act
            var result = _insertionClass.returnDataKey(data, 0);

            //Assert
            Assert.IsTrue(result.Equals(""));
        }

        [TestMethod()]
        public void ReturnDataKeyTest_InputWithOneElement_ElementIsReturned()
        {
            //Assign
            Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();
            data.Add("key", new List<string>());

            //Act
            var result = _insertionClass.returnDataKey(data, 0);

            //Assert
            Assert.IsTrue(result.Equals("key"));
        }

        [TestMethod()]
        public void ReturnDataKeyTest_InputWithManyElements_ElementIsReturned()
        {
            //Assign
            Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();
            // lets add data to dictionary, lets add values as well
            data.Add("key1", new List<string>());
            data.Add("key2", new List<string>());
            data.Add("key3", new List<string>());
            // now lets add not keys, but values
            data["key1"].Add("value1");
            data["key3"].Add("value3");

            //Act
            var result = _insertionClass.returnDataKey(data, 1);

            //Assert
            Assert.IsTrue(result.Equals("key2"));

        }


        // now with returnDataValue
        [TestMethod()]
        public void ReturnDataValueTest_EmptyInput_EmptyStringIsReturned()
        {
            //Assign
            Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();

            //Act
            var result = _insertionClass.returnDataValue(data, 0);

            //Assert
            Assert.IsTrue(result.Equals(""));
        }

        [TestMethod()]
        public void ReturnDataValueTest_InputWithOneElement_ElementIsReturned()
        {
            //Assign
            Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();
            data.Add("key", new List<string>());
            data["key"].Add("value");

            //Act
            var result = _insertionClass.returnDataValue(data, 0);

            //Assert
            Assert.IsTrue(result.Equals("value"));
        }

        [TestMethod()]
        public void ReturnDataValueTest_InputWithManyElements_ElementIsReturned()
        {
            //Assign
            Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();
            // lets add data to dictionary, lets add values as well
            data.Add("key1", new List<string>());
            data.Add("key2", new List<string>());
            data.Add("key3", new List<string>());
            // now lets add not keys, but values
            data["key1"].Add("value1");
            data["key3"].Add("value3");

            //Act
            var result = _insertionClass.returnDataValue(data, 2);

            //Assert
            Assert.IsTrue(result.Equals("value3"));
        }

        [TestMethod()]
        public void CalculateDictionaryKeyLength_EmptyInput_ZeroIsReturned()
        {
            //Assign
            Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();

            //Act
            var result = _insertionClass.CalculateDictionaryKeyLength(data);

            //Assert
            Assert.IsTrue(result == 0);
        }

        [TestMethod()]
        public void CalculateDictionaryKeyLength_InputWithManyElements_IntegerIsReturned()
        {
            Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();

            // lets add data to dictionary, lets add values as well
            data.Add("key1", new List<string>());
            data.Add("key2", new List<string>());
            data.Add("key3", new List<string>());
            data["key1"].Add("value1");
            data["key3"].Add("value3");
            data["key3"].Add("value4");

            //Act
            var result = _insertionClass.CalculateDictionaryKeyLength(data);

            //Assert
            Assert.IsTrue(result == 3);
        }
    }
}