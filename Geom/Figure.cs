using Microsoft.Graphics.Canvas.Effects;

namespace GeoLab_Proj.Geom
{
    public enum AngleType
    {
        Неіснує,
        Прямокутний,
        Гострокутний,
        Тупокутний
    }
    public enum TriangleSideTypeEnum
    {
        Неіснує,
        Рівносторонній,
        Рівнобедренний,
        Довільний
    }
    // Типи чотирикутників
    public enum QudrangleTypeEnum
    {
        Квадрат,
        Прямокутник,
        Ромб,
        Паралелограм,
        Трапеція,
        Довільний
    }
    // Типи трапецій
    public enum TrapezoidTypeEnum
    {
        Рівнобедренна,
        Прямокутна,
        Довільна
    }

    public enum FigureTypeEnum
    {
        Error,
        Пряма,
        Трикутник,
        Чотирикутник,
        Многокутник
    }

    public static class Figure
    {
        public static PointCollection Points = new();
        public static double Area()
        {
            double area = 0.0;

            for (int i = 0; i < Points.Count; ++i)
            {
                int j = (i + 1) % Points.Count;
                area += 0.5 * (Points[i].X * Points[j].Y - Points[j].X * Points[i].Y);
            }

            return Math.Abs(area);
        }
        public static double Perimeter()
        {
            var P = 0.0;
            for (int i = 0; i < Points.Count - 1; i++)
                P += Side(Points[i], Points[i + 1]);

            return P += Side(Points[0], Points[Points.Count - 1]);
        }
        public static double Side(Point a, Point b)
        {
            double x = a.X - b.X;
            double y = a.Y - b.Y;
            return Math.Sqrt(x * x + y * y);
        }
        //Перевірка на паралельність
        private static bool Parallel(Point a, Point b)
        {
            double vectorMult = a.X*b.X + a.Y*b.Y;

            double m1 = Math.Sqrt(a.X * a.X + a.Y * a.Y), m2 = Math.Sqrt(b.X * b.X + b.Y * b.Y);

            if (Math.Abs(vectorMult) == m1 * m2)
                return true;
            else return false;
        }


        /// <returns>returns angle for point b</returns>
        public static double Angle(Point a, Point b, Point c)
        {
            double x = (b.X - a.X) * (c.X - b.X);
            double y = (b.Y - a.Y) * (c.Y - b.Y);
            double length = Side(b, a) * Side(c, b);

            return Math.Acos((x + y) / length);
        }

        public static List<double> AllSides() 
        {
            List<double> sides = new();

            for (int i = 0; i < Points.Count - 1; i++)
                sides.Add(Side(Points[i], Points[i + 1]));

            sides.Add(Side(Points[Points.Count-1], Points[0]));

            return sides;
        }

        public static List<double> AllAngels()
        {
            List<double> angles = new();

            angles.Add(Angle(Points[Points.Count - 1], Points[0], Points[1]));

            for (int i = 1; i < Points.Count - 1; i++)
                angles.Add(Angle(Points[i-1], Points[i], Points[i+1]));

            angles.Add(Angle(Points[Points.Count - 2], Points[Points.Count - 1], Points[0]));

            return angles;
        }

        public static FigureTypeEnum FigureType()
        {
            switch (Points.Count)
            {
                case 2:
                    return FigureTypeEnum.Пряма;
                case 3:
                    return FigureTypeEnum.Трикутник;
                case 4:
                    return FigureTypeEnum.Чотирикутник;
                default:
                    return FigureTypeEnum.Многокутник;
            }
        }

        #region Triangle
        public static AngleType TriangleAngleType()
        {
            double angleA, angleB, angleC;
            angleA = Angle(Points[2], Points[0], Points[1]);
            angleB = Angle(Points[0], Points[1], Points[2]);
            angleC = Angle(Points[0], Points[2], Points[1]);

            if (angleA == Math.PI / 2 || angleB == Math.PI / 2 || angleC == Math.PI / 2)
                return AngleType.Прямокутний;
            if (angleA > Math.PI / 2 || angleB > Math.PI / 2 || angleC > Math.PI / 2)
                return AngleType.Тупокутний;

            return AngleType.Гострокутний;
        }
        public static TriangleSideTypeEnum TriangleSideType()
        {
            double sideA, sideB, sideC;
            sideA = Side(Points[0], Points[1]);
            sideB = Side(Points[0], Points[2]);
            sideC = Side(Points[1], Points[2]);

            if (sideA == sideB && sideB == sideC)
                return Geom.TriangleSideTypeEnum.Рівносторонній;

            if (sideA == sideB || sideB == sideC || sideA == sideC)
                return Geom.TriangleSideTypeEnum.Рівнобедренний;

            return TriangleSideTypeEnum.Довільний;
        }
        public static (bool isTriangle, AngleType angleType, TriangleSideTypeEnum sideType) TriangleType()
        {
            if (Points.Count != 3)
                return (false, 0, 0);

            return (true, TriangleAngleType(), TriangleSideType());
        }

