using CommunityToolkit.Mvvm.ComponentModel;
using GeoLab_Proj.Geom;

namespace GeoLab_Proj
{
    public partial class ResultVM : ObservableObject
    {
        int sidesCount = 0;

        [ObservableProperty]
        string figureType;
        [ObservableProperty]
        List<double> allSides = new();
        [ObservableProperty]
        List<string> sidesNames = new(); 

        public ResultVM()
        {
            OutputBaseInfo();
        }

        public void OutputBaseInfo()
        {
            FigureType =  Figure.FigureType().ToString();
            sidesCount = Figure.Points.Count;
            // allSides = 
        }
    }
}
