using Point = Microsoft.Maui.Graphics.Point;

namespace GeoLab_Proj;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		LabelOut.Text = "";
		Polygon.Points.Clear();
		List<double> vectors = new();

        string inputValues = Entry.Text;
        if (string.IsNullOrWhiteSpace(inputValues))
        {
            Console.WriteLine("Try next time to input some values...");
            
        }
        string[] cuttedValues = inputValues.Split(" ", StringSplitOptions.TrimEntries);
		foreach (var item in cuttedValues)
		{
            Point p;
            Point.TryParse(item, out p);
            Polygon.Points.Add(p);
            LabelOut.Text += " " + p;
        }
    }
}

