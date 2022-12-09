using System.Drawing;
using PointF = System.Drawing.PointF;
namespace GeoLab_Proj
{
    class Triangle
    {
        private PointF a, b, c;
        private uint[] sides;

        public PointF A { get => a; }
        public PointF B { get => b; }
        public PointF C { get => c; }

        public Triangle(PointF a, PointF b, PointF c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public Triangle(ref uint[] sides)
        {
            this.sides = sides;
        }

        public double GetArea()
        {
            double a = Side(this.a, this.b);
            double b = Side(this.b, this.c);
            double c = Side(this.c, this.a);

            double p = (a + b + c) / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        private double Side(PointF a, PointF b)
        {
            float x = a.X - b.X;
            float y = a.Y - b.Y;
            return Math.Sqrt(x * x + y * y);
        }

        public void CalculateTriangle()
        {
            double p = (sides[0] + sides[1] + sides[2]) / 2;
            Console.WriteLine($"Area of your triangle is: {Math.Sqrt(p * (p - sides[0]) * (p - sides[1]) * (p - sides[2]))}  square cm");

            if (sides[0] == sides[1] && sides[1] == sides[2])
            {
                Console.WriteLine($"Your figure is an equilateral triangle."); // равносторонний треугольник
                return;
            }

            if (sides[0] == sides[1] || sides[1] == sides[2] || sides[0] == sides[2])
            {
                Console.WriteLine($"Your figure is an isosceles triangle."); // равнобедренный треугольник
                return;
            }

            int biggestValIndex = Array.IndexOf<uint>(sides, sides.Max());
            if (Math.Pow(sides[biggestValIndex], 2) == ((Math.Pow(sides[0], 2)) + Math.Pow(sides[1], 2) + Math.Pow(sides[2], 2) - Math.Pow(sides[biggestValIndex], 2)))
                Console.WriteLine($"Your figure is an right triangle."); // прямоугольный треугольник    
        }

    }
}
