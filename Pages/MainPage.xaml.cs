using Microsoft.Maui.Controls.Shapes;
using Point = Microsoft.Maui.Graphics.Point;

namespace GeoLab_Proj;

public partial class MainPage : ContentPage
{
    List<Point> points = new();
	public MainPage()
	{
		InitializeComponent();
	}

	private void OnDrawPolygonClicked(object sender, EventArgs e)
	{
		LabelOut.Text = "";
        Polygon.Points.Clear();

        //DrawPoints();
    }
}