using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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
        private readonly DatabaseContext _context;

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
            var emp = _context.Employee.FirstOrDefault(a => a.id == id);
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
            var empInDb = _context.Employee.FirstOrDefault(a => a.id == employee.id);
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

        [HttpGet]
        [Route("test")]
        public IActionResult Upload1()
        {
            return Ok("hi");
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("upload")]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }
    }
}
