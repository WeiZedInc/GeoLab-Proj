using GeoLab_Proj.Geom;
using GeoLab_Proj.Utils;
using Microsoft.Maui.Controls.Shapes;

namespace GeoLab_Proj;

public partial class MainPage : ContentPage
{
    readonly MainVM mainVM;
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

        mainVM.DrawPoints(ref Entry, ref LabelOut, ref Polygon);
        mainVM.Test(ref LabelOut);
    }
}