using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using EmployeeProfile.DataAccess;
using EmployeeProfile.DTOs;
using EmployeeProfile.Utilities;
using EmployeeProfile.Models;
using System.Collections.ObjectModel;
using EmployeeProfile.Views;

namespace EmployeeProfile.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly EmployeeDbContext _dbContext;
        [ObservableProperty]
        private ObservableCollection<EmployeeDTO> listEmployee = new ObservableCollection<EmployeeDTO>();

        public MainViewModel(EmployeeDbContext context)
        {
            _dbContext = context;

            MainThread.BeginInvokeOnMainThread(new Action(async () => await Obtener()));

            WeakReferenceMessenger.Default.Register<EmployeeMessages>(this, (r, m) =>
            {
                EmployeeMessageReceived(m.Value);
            });
        }

        public async Task Obtener()
        {
            var lista = await _dbContext.Employee.ToListAsync();
            if(lista.Any())
            {
                foreach(var item in lista)
                {
                    ListEmployee.Add(new EmployeeDTO
                    {
                        IdEmployee = item.IdEmployee,
                        FullName = item.FullName,
                        Email = item.Email,
                        Salary = item.Salary,
                        ContractDate = item.ContractDate,
                    });
                }
            }
        }

        private void EmployeeMessageReceived(EmployeeMessage employeeMessage)
        {
            var employeeDto = employeeMessage.EmployeeDto;

            if (employeeMessage.create)
            {
                ListEmployee.Add(employeeDto);
            }
            else
            {
                var encontrado = ListEmployee
                    .First(e => e.IdEmployee == employeeDto.IdEmployee);

                encontrado.FullName = employeeDto.FullName;
                encontrado.Email = employeeDto.Email;
                encontrado.Salary = employeeDto.Salary;
                encontrado.ContractDate = employeeDto.ContractDate;

            }

        }

        [RelayCommand]
        private async Task Crear()
        {
            var uri = $"{nameof(EmployeePage)}?id=0";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Edit(EmployeeDTO employeeDto)
        {
            var uri = $"{nameof(EmployeePage)}?id={employeeDto.IdEmployee}";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Delete(EmployeeDTO employeeDto)
        {
            bool answer = await Shell.Current.DisplayAlert("Message", "Are you sure want to delete this?", "Yes", "Cancel");

            if (answer)
            {
                var encontrado = await _dbContext.Employee
                    .FirstAsync(e => e.IdEmployee == employeeDto.IdEmployee);

                _dbContext.Employee.Remove(encontrado);
                await _dbContext.SaveChangesAsync();
                ListEmployee.Remove(employeeDto);

            }

        }


    }
}
