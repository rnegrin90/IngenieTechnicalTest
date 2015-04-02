using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IngenieProject;

namespace IngenieProjectTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestOutput()
        {
            Control c = new Control(7, 15);
            Spider s1 = new Spider(2, 4, "Left", c);

            String output = s1.Move("FLFLFRFFLF");

            Assert.AreEqual("3 1 Right\n", output);
        }

        [TestMethod]
        public void TestMovement()
        {
            Control c = new Control(7, 15);
            Spider s1 = new Spider(2,4,"Left", c);
            Spider s2 = new Spider(3, 1, "Right", c);

            s1.Move("FLFLFRFFLF");

            Assert.AreEqual(s2, s1);
        }

        [TestMethod]
        public void TestInput()
        {
            String input = "7 15\n2 4 Left\nFLFLFRFFLF\n";
            Control c = new Control();
            String expected = "3 1 Right\n";

            c.ParseInput(input);
            String result = c.GetOutput();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestFormatException()
        {
            String input = "7 15\n2 left\nFLFLFRFFLF\n";
            Control c = new Control();

            c.ParseInput(input);
        }
    }
}
