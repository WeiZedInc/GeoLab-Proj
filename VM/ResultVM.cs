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
        List<string> sidesNames = new();

        [ObservableProperty]
        string[] anglesNames = { "α", "β", "γ", "δ", "ε", "ζ", "η", "θ", "λ", "ω" };

        [ObservableProperty]
        string sideType, angleType, quandrangleType, trapezoidType;
        [ObservableProperty]
        bool isTriangleVisible, isQuadrangleVisible, isTrapezoidVisible;

        public ResultVM()
        {
            OutputBaseInfo();
        }

        public void OutputBaseInfo()
        {
            sidesCount = Figure.Points.Count;
            FigureType =  Figure.FigureType().ToString();
            MakeNames();
            AllSides = Figure.AllSides();
            AllAngles = Figure.AllAngles();
            Perimeter= Figure.Perimeter();
            Area= Figure.Area();
            SwitchOutput();
        }

        void MakeNames()
        {
            string[] newAngles = new string[sidesCount];
            for (int i = 0; i < sidesCount; i++)
            {
                sidesNames.Add($"{i+1}.");
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
            }
            else if(FigureType == FigureTypeEnum.Чотирикутник.ToString())
            {
                quandrangleType = Figure.QudrangleType().ToString();
                isQuadrangleVisible= true;
                if (quandrangleType == QudrangleTypeEnum.Трапеція.ToString())
                {
                    trapezoidType = Figure.TrapezoidType().ToString();
                    isTrapezoidVisible = true;
                }
            }
        }
    }
}
