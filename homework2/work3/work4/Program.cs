using System;

namespace work4
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] num = new int[100];
            for (int i = 2; i <num.Length; i++) {
                num[i] = 1;
            }
            for (int i = 2; i < num.Length; i++) {
                if (num[i] == 1) {
                    int j = i + 1;
                    while (j < num.Length) {
                        if (num[j] == 1 && j % i == 0) {
                            num[j] = 0;
                        }
                        j++;
                    }
                
                }
            
            }
            for (int i = 2; i < num.Length; i++) {
                if (num[i] == 1) {
                    Console.WriteLine(i);
                }
            
            }
        }
    }
}
