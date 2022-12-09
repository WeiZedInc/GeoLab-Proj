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

        /// <returns>returns angle for point b</returns>
        public static double Angle(Point a, Point b, Point c)
        {
            double x = (b.X - a.X) * (c.X - b.X);
            double y = (b.Y - a.Y) * (c.Y - b.Y);
            double length = Side(b, a) * Side(c, b);

            return Math.Acos((x + y) / length);
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


    }
}
