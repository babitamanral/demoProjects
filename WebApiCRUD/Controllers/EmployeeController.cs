using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using WebApiCRUD.Models;

namespace WebApiCRUD.Controllers
{
    public class EmployeeController : ApiController
    {
        //GET
        public IHttpActionResult GetAllEmployee()
        {
            IList<EmployeeViewModel> employeeList = null;
            using (var x = new WebAPIEntities())
            {
                employeeList = x.Employees
                       .Select(e => new EmployeeViewModel()
                       {
                           Id=e.id,
                           Name=e.name,
                           Location=e.location,
                           Designation=e.designation
                       }).ToList<EmployeeViewModel>();
            
            }
            if (employeeList.Count == 0)
            {
                return NotFound();
            }
            return Ok(employeeList);
        }

        //POST
        public IHttpActionResult PostEmployee(EmployeeViewModel employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Entries.!!");
            }
            using (var x = new WebAPIEntities())
            {
                x.Employees.Add(new Employee()
                {
                    id = employee.Id,
                    name = employee.Name,
                    location = employee.Location,
                    designation = employee.Designation
                });
                x.SaveChanges();
            }
            return Ok();
        }

        //PUT
        public IHttpActionResult PutEmployee(EmployeeViewModel employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Entries.!!");
            }
            using (var x = new WebAPIEntities())
            {

                var checkIfExits = x.Employees.Where(e => e.id == employee.Id).FirstOrDefault<Employee>();
                if (checkIfExits != null)
                {
                    checkIfExits.id = employee.Id;
                    checkIfExits.name = employee.Name;
                    checkIfExits.location = employee.Location;
                    checkIfExits.designation = employee.Designation;

                    x.SaveChanges();
                }
                else
                    return NotFound();
            }
            return Ok();
        }
        //DELETE
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Please Enter valid id");
            using (var x = new WebAPIEntities())
            {
                var employee = x.Employees.Where(e => e.id == id).FirstOrDefault();

                x.Entry(employee).State = System.Data.Entity.EntityState.Deleted;
                x.SaveChanges();
            }
            return Ok();
        }

    }
}
