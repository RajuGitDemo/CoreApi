using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetCoreApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JsonValidatorController : ControllerBase
    {
        // GET: api/<JsonValidatorController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<JsonValidatorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<JsonValidatorController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<JsonValidatorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<JsonValidatorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        /// <summary>
        /// This ServiceResult Post() Method use for checking upload file with help of Postman application.
        /// checking steps:
        /// (1):go to post man put url 'https://localhost:44365/JsonValidator/Service'
        /// (2): select post method
        /// (3):select body tab and radio button form-data
        /// (4): put Key column any name and select file from dropdown after then browse file and select any file put them in value field.
        /// (5):then click sed button
        /// </summary>
        /// <returns></returns>
        [Route("Service")]
        public ServiceResult Post()
        {
        
            string userid = HttpContext.Request.Headers["raju"].ToString();
            var files = HttpContext.Request.Form.Files;
            var vdfworkingdirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FOLDERNAME");
            var vdfworkingfile = $"{vdfworkingdirectory}\\{files[0].FileName}";
            return new ServiceResult
            {
                StatusCode = 2000,
                Content = "test success"
            };

        }

        public class ServiceResult
        {
            public int StatusCode { get; set; }
            public object Content { get; set; }
        }
    }
}
