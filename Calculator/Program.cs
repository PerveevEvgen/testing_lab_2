namespace Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Calculator calc = new Calculator(".\\", "test.txt", "result.txt", "100 - 20");
            calc.WriteFile();
            string resultOfReading = calc.ReadFile();
            Console.WriteLine(resultOfReading);
            int result = calc.CalculateExpression(resultOfReading);
            calc.WriteResFile();
            Console.WriteLine(result);
        }
    }
}