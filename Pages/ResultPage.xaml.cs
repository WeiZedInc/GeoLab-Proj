using GeoLab_Proj.Utils;

namespace GeoLab_Proj;

public partial class ResultPage : ContentPage
{
	readonly ResultVM resultVM;
	readonly MainVM mainVM;
	public ResultPage()
	{
		InitializeComponent();
		resultVM = ServiceHelper.GetService<ResultVM>();
        mainVM = ServiceHelper.GetService<MainVM>();
		BindingContext = resultVM;

		DrawPoly();
	}

	public void DrawPoly()
	{
		Root.Add(MainPage.Poly);
	}

}