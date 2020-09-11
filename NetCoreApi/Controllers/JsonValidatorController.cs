using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
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

        [Route("ValidateforJSONSchema")]
        public ServiceResult PostValidateSchema()
        {
            string userid = HttpContext.Request.Headers["raju"].ToString();
            var files = HttpContext.Request.Form.Files;
            var vdfworkingdirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FOLDERNAME");
            var vdfworkingfile = $"{vdfworkingdirectory}\\{files[0].FileName}";
            string str = $"{vdfworkingfile}";

            if (!string.IsNullOrWhiteSpace(str) && Path.GetExtension(str).Equals(".csv", StringComparison.InvariantCultureIgnoreCase))
            {
                using (FileStream fs = new FileStream(vdfworkingfile, FileMode.Create))
                {
                    files[0].CopyTo(fs);

                    using (StreamReader reader = new StreamReader("C:\\Users\\Raju\\Downloads\\CHeckCSV\\qrreport_FTPA2000-260_1181640-001.csv"))
                    {
                        // var  csv = reader.ReadToEnd();
                        var csv = new List<string[]>(); // or, List<YourClass>
                        var lines = System.IO.File.ReadAllLines(@"C:\Users\Raju\Downloads\CHeckCSV\qrreport_FTPA2000-260_1181640-001.csv");
                        foreach (string line in lines)
                            csv.Add(line.Split(',')); // or, populate YourClass          
                        //string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(csv);
                    }
                }

            }
            test();
            return new ServiceResult
            {
                StatusCode = 2000,
                Content = "test success"
            };

        }

        [Obsolete]
        private void test()
        {
            JsonSchema schema2 = new JsonSchema();
            schema2.Type = JsonSchemaType.Object;
            schema2.Properties = new Dictionary<string, JsonSchema>
         {
            { "name", new JsonSchema
               { Type = JsonSchemaType.String } },
            { "id", new JsonSchema
               { Type = JsonSchemaType.String } },
            { "company", new JsonSchema
               { Type = JsonSchemaType.String } },
            { "role", new JsonSchema
               { Type = JsonSchemaType.String } },
            {
               "skill", new JsonSchema
               {
                  Type = JsonSchemaType.Array,
                  Items = new List<JsonSchema>
                     { new JsonSchema
                        { Type = JsonSchemaType.String } }
               }
            },
         };
            JObject employee2 = JObject.Parse(@"{
            'name': 'Tapas', 'id': '12345',
            'company': 'TCS', 'role': 'developer',
            'skill': ['.NET', 'JavaScript', 'C#', 'Angular',
            'HTML']
         }");
            bool valid2 = employee2.IsValid(schema2);
        }

        /// <summary>
        /// SEND file with postman and try to read;
        /// </summary>
        /// <param name="objfile"></param>
        /// <returns></returns>

        [Route("ValidateJson")]
        [HttpPost]
        public async Task<string> Post([FromForm] FileUpload objfile)
        {
            //List<EmployeeD> empList = JsonConvert.DeserializeObject<List<EmployeeD>>(objfile.Employees);
            if (objfile.files.Length > 0)
            {

            }
            return "";

        }


    }
    public class ServiceResult
    {
        public int StatusCode { get; set; }
        public object Content { get; set; }
    }

    public class FileUpload
    {
        public IFormFile files { get; set; }

    }

    public class EmployeeD
    {
        public int EmpId { get; set; }
        public String EmpName { get; set; }
        public String Designation { get; set; }
    }
}
