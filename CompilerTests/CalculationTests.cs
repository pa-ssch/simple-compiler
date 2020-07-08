using CompilerExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompilerTests
{
    [TestClass]
    public class CalculationTests
    {
        [TestMethod]
        public void TestSum()
        {
            // Plan
            using var input = "PRINT5+5;PRINT 10 + 11  ;".ToStream();
            var output = "10;21;";

            // Do
            var compiler = new Compiler.CompilerEnvironment();
            compiler.Compile(input);

            // Check
            Assert.AreEqual(output, compiler.Output);
        }


        [TestMethod]
        public void TestTernary()
        {
            // Plan
            using var input = "x = 5;z = x ? 10 :x;PRINT z;PRINT 1 ? 8 : 7;".ToStream();
            var output = "5;8;";

            // Do
            var compiler = new Compiler.CompilerEnvironment();
            compiler.Compile(input);

            // Check
            Assert.AreEqual(output, compiler.Output);
        }

        [TestMethod]
        public void TestTernaryPriority()
        {
            // Plan
            using var input = "x = 5;PRINT x + -4 ? 11 : 12;PRINT (x + -4) ? 11 : 12;".ToStream();
            var output = "17;11;";

            // Do
            var compiler = new Compiler.CompilerEnvironment();
            compiler.Compile(input);

            // Check
            Assert.AreEqual(output, compiler.Output);
        }
    }
}
