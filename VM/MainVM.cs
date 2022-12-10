using CommunityToolkit.Mvvm.ComponentModel;
using GeoLab_Proj.Geom;

namespace GeoLab_Proj
{
    public partial class MainVM : ObservableObject
    {
        [ObservableProperty]
        PointCollection points = new();
        [ObservableProperty]
        string entryText = "";
        [ObservableProperty]
        string labelOutText = "";

        string[] ManageInput()
        {
            string inputValues = EntryText;
            if (string.IsNullOrWhiteSpace(inputValues))
            {
                labelOutText = "Try next time to input some values...";
                return null;
            }

            return inputValues.Split(" ", StringSplitOptions.TrimEntries);

        }

        public async void DrawPoints()
        {
            var cuttedValues = ManageInput();
            if (cuttedValues == null)
                return;

            short pointsCounter = 0;
            foreach (var vec in cuttedValues)
            {
                if (pointsCounter == 10) // 10 points limit
                    break;

                pointsCounter++;
                if (Point.TryParse(vec, out Point p))
                {
                    labelOutText += " " + p;
                    Figure.Points.Add(p);
                }
            }

            if (Figure.Points.Count < 2)
            {
                Figure.Points.Clear();
                await Shell.Current.DisplayAlert("Ooops", "Input coordinates first ;c", "Try again");
            }
            else
                foreach (var point in Figure.Points)
                    Points.Add(point);
        }
    }
}