        public static (double ab, double bc, double ac) Get3Sides() =>
            (Side(Points[0], Points[1]), Side(Points[1], Points[2]), Side(Points[0], Points[2]));

        public static (double a, double b, double c) FindNorms()
        {
            var sides = Get3Sides();
            double area = Area();
            double normA, normB, normC;
            normA = 2 * area / sides.ab;
            normB = 2 * area / sides.bc;
            normC = 2 * area / sides.ac;
            return (normA, normB, normC);
        }
        public static (double a, double b, double c) FindMedians()
        {
            double medianA, medianB, medianC;
            Point d1 = new Point();
            Point d2 = new Point();
            Point d3 = new Point();

            d1.X = (Points[1].X + Points[2].X)/2;
            d1.Y = (Points[1].Y + Points[2].Y)/2;

            d2.X = (Points[0].X + Points[2].X) / 2;
            d2.Y = (Points[0].Y + Points[2].Y) / 2;

            d3.X = (Points[0].X + Points[1].X) / 2;
            d3.Y = (Points[0].Y + Points[1].Y) / 2;

            medianA = Side(Points[0], d1);
            medianB = Side(Points[1], d2);
            medianC = Side(Points[2], d3);

            return (medianA, medianB, medianC);
        }
        public static (double bisecA, double bisecB, double bisecC) FindBisectors()
        {
            var sides = Get3Sides();
            double x1 = (sides.ab * sides.ac) / (sides.ab + sides.bc);
            double x2 = (sides.ac * sides.bc) / (sides.ac + sides.ab);
            double x3 = (sides.ac * sides.bc) / (sides.ac + sides.bc);

            double bisecA = Math.Sqrt((sides.ac * sides.ab) - x2 * (sides.bc - x2));
            double bisecB = Math.Sqrt((sides.ab * sides.bc) - x1 * (sides.ac - x1));
            double bisecC = Math.Sqrt((sides.ac * sides.bc) - x3 * (sides.ab - x3));

            return (bisecA,bisecB, bisecC);
        }

        public static (double x, double y) Orthocenter()
        {
            double x, y;
            if (Points[2].Y - Points[0].Y == 0)
            {
                x = Points[1].X;

                double k = -(Points[2].X - Points[1].X) / (Points[2].Y - Points[1].Y);

                y = k * (Points[1].X - Points[0].X) + Points[0].Y;
                return (x, y);
            }

            // possible error
            if (Points[2].Y - Points[1].Y == 0)
            {
                x = Points[0].X;

                double k = -(Points[2].X - Points[0].X) / (Points[2].Y - Points[0].Y);

                y = k * (Points[0].X - Points[1].X) + Points[1].Y;
                return (x, y);
            }

            double k1, k2;

            k1 = -(Points[2].X - Points[0].X) / (Points[2].Y - Points[0].Y);
            k2 = -(Points[2].X - Points[1].X) / (Points[2].Y - Points[1].Y);

            x = (Points[1].X * k1 - Points[0].X * k2 - Points[1].Y + Points[0].Y)/(k1-k2);
            y = k1 * (x - Points[1].X) + Points[1].Y;

            return (x, y);
        }
        public static (double x, double y) MassCenter() => ((Points[0].X + Points[1].X + Points[2].X) / 3, (Points[0].Y + Points[1].Y + Points[2].Y) / 3);
        public static (double x, double y, double radius) CircumscribedCircle()
        {
            double kef1 = Math.Pow(Points[0].X, 2) - Math.Pow(Points[1].X, 2) +
                    Math.Pow(Points[0].Y, 2) - Math.Pow(Points[1].Y, 2);
            double kef2 = Math.Pow(Points[1].X, 2) - Math.Pow(Points[2].X, 2) +
                    Math.Pow(Points[1].Y, 2) - Math.Pow(Points[2].Y, 2);
            double kef3 = (Points[1].X - Points[2].X) * (Points[0].Y - Points[1].Y);
            double kef4 = (Points[0].X - Points[1].X) * (Points[1].Y - Points[2].Y);

            if (Points[0].Y - Points[1].Y == 0)
            {

                double k = kef1 / (2 * (Points[0].X - Points[1].X));

                double m = (kef2 - 2 * k * (Points[1].X - Points[2].X)) / (2 * (Points[1].Y - Points[2].Y));

                Point rad = new();
                rad.X = k;
                rad.Y = m;

                return (k, m, Side(Points[0], rad));
            }

            double x = (2 * (Points[0].Y - Points[1].Y) * kef2 - 2 * (Points[1].Y - Points[2].Y) * kef1) /
                (4 * (kef3 - kef4));

            double y = (kef1 - kef1 * (Points[0].X - Points[1].X)) / (2 * (Points[0].Y - Points[1].Y));

            Point r = new();
            r.X = x;
            r.Y = y;

            double radius = Side(Points[0], r);

            return (x, y, radius);
        }
        public static (double x, double y, double radius) InscribedCircle()
        {
            var s = Get3Sides();

            double x = (s.ab * Points[0].X + s.bc * Points[1].X + s.ac * Points[2].X) / (s.ab + s.bc + s.ac);
            double y = (s.ab * Points[0].Y + s.bc * Points[1].Y + s.ac * Points[2].Y) / (s.ab + s.bc + s.ac);

            Point r = new(); 
            r.X = x;
            r.Y = y;

            return (x, y, Side(r, Points[0]));
        }

