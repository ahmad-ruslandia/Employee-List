using EmployeeList.ViewModels;

namespace EmployeeList.Views;

public partial class EmployeePage : ContentPage
{
	public EmployeePage(EmployeeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}