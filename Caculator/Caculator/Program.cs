using System;

namespace Caculator
{
    class Program
    {
        static void Main(string[] args)
        {
            String num1s = Console.ReadLine();
            double num1 = checkNum(num1s);
            string func = Console.ReadLine();
            String num2s = Console.ReadLine();
            double num2 = checkNum(num2s);
            getResult(num1,num2,func);
        }

        static double checkNum(string num) {
            try
            {
                double i = Double.Parse(num);
                return i;
            }
            catch(Exception e) {
                Console.WriteLine("输入有误");
                string str = Console.ReadLine();
                return checkNum(str);
            }
        
        }
        static void getResult(double num1,double num2,string func) {

            double result = 0;
            switch (func) {
                case "+":
                    result = num1 + num2;
                    break;
                case "-":
                    result = num1 - num2;
                    break;
                case "*":
                    result = num1 * num2;
                    break;
                case "/":
                    result = num1 / num2;
                    break;
                default:
                    Console.WriteLine("输入有误");
                    return;
            }
            Console.WriteLine(result);
        
        }
    }
}
