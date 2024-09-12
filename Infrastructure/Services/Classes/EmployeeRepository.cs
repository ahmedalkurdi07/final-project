using Application.Domain.Models;
using Infrastructure.DbContexts;
using Infrastructure.Services.Interfaces;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Classes
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }
        public Employee? CreateEmployee(int CenterId, EmployeeForCreate item)
        {
            var center = _context.Centers.FirstOrDefault(c => c.Id == CenterId && c.IsActive);
            if (center == null) return null;

            var newEmployee = new Employee()
            {
                FirstName = item.FirstName,
                LastName = item.LastName,
                Gender = item.Gender,
                ClinicId = item.ClinicId,
                PhoneNumber = item.PhoneNumber,
                Email = item.Email,
                AddressId = item.AddressId,
                IsActive = item.IsActive,
                CenterId = item.CenterId,
                Role = item.Role,
                
            };

            _context.Employees.Add(newEmployee);
            SaveChanges();

            return newEmployee;
        }

        public Employee? DeleteById(int CenterId, int id)
        {
            var employee = GetByIdAsync(CenterId, id);

            if (employee == null)
                return null;

            employee.Result!.IsActive = false;

            _context.Employees.Update(employee.Result!);
            SaveChanges();

            return employee.Result!;
        }

        public async Task<(List<Employee>, PaginationMetaData)> GetAllAsync(int CenterId, int pageNumber, int pageSize)
        {
            var totalItemCount = await _context.Employees.Where(i => i.IsActive && i.CenterId == CenterId).CountAsync();
            var paginationdata = new PaginationMetaData(totalItemCount, pageSize, pageNumber);

            var all = await _context.Employees.Where(c => c.IsActive)
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            return (all, paginationdata);
        }

        public async Task<Employee?> GetByIdAsync(int CenterId, int id)
        {
            var center = await _context.Clinics.FirstOrDefaultAsync(c => c.Id == CenterId && c.IsActive);
            if (center == null) return null;

            var employee = await _context.Employees
                .FirstOrDefaultAsync(p => p.EmployeeId == id && p.IsActive && p.ClinicId == CenterId);

            if (employee == null)
                return null;

            return employee;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<(Employee?, List<string>)> UpdateByIdAsync(int CenterId, int id, JsonPatchDocument<EmployeeForUpdate> PatchDocument)
        {
            var employee = GetByIdAsync(CenterId, id);
            if (employee == null) return (null, new List<string> { "Employee not found." }); ;
            var emp = employee.Result!;

            var employeeToPatch = new EmployeeForUpdate()
            {
                AddressId = emp.AddressId,
                Address = emp.Address,
                Email = emp.Email,
                FirstName = emp.FirstName,
                Gender = emp.Gender,
                IsActive = emp.IsActive,
                LastName = emp.LastName,
                PhoneNumber = emp.PhoneNumber,
                ClinicId = emp.ClinicId,
                Clinic = emp.Clinic,
                CenterId = emp.CenterId,
                Center = emp.Center,
                Role = emp.Role,

            };
            var errors = new List<string>();
            try
            {
                PatchDocument.ApplyTo(employeeToPatch, error =>
                {
                    errors.Add(error.ErrorMessage);
                });
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
            }
            emp.FirstName = employeeToPatch.FirstName;
            emp.LastName = employeeToPatch.LastName;
            emp.PhoneNumber = employeeToPatch.PhoneNumber;
            emp.Email = employeeToPatch.Email;
            emp.Gender = employeeToPatch.Gender.Value;
            emp.IsActive = employeeToPatch.IsActive.Value;
            emp.Role = employeeToPatch.Role;

            emp.AddressId = employeeToPatch.AddressId.Value;
            emp.Address = employeeToPatch.Address;

            emp.Clinic = employeeToPatch.Clinic;
            emp.ClinicId = employeeToPatch.ClinicId.Value;

            emp.Center = employeeToPatch.Center;
            emp.CenterId = employeeToPatch.CenterId.Value;


            _context.Update(employee);
            await SaveChangesAsync();
            return (emp, errors);
        }
    }
}
