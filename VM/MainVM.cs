using CommunityToolkit.Mvvm.ComponentModel;
using GeoLab_Proj.Geom;
using Microsoft.Maui.Controls.Shapes;

namespace GeoLab_Proj
{
    public partial class MainVM : ObservableObject
    {
        string[] ManageInput(ref Entry Entry, ref Label LabelOut)
        {
            string inputValues = Entry.Text;
            if (string.IsNullOrWhiteSpace(inputValues))
            {
                LabelOut.Text = "Try next time to input some values...";
                return null;
            }

            return inputValues.Split(" ", StringSplitOptions.TrimEntries);

        }

        public void DrawPoints(ref Entry entry, ref Label labelOut,ref Polygon polygon)
        {
            var cuttedValues = ManageInput(ref entry, ref labelOut);
            if (cuttedValues == null)
                return;

            Point p;
            short pointsCounter = 0;
            foreach (var vec in cuttedValues)
            {
                if (pointsCounter == 10) // 10 points limit
                    break;

                pointsCounter++;
                Point.TryParse(vec, out p);
                labelOut.Text += " " + p;

                Figure.Points.Add(p);
            }

            foreach (var point in Figure.Points)
                polygon.Points.Add(point);
        }

        public void Test(ref Label labelOut)
        {
            var sos = Figure.TriangleType();
            labelOut.Text = sos.isTriangle + sos.angleType.ToString() + sos.sideType.ToString();
        }
    }
}
