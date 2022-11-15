using Microsoft.Maui.Controls.Shapes;
using Point = Microsoft.Maui.Graphics.Point;

namespace GeoLab_Proj;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private void OnDrawPolygonClicked(object sender, EventArgs e)
	{
		LabelOut.Text = "";
        Polygon.Points.Clear();

        ManageInput();
    }

    void ManageInput()
    {
        string inputValues = Entry.Text;
        if (string.IsNullOrWhiteSpace(inputValues))
        {
            LabelOut.Text = "Try next time to input some values...";
            return;
        }

        string[] cuttedValues = inputValues.Split(" ", StringSplitOptions.TrimEntries);
        Point p;
        short pointsCounter = 0;
        foreach (var vec in cuttedValues)
        {
            if (pointsCounter == 10) // 10 points limit
                break;

            pointsCounter++;
            Point.TryParse(vec, out p);
            Polygon.Points.Add(OptimizePoints(p));
            LabelOut.Text += " " + p;
        }
    }
    Point OptimizePoints(Point p)
    {
        if (p.X < 10)
            p.X = p.X * 10;
        else if (p.X > 100)
            p.X = p.X / 10;

        if (p.Y < 10)
            p.Y = p.Y * 10;
        else if (p.Y > 100)
            p.Y = p.Y / 10;
        return p;
    }
}