using CommunityToolkit.Mvvm.ComponentModel;

namespace EmployeeList.DTOs
{
    public partial class EmployeeDTO : ObservableObject
    {
        [ObservableProperty]
        public int idEmployee;
        [ObservableProperty]
        public string fullName;
        [ObservableProperty]
        public string email;
        [ObservableProperty]
        public decimal salary;
        [ObservableProperty]
        public DateTime contractDate;
    }
}
