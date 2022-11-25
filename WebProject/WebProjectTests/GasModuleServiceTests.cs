using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.Data;
using WebProject;

namespace WebProjectTests
{
    [TestClass()]
    public class GasModuleServiceTests
    {
        // initialise ExistingGasModule propery:
        private ExistingGasModule _existingGasModule { get; set; } = null;

        [TestInitialize]
        public void Setup()
        {
            _existingGasModule = new ExistingGasModule();
        }

        [TestMethod()]
        public void GetModuleTest_CircleKGasStation_CircleKModuleIDIsReturned()
        {
            //Assign
            string gasStation = "Circle_K";

            //Act
            var result = ExistingGasModule.getModule(gasStation); // we'll need to make a wrapper in the future

            //Assert
            Assert.IsTrue(result.Equals(ExistingGasModule.CircleKModuleID));
        }

        [TestMethod()]
        public void GetModuleTest_Viada_nullIsReturned()
        {
            //Assign
            string gasStation = "Viada";

            //Act
            var result = ExistingGasModule.getModule(gasStation); // we'll need to make a wrapper in the future

            //Assert
            Assert.IsTrue(result == null);
        }

    }
}
