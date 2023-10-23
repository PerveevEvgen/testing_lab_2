using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator;

namespace CalculatorTest
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void WriteFileTest()
        {
            string expr = "1 + 1";
            var calc = new Calculator.Calculator("C:\\Users\\jperv\\Desktop\\Testing_labs\\CalculatorTest\\", "test.txt", "result.txt", expr);
            calc.WriteFile();
            bool isWritten = File.Exists("C:\\Users\\jperv\\Desktop\\Testing_labs\\CalculatorTest\\test.txt");
            Assert.IsTrue(isWritten);
            Assert.AreEqual(expr, calc.ReadFile());

        }

        [TestMethod]
        public void WriteResFileTest()
        {
            string expr = "1 + 1";
            var calc = new Calculator.Calculator("C:\\Users\\jperv\\Desktop\\Testing_labs\\CalculatorTest\\", "test.txt", "result.txt", expr);
            calc.WriteFile();
            int res = calc.CalculateExpression(expr);
            calc.WriteResFile();
            bool isWritten = File.Exists("C:\\Users\\jperv\\Desktop\\Testing_labs\\CalculatorTest\\result.txt");
            Assert.IsTrue(isWritten);
            Assert.AreEqual(res, 2);
        }

        [TestMethod]
        public void readFileTest()
        {
            string expr = "3 - 5 + 6 / 2";
            var calc = new Calculator.Calculator("C:\\Users\\jperv\\Desktop\\Testing_labs\\CalculatorTest\\", "test.txt", "result.txt", expr);
            calc.WriteFile();
            string resultOfReading = calc.ReadFile();
            Assert.AreEqual(expr, resultOfReading);

        }

        [TestMethod]
        public void CalculateExpressionTest()
        {
            Dictionary<int, string> data = new Dictionary<int, string>() 
            {
                {0, "1 + 1" },
                {1, "4 * 8 - 10" },
                {2, "20 + 5 / 5" },
                {3, "10 / 3" },
                {4, "100 / 3" },
                {5, "100 | 3" },
                {6, "100 - " },
            };
            List<int> expected = new List<int> { 2, 22, 21, 3, 33, -1, -1};
            foreach (var item in data)
            {
                var calc = new Calculator.Calculator("C:\\Users\\jperv\\Desktop\\Testing_labs\\CalculatorTest\\", "test.txt", "result.txt", item.Value);
                calc.WriteFile();
                int result = calc.CalculateExpression(item.Value);
                calc.WriteResFile();
                Assert.AreEqual(expected[item.Key], result);  

            }
        }
        [TestMethod]
        public void IsOperatorTest()
        {
            char[] operators = new char[] { '-', '+', '/', '*' };
            string expr = "3 - 5 + 6 / 2";
            var calc = new Calculator.Calculator("C:\\Users\\jperv\\Desktop\\Testing_labs\\CalculatorTest\\", "test.txt", "result.txt", expr);
            foreach (var item in operators)
            {
                Assert.AreEqual(calc.IsOperator(item),true);
            }
            Assert.AreEqual(calc.IsOperator('(') ,false);
        }
        [TestMethod]
        public void GetPrecedenceTest()
        {
            string expr = "3 - 5 + 6 / 2";
            var calc = new Calculator.Calculator("C:\\Users\\jperv\\Desktop\\Testing_labs\\CalculatorTest\\", "test.txt", "result.txt", expr);
            char[] operators = new char[] { '-', '+', '/', '*', '{','&' };
            int[] expected = new int[] { 1, 1, 2, 2, 0, 0 };
            for (int i = 0; i < operators.Length; i++)
            {
                Assert.AreEqual(calc.GetPrecedence(operators[i]), expected[i]);
            }
        }
        [TestMethod]
        public void HasHigherPrecedenceTest()
        {
            string expr = "3 - 5 + 6 / 2";
            var calc = new Calculator.Calculator("C:\\Users\\jperv\\Desktop\\Testing_labs\\CalculatorTest\\", "test.txt", "result.txt", expr);
            Assert.IsTrue(calc.HasHigherPrecedence('*', '+'));
            Assert.IsFalse(calc.HasHigherPrecedence('-', '/'));
        }

        [TestMethod]
        public void ApplyOperatorTest() 
        {
            Stack<int> operands = new Stack<int>();
            operands.Push(3);
            operands.Push(4);
            char op = '+';
            string expr = "3 - 5 + 6 / 2";
            var calc = new Calculator.Calculator("C:\\Users\\jperv\\Desktop\\Testing_labs\\CalculatorTest\\", "test.txt", "result.txt", expr);
            calc.ApplyOperator(operands, op);
            Assert.AreEqual(operands.Peek(), 7);
        }
        [TestMethod]
        public void ApplyOperator_DivisionByZero()
        {
            Stack<int> operands = new Stack<int>();
            operands.Push(5);
            operands.Push(0);
            char op = '/';

            string expr = "3 - 5 + 6 / 2";
            var calc = new Calculator.Calculator("C:\\Users\\jperv\\Desktop\\Testing_labs\\CalculatorTest\\", "test.txt", "result.txt", expr);
            calc.ApplyOperator(operands, op);

            Assert.AreEqual(operands.Peek(), -1);
        }
    }
}