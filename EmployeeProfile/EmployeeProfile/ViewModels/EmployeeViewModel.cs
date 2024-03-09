
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using EmployeeProfile.DataAccess;
using EmployeeProfile.DTOs;
using EmployeeProfile.Utilities;
using EmployeeProfile.Models;

namespace EmployeeProfile.ViewModels
{
    public partial class EmployeeViewModel : ObservableObject, IQueryAttributable
    {
        private readonly EmployeeDbContext _dbContext;

        [ObservableProperty]
        private EmployeeDTO employeeDto = new EmployeeDTO();

        [ObservableProperty]
        private string tituloPagina;

        private int IdEmployee;

        [ObservableProperty]
        private bool loadingEsVisible = false;

        public EmployeeViewModel(EmployeeDbContext context)
        {
            _dbContext = context;
            EmployeeDto.ContractDate = DateTime.Now;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var id = int.Parse(query["id"].ToString());
            IdEmployee = id;

            if(IdEmployee == 0)
            {
                TituloPagina = "Add Employee";
            }
            else
            {
                TituloPagina = "Edit Employee";
                LoadingEsVisible = true;
                await Task.Run(async () =>
                {
                    var encontrado = await _dbContext.Employee.FirstAsync(e => e.IdEmployee == IdEmployee);
                    EmployeeDto.IdEmployee = encontrado.IdEmployee;
                    EmployeeDto.FullName = encontrado.FullName;
                    EmployeeDto.Email = encontrado.Email;
                    EmployeeDto.Salary = encontrado.Salary;
                    EmployeeDto.ContractDate = encontrado.ContractDate;

                    MainThread.BeginInvokeOnMainThread(() => { LoadingEsVisible = false; });
                });
            }
        }

        [RelayCommand]
        private async Task Submit()
        {
            LoadingEsVisible = true;
            EmployeeMessage message = new EmployeeMessage();

            await Task.Run(async () =>
            {
                if(IdEmployee == 0)
                {
                    var tbEmployee = new Employee
                    {
                        FullName = EmployeeDto.FullName,
                        Email = EmployeeDto.Email,
                        Salary = EmployeeDto.Salary,
                        ContractDate = EmployeeDto.ContractDate,
                    };

                    _dbContext.Employee.Add(tbEmployee);
                    await _dbContext.SaveChangesAsync();

                    EmployeeDto.IdEmployee = tbEmployee.IdEmployee;
                    message = new EmployeeMessage()
                    {
                        create = true,
                        EmployeeDto = EmployeeDto
                    };

                }
                else
                {
                    var encontrado = await _dbContext.Employee.FirstAsync(e => e.IdEmployee == IdEmployee);
                    encontrado.FullName = EmployeeDto.FullName;
                    encontrado.Email = EmployeeDto.Email;
                    encontrado.Salary = EmployeeDto.Salary;
                    encontrado.ContractDate = EmployeeDto.ContractDate;

                    await _dbContext.SaveChangesAsync();

                    message = new EmployeeMessage()
                    {
                        create = false,
                        EmployeeDto = EmployeeDto
                    };

                }

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    LoadingEsVisible = false;
                    WeakReferenceMessenger.Default.Send(new EmployeeMessages(message));
                    await Shell.Current.Navigation.PopAsync();
                });

            });
        }


    }
}
