using GeoLab_Proj.Utils;

namespace GeoLab_Proj;

public partial class ResultPage : ContentPage
{
	readonly ResultVM resultVM;
	public ResultPage()
	{
		InitializeComponent();
		resultVM = ServiceHelper.GetService<ResultVM>();
		BindingContext = resultVM;
	}

}