        #endregion


        #region Quadrangle
        public static (double ab, double bc, double cd, double ad) Get4Sides() =>
            (Side(Points[0], Points[1]), Side(Points[1], Points[2]), Side(Points[2], Points[3]), Side(Points[0], Points[3]));

        //діагоналі чотирикутника
        public static (double ac, double bd) GetDiagonales()
        {
            double ac = Side(Points[0], Points[2]);
            double bd = Side(Points[1], Points[3]);

            return (ac, bd);
        }

        public static (double a, double b, double c, double d) Get4Angles() =>
            (Angle(Points[3], Points[0], Points[1]), Angle(Points[0], Points[1], Points[2]),
            Angle(Points[1], Points[2], Points[3]), Angle(Points[2], Points[3], Points[0]));

        // Перевірка на тип чотирикутника
        public static QudrangleTypeEnum QudrangleType()
        {
            if (isSquare())
                return QudrangleTypeEnum.Квадрат;
            else
                if (isRectangle())
                return QudrangleTypeEnum.Прямокутник;
            else
                if (isDiamond())
                return QudrangleTypeEnum.Ромб;
            else
                if (isParallelogram())
                return QudrangleTypeEnum.Паралелограм;
            else
                if (isTrapezoid())
                return QudrangleTypeEnum.Трапеція;
            else
                return QudrangleTypeEnum.Довільний;

        }

        private static bool isParallelogram()
        {
            var s = Get4Sides();

            if (s.ab == s.cd && s.bc == s.ad)
                return true;
            else return false;
        }

        private static bool isDiamond()
        {
            var s = Get4Sides(); 

            if (s.ab == s.bc && s.bc == s.cd && s.cd == s.ad)
                return true;
            else return false;
        }

        private static bool isRectangle()
        {
            var s = Get4Angles();

            if (s.a == s.b && s.b == s.c && s.c == s.d && isParallelogram())
                return true;
            else return false;
        }

        private static bool isSquare() 
        {
            if (isDiamond() && isRectangle())
                return true;
            else
                return false;
        }

        private static bool isTrapezoid() 
        {
            Point vAB = new Point();
            vAB.X = Points[0].X - Points[1].X;
            vAB.Y = Points[0].Y - Points[1].Y;

            Point vBC = new Point();
            vBC.X = Points[1].X - Points[2].X;
            vBC.Y = Points[1].Y - Points[2].Y;

            Point vCD = new Point();
            vCD.X = Points[2].X - Points[3].X;
            vCD.Y = Points[2].Y - Points[3].Y;

            Point vDA = new Point();
            vDA.X = Points[3].X - Points[0].X;
            vDA.Y = Points[3].Y - Points[0].Y;

            if ((Parallel(vAB, vCD) && !Parallel(vBC, vDA)) || (Parallel(vBC, vDA) && !Parallel(vAB, vCD)))
                return true;
            else
                return false;
        }
        // Кінець перевірки на тип чотирикутника
        //
        // Перевірка типів трапеції

