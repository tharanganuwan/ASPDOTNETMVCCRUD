using ASPDOTNETMVCCRUD.Data;
using ASPDOTNETMVCCRUD.Models;
using ASPDOTNETMVCCRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPDOTNETMVCCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MVCDbContex _dbContext;

        public EmployeeController( MVCDbContex DbContext)
        {
            _dbContext = DbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employee = await _dbContext.Employee.ToListAsync();
            return View(employee);
        }
         

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest) 
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
            };

            await _dbContext.Employee.AddAsync(employee);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid Id)
        {
            var employee = await _dbContext.Employee.FirstOrDefaultAsync(x => x.Id == Id);

            if(employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth,
                };

				return await Task.Run(() => View("View",viewModel));
			}

			return RedirectToAction("Index");

		}

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await _dbContext.Employee.FindAsync(model.Id);

            if(employee != null) 
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.Department = model.Department;
                employee.DateOfBirth = model.DateOfBirth;

                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await _dbContext.Employee.FindAsync(model.Id);

            if(employee != null) 
            {
                 _dbContext.Employee.Remove(employee);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
