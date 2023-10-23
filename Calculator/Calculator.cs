using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Calculator
    {
        public string Path;
        public string FileName;
        public string ResultFileName;
        public string Expression;
        public int Result;

        public Calculator(string path, string fileName, string resFileName, string expression)
        {
            Path = path;
            FileName = fileName;
            ResultFileName = resFileName;
            Expression = expression;
        }
        public void WriteFile()
        {
            File.WriteAllText(Path + FileName, Expression);
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine("File was successfully written");
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public void WriteResFile()
        {
            File.WriteAllText(Path + ResultFileName, Result.ToString());
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine("Result was successfully written");
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public string ReadFile()
        {
                string parsed = File.ReadAllText(Path + FileName);
                return parsed;

        }

        public int CalculateExpression(string expression)
        {
            try
            {
                string[] tokens = expression.Split(' ');
                Stack<int> operands = new Stack<int>();
                Stack<char> operators = new Stack<char>();

                int result = 0;

                foreach (string token in tokens)
                {
                    if (int.TryParse(token, out int number))
                    {
                        operands.Push(number);
                    }
                    else if (IsOperator(token[0]))
                    {
                        while (operators.Count > 0 && HasHigherPrecedence(operators.Peek(), token[0]))
                        {
                            ApplyOperator(operands, operators.Pop());
                        }
                        operators.Push(token[0]);
                    }
                }

                while (operators.Count > 0)
                {
                    ApplyOperator(operands, operators.Pop());
                }

                if (operands.Count == 1)
                {
                    result = operands.Pop();
                }
                else
                {
                    Console.WriteLine("Invalid expression");
                    result = -1;
                }
                Result = result;
                return result;
            }
            catch (System.IndexOutOfRangeException)
            {

                Console.WriteLine("Invalid expression");
                return -1;
            }
        }

        public bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }

        public int GetPrecedence(char op)
        {
            if (op == '+' || op == '-')
                return 1;
            if (op == '*' || op == '/')
                return 2;
            return 0;
        }

        public bool HasHigherPrecedence(char op1, char op2)
        {
            int precedence1 = GetPrecedence(op1);
            int precedence2 = GetPrecedence(op2);
            return precedence1 >= precedence2;
        }

        public void ApplyOperator(Stack<int> operands, char op)
        {
            if (operands.Count < 2)
            {
                Console.WriteLine("Помилка: Недостатньо операндів.");
                return;
            }

            int b = operands.Pop();
            int a = operands.Pop();

            switch (op)
            {
                case '+':
                    operands.Push(a + b);
                    break;
                case '-':
                    operands.Push(a - b);
                    break;
                case '*':
                    operands.Push(a * b);
                    break;
                case '/':
                    if (b != 0)
                    {
                        operands.Push(a / b);
                    }
                    else
                    {
                        Console.WriteLine("Помилка: Ділення на нуль.");
                        operands.Push(-1);
                    }
                    break;
            }
        }
    }
}