        public static TrapezoidTypeEnum TrapezoidType()
        {
            if (isRectangleTrapezoid())
                return TrapezoidTypeEnum.Прямокутна;
            else
                if (isIsoscelesTrapezoid())
                return TrapezoidTypeEnum.Рівнобедренна;
            else
                return TrapezoidTypeEnum.Довільна;
        }

        private static bool isRectangleTrapezoid()
        {
            var a = Get4Angles();

            if ((a.a == Math.PI / 2 && a.b == Math.PI / 2) || (a.b == Math.PI / 2 && a.c == Math.PI / 2) ||
                (a.c == Math.PI / 2 && a.d == Math.PI / 2) || (a.d == Math.PI / 2 && a.a == Math.PI / 2))
                return true;
            else
                return false;
        }

        private static bool isIsoscelesTrapezoid()
        {
            var s = Get4Sides();

            if (s.ab == s.cd || s.bc == s.ad)
                return true;
            else return false;
        }

        // Провірка на кола

        // Описане коло
        public static bool PossibleCircumscribed()
        {
            var s = Get4Angles();

            if (s.a + s.c == Math.PI && s.b + s.d == Math.PI)
                return true;
            else
                return false;
        }

        public static bool PosibleInscribed()
        {
            var s = Get4Sides();

            if (s.ab + s.cd == s.bc + s.ad)
                return true;
            else
                return false;
        }

        #endregion


        #region Polygon n>4

        private static Point Centroid()
        {
            Point centroid = new();
            foreach (Point i in Points)
            {
                centroid.X += i.X;
                centroid.Y += i.Y;
            }

            centroid.X /= Points.Count();
            centroid.Y /= Points.Count();

            return centroid;
        }
        public static bool RightPolygon()
        {
            if (Points.Count < 3)
                return false;

            Point centroid = Centroid();

            double len = Side(centroid, Points[0]);

            foreach (Point i in Points)
            {
                if (len != Side(i, centroid))
                    return false;
            }

            double ang = Angle(Points[Points.Count - 1], Points[0], Points[1]);

            for (int i = 1; i < Points.Count - 1; i++)
                if (ang != Angle(Points[i - 1], Points[i], Points[i + 1]))
                    return false;

            if (ang != Angle(Points[Points.Count - 2], Points[Points.Count - 1], Points[0]))
                return false;

            return true;

        }

        public static (double x, double y, double radius) RightCircum()
        {
            if (!RightPolygon())
                return (0, 0, -1);

            Point r = Centroid();

            return (r.X, r.Y, Side(r, Points[0]));
        }

        public static (double x, double y, double radius) RightInscr()
        {
            if (!RightPolygon())
                return (0, 0, -1);

            Point r = Centroid();
            Point mid = new();

            mid.X = (Points[0].X + Points[1].X) / 2;
            mid.Y = (Points[0].Y + Points[1].Y) / 2;

            return (r.X, r.Y, Side(r, mid));
        }
        #endregion

        #region Line

        public static double HorizontalLine()
        {
            if (Points[0].Y == Points[1].Y)
                return Points[0].Y;
           else return double.NaN;
        }

        public static double VerticalLine()
        {
            if (Points[0].X == Points[1].X)
                return Points[0].X;
            else return double.NaN;
        }

        public static (double k, double b) SlopeEquation()
        {
            double k = (Points[1].Y - Points[0].Y) / (Points[1].X - Points[0].X);
            double b = -Points[0].X * k + Points[0].Y;

            return (k, b); 
        }

        public static (double x0, double y0, double l, double m) CanonicalEquation()
        {
            double l = Points[1].X - Points[0].X;
            double m = Points[1].Y - Points[0].Y;

            return (Points[0].X, Points[0].Y, l, m);
        }

        public static (double A, double B, double C) StandartEquation()
        {
            double A = Points[0].Y - Points[1].Y;
            double B = Points[1].X - Points[0].X;
            double C = -Points[0].X*A - Points[0].Y*B;

            return (A, B, C);
        }

        public static (double a, double b) InterceptEquation()
        {
            var s = StandartEquation();

            double a = s.A / s.C;
            double b = s.B / s.C;

            return (a, b);
        }

        #endregion
    }
}
