using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Drwal_SEW_Projekt_EF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoDController : ControllerBase
    {
        // GET: api/<VoDController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<VoDController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<VoDController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<VoDController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VoDController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
