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
        mainVM.LabelOutText = "";
        mainVM.Points.Clear();
        Figure.Points.Clear();

        mainVM.DrawPoints();
        InitializeComponent();
    }

    private async void CalculateBtn_Clicked(object sender, EventArgs e)
    {
        if (mainVM.Points.Count == 0)
        {
            await DisplayAlert("Ooops", "Input coordinates first ;c", "Try again");
            return;
        }

        await Shell.Current.GoToAsync("ResultPage");
    }
}