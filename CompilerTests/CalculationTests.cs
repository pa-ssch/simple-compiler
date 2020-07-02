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
    }
}
