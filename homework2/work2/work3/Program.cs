using System;

namespace work3
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }
        static void getResult(int []num,out int max,out int min,out int sum,out double average) {
            max=num[0];
            min = num[0];
            sum=0;
            average=0;
            for (int i = 0; i < num.Length; i++) {
                sum += num[i];
                max = num[i] > max ? num[i] : max;
                min = num[i] < min ? num[i] : min;
            }
            average = sum / num.Length;
            Console.WriteLine("最大值" + max + "最小值" + min + "所有元素的和" + sum + "平均值" + average);
        }
        static void getArray(ref int[] num) {
            try
            {
                string N = Console.ReadLine();
                int n = int.Parse(N);
                num = new int[n];
                for (int i = 0; i < n; i++) {
                    num[i]=int.Parse(Console.ReadLine());
                }
            }
            catch (Exception e) {
                Console.WriteLine("输入有误");
                return;
            }
        
        }
    }
}
