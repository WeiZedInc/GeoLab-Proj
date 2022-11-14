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

        string inputValues = Entry.Text;
        if (string.IsNullOrWhiteSpace(inputValues))
        {
            LabelOut.Text = "Try next time to input some values...";
            return;
        }

        string[] cuttedValues = inputValues.Split(" ", StringSplitOptions.TrimEntries);
        Point p;
        foreach (var item in cuttedValues)
		{
            Point.TryParse(item, out p);
            Polygon.Points.Add(p);
            LabelOut.Text += " " + p;
        }
    }
}

