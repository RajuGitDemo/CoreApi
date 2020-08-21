using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreApi.Models;

namespace NetCoreApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DatabaseContext _context ;

        public EmployeeController(DatabaseContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult<List<Employee>> Get()
        {
            return Ok(_context.Employee.ToList());
        }
        [Route("{id}")]
        [HttpGet]
        public ActionResult<Employee> Get(int id)
        {
            var emp= _context.Employee.FirstOrDefault(a => a.id == id);
            return Ok(emp);
        }
        [HttpPost]
        public ActionResult<Employee> post(Employee employee)
        {
            _context.Employee.Add(employee);
            _context.SaveChanges();
            return Ok(employee);
        }

        [HttpPut]
        public ActionResult<Employee> put(Employee employee)
        {
           var empInDb= _context.Employee.FirstOrDefault(a => a.id == employee.id);
            empInDb.name = employee.name;
            empInDb.email = employee.email;
            empInDb.password = employee.password;
            _context.SaveChanges();
            return Ok(employee);
        }
        [Route("{id}")]
        [HttpDelete]

        public ActionResult<Employee> delete(int id)
        {
            var emp = _context.Employee.FirstOrDefault(a => a.id == id);
            _context.Remove(emp);
            _context.SaveChanges();
            return Ok(emp);
        }

    }
}
