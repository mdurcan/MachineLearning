using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_4_5.projectCode;

namespace C_4_5Tests
{
    //
    [TestClass]
    public class C45MainTest
    {

        C45Main toTest = new C45Main();
        string file =@"C:\Users\Martin\Documents\4th_year\ML\Assignment3\testText.txt";


        // test the txt file in taking in the target information
        [TestMethod]
        public void ReadInDataTestTarget()
        {
            toTest.ReadInData(file);

            Assert.AreEqual("name", toTest.GetTargetName());
            Assert.AreEqual("p", toTest.GetPostiveResult());
            Assert.AreEqual("n", toTest.GetNegativeResult());
        }
    }
}
