using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompilerTests
{
    [TestClass]
    public class VariableTests
    {
        [TestMethod]
        public void TestAssign()
        {
            // Plan
            using var input = "x = 9*9;PRINT x;x = 8;y = 2;z = x*y;PRINT x; PRINT z; PRINT y; PRINT x+y+z".ToStream();
            var output = "81;8;16;2;26;";

            // Do
            var compiler = new Compiler.CompilerEnvironment();
            compiler.Compile(input);

            // Check
            Assert.AreEqual(output, compiler.Output);
        }
    }
}
