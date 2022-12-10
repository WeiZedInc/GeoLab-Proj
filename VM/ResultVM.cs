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
        string sideType, angleType;
        [ObservableProperty]
        bool isSideAngleTypeVisible = false;

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
            for (int i = 0; i < sidesCount; i++)
                sidesNames.Add($"h{i+1}");
        }

        void SwitchOutput()
        {
            if (FigureType == FigureTypeEnum.Трикутник.ToString())
            {
                SideType = Figure.TriangleSideType().ToString();
                AngleType = Figure.TriangleAngleType().ToString();
                IsSideAngleTypeVisible = true;
            }
            else if(FigureType == FigureTypeEnum.Чотирикутник.ToString())
            {

            }
        }
    }
}
