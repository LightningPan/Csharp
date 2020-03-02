using System;

namespace work2
{
    class Program
    {
        static void Main(string[] args)
        {
            string userData;
            userData = Console.ReadLine();
            int userNum = checkNum(userData);
            Analyse(userNum);
            
        }
        static int checkNum(string num)
        {
            try
            {
                int i = int.Parse(num);
                if (i<=0) {
                    Console.WriteLine("输入有误,请重新输入");
                    string str = Console.ReadLine();
                    return checkNum(str);
                }
                return i;
            }
            catch (Exception e)
            {
                Console.WriteLine("输入有误,请重新输入");
                string str = Console.ReadLine();
                return checkNum(str);
            }

        }

        static void Analyse(int num) {

            for (int i = 2; i <= num;) {
                if (num % i == 0)
                {
                    Console.Write(i + " ");
                    num /= i;
                }
                else {
                    i++;
                }
                
            }
        
        
        }
       
    }

    
}
