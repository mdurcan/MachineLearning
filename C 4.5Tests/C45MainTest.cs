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
        public void ReadInDataTestTargetPass()
        {
            toTest.ReadInData(file);

            Assert.AreEqual("name", toTest.GetTargetName());
            Assert.AreEqual("yes", toTest.GetPostiveResult());
            Assert.AreEqual("no", toTest.GetNegativeResult());
        }

        // test the txt attributes
        [TestMethod]
        public void ReadInDataTestAttributesPass()
        {
            toTest.ReadInData(file);

            List<IAttributes> attributes = toTest.GetAttributes();

            Assert.AreEqual("time", attributes[0].GetName());
            Assert.AreEqual("discrete", attributes[0].GetAttributeType());
            Assert.AreEqual("morning",attributes[0].GetAttributeValues()[0]);
            Assert.AreEqual("late", attributes[0].GetAttributeValues()[1]);
            Assert.AreEqual("value", attributes[1].GetName());
            Assert.AreEqual("continuous", attributes[1].GetAttributeType());
        }

        // test the print tree
        [TestMethod]
        public void TestPrintTree()
        {
            toTest.ReadInData(file);
            
            toTest.CreateTree();
            toTest.PrintTree();

        }
    }
}
