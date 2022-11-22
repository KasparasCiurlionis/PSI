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
    public class InsertionTests
    {
        //Method name, Scenario we're testing, expected behaviour
        [TestMethod()]
        public void PriceValidationTest_PriceMatchesRegex_PriceIsAddedToTheList()
        {
            //Arrange
            var insertion = new Insertion();
            string gasPrice = "1.999";
            List<string> gasInfo = new List<string>();

            //Act
            var result = insertion.PriceValidation(gasPrice, gasInfo);
            var last = result.Last();

            //Assert
            Assert.IsTrue(last.Equals(gasPrice));
        }

        [TestMethod()]
        public void PriceValidationTest_UserInputEmpty_DashIsAddedToTheList()
        {
            var insertion = new Insertion();
            string gasPrice = "";
            List<string> gasInfo = new List<string>();

            var result = insertion.PriceValidation(gasPrice, gasInfo);
            //var last = result.Last();

            Assert.IsNull(result);
        }

        /*[TestMethod()]
        public void PriceValidationTest_UserInputNotValid_EmptyListIsReturned()
        {
            var insertion = new Insertion();
            string gasPrice = "1000";
            List<string> gasInfo = new List<string>();

            var result = insertion.PriceValidation(gasPrice, gasInfo);

            Assert.IsNull(result);
        }*/
    }
}