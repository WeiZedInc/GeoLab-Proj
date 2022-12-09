using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoLab_Proj.VM
{
    public class MainVM : ObservableObject
    {
        List<Point> points = new();

        string[] ManageInput(Entry Entry, Label LabelOut)
        {
            string inputValues = Entry.Text;
            if (string.IsNullOrWhiteSpace(inputValues))
            {
                LabelOut.Text = "Try next time to input some values...";
                return null;
            }

            return inputValues.Split(" ", StringSplitOptions.TrimEntries);

        }

        void DrawPoints(Entry entry, Label labelOut, Polygon polygon)
        {
            var cuttedValues = ManageInput(entry, labelOut);
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
                points.Add(p);
                labelOut.Text += " " + p;
            }

            foreach (var point in points)
                polygon.Points.Add(point);
        }
    }
}
