using System;

namespace work
{

    interface shape {
        double getArea();
        double getCircumference();
        bool isValid();
    }

    public class Triangle : shape
    {
        private double a, b, c;


         public Triangle(double a, double b, double c) {
            a = this.a;
            b = this.b;
            c = this.c;

        }
        public double getArea()
        {
            double p = (a + b + c) / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        public double getCircumference()
        {
            return a + b + c;
        }

        public bool isValid()
        {
            if (a <= 0 || b < 0 || c < 0)
            {
                return false;
            }
            else {
                if (a + b > c && a + c > b && b + c > a)
                {
                    return true;
                }
                else {
                    return false;
                }

            }
        }
    }

    public class Rectangle : shape {
        private double length;
        private double width;
        public Rectangle(double a, double b) {
            length = a > b ? a : b;
            width = a < b ? a : b;
        }
        public bool isValid() {
            if (length <= 0 || width <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public double getCircumference() {
            return (length + width) * 2;
        }
        public double getArea() {
            return length * width; ;
        }
    }

    public class Square : shape {
        private double edge;
        public Square(double edge)
        {
            this.edge = edge;
        }
        public bool isValid()
        {
            if (edge <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public double getCircumference()
        {
            return edge * 4;
        }
        public double getArea()
        {
            return Math.Pow(edge, 2);
        }
      


    }

    public class generateNewShape{
        
        public static Rectangle getRectangle(double x,double y) {
            Rectangle a = new Rectangle(x,y);
            return a;
        }
        public static Square getSquare(double x)
        {
            Square a = new Square(x);
            return a;
        }
        public static Triangle getTriangle(double x, double y,double z)
        {
            Triangle a = new Triangle(x,y,z);
            return a;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Square a = generateNewShape.getSquare(10);
            Console.WriteLine(a.getArea());
        }
    }
}
