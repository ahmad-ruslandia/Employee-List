using EmployeeProfile.ViewModels;

namespace EmployeeProfile.Views;

public partial class EmployeePage : ContentPage
{
	public EmployeePage(EmployeeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}