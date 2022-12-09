using GeoLab_Proj.Geom;
using GeoLab_Proj.Utils;
using Microsoft.Maui.Controls.Shapes;

namespace GeoLab_Proj;

public partial class MainPage : ContentPage
{
    readonly MainVM mainVM;
    public static Polygon Poly;
    public MainPage()
	{
		InitializeComponent();
        mainVM = ServiceHelper.GetService<MainVM>();
        BindingContext = mainVM;
    }

    private void OnDrawPolygonClicked(object sender, EventArgs e)
	{
        LabelOut.Text = "";
        Polygon.Points.Clear();
        Figure.Points.Clear();
        Poly = Polygon;

        mainVM.DrawPoints(ref Entry, ref LabelOut, ref Polygon);
        mainVM.Test(ref LabelOut);
    }

    private async void CalculateBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ResultPage");
    }
}