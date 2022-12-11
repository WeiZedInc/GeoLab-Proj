using CommunityToolkit.Mvvm.ComponentModel;
using GeoLab_Proj.Geom;
using System.Collections.ObjectModel;

namespace GeoLab_Proj
{
    public partial class ResultVM : ObservableObject
    {
        int sidesCount = 0;

        [ObservableProperty]
        string figureType;
        [ObservableProperty]
        double perimeter, area;
        [ObservableProperty]
        List<double> allSides = new();
        [ObservableProperty]
        List<double> allAngles = new();
        [ObservableProperty]
        List<double> allNorms = new();
        [ObservableProperty]
        List<string> sidesNames = new();

        [ObservableProperty]
        (double x, double y) orthocentre;

        [ObservableProperty]
        string[] normsNames = new string[] { "h1", "h2", "h3" };

        [ObservableProperty]
        string[] anglesNames = { "α", "β", "γ", "δ", "ε", "ζ", "η", "θ", "λ", "ω" };

        [ObservableProperty]
        string sideType, angleType, quandrangleType, trapezoidType, polygonType, medians, bisectors, 
            circumscribed, inscribed, circlePossible, rightPolyInscCircle, rightPolyCircumCircle, centremass;
        [ObservableProperty]
        bool isTriangleVisible, isQuadrangleVisible, isTrapezoidVisible, isPolygonVisible, isRightPolygonVisible;

        public ResultVM()
        {
            OutputBaseInfo();
        }

        public void OutputBaseInfo()
        {
            sidesCount = Figure.Points.Count;
            FigureType = Figure.FigureType().ToString();
            MakeNames();
            AllSides = Figure.AllSides();
            AllAngles = Figure.AllAngles();
            Perimeter = Figure.Perimeter();
            Area = Figure.Area();
            SwitchOutput();
        }

        void MakeNames()
        {
            string[] newAngles = new string[sidesCount];
            for (int i = 0; i < sidesCount; i++)
            {
                sidesNames.Add($"{i + 1}.");
                newAngles[i] = anglesNames[i];
            }
            anglesNames = newAngles;
        }

        void SwitchOutput()
        {
            if (FigureType == FigureTypeEnum.Трикутник.ToString())
            {
                SideType = Figure.TriangleSideType().ToString();
                angleType = Figure.TriangleAngleType().ToString();
                isTriangleVisible = true;

                var norms = Figure.FindNorms();
                AllNorms.Add(norms.a);
                AllNorms.Add(norms.b);
                AllNorms.Add(norms.c);

                Orthocentre = Figure.Orthocenter();

                var medi = Figure.FindMedians();
                Medians = $"({medi.a:F2}; {medi.b:F2}; {medi.c:F2})";

                var cent = Figure.MassCenter();
                centremass = $"({cent.x:F2}; {cent.y:F2};)";

                var bisec = Figure.FindBisectors();
                Bisectors = $"({bisec.bisecA:F2}; {bisec.bisecB:F2}; {bisec.bisecC:F2})";

                var insc = Figure.InscribedCircle();
                Inscribed = $"(x={insc.x:F2}; y={insc.y:F2}; R={insc.radius:F2})";

                var circum = Figure.CircumscribedCircle();
                Circumscribed = $"(x={circum.x:F2}; y={circum.y:F2}; r={circum.radius:F2})";
            }
            else if (FigureType == FigureTypeEnum.Чотирикутник.ToString())
            {
                quandrangleType = Figure.QudrangleType().ToString();
                if (Figure.PossibleCircumscribed())
                    circlePossible = "описане";
                else
                    circlePossible = "вписане";

                isQuadrangleVisible = true;
                if (quandrangleType == QudrangleTypeEnum.Трапеція.ToString())
                {
                    trapezoidType = Figure.TrapezoidType().ToString();
                    isTrapezoidVisible = true;
                }
            }
            else if (FigureType == FigureTypeEnum.Многокутник.ToString())
            {
                if (Figure.RightPolygon())
                {
                    PolygonType = "Правильний";
                    var inscr = Figure.RightInscr();
                    var circum = Figure.RightCircum();

                    rightPolyInscCircle = $"(x={inscr.x:F2}; y={inscr.y:F2}; R={inscr.radius:F2})";
                    rightPolyCircumCircle = $"(x={circum.x:F2}; y={circum.y:F2}; r={circum.radius:F2})";
                    IsRightPolygonVisible = true;
                }
                else
                    PolygonType = "Довільний";
                isPolygonVisible = true;
            }
        }
    }
}
