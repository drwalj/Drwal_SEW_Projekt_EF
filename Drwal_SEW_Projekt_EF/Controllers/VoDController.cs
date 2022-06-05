using Microsoft.AspNetCore.Mvc;
using Drwal_SEW_Projekt_EF.Data;
using Drwal_SEW_Projekt_EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Drwal_SEW_Projekt_EF.Controllers
{
    [Route("api/VoD")]
    [ApiController]
    public class VoDController : ControllerBase
    {

        vod_drwalContext context = new vod_drwalContext();


        [HttpGet("/Movie")]
        public ActionResult<List<Movie>> GetAllMovies()
        {
            Console.WriteLine("Recieved request for: GetAllMovies...");
            return Ok(context.Movie);
        }

        [HttpGet("/Clients")]
        public ActionResult<List<Client>> GetAllClients()
        {
            Console.WriteLine("Recieved request for: GetAllClients...");
            return Ok(context.Client);
        }


        [HttpGet("Client/{id}")]
        public ActionResult<Client> GetClientWithId(int id)
        {
            try
            {
                Console.WriteLine($"Recieved request for: GetClientWithId({id})");
                Client suspect = context.Client.FirstOrDefault(a => a.ClientId == id);
                if (suspect!=null)
                {
                    Console.WriteLine("Client Found!");
                    return Ok(suspect);
                }

                else
                {
                    Console.WriteLine("Client NOT Found!");
                    return NotFound($"Client {id} not found...");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        [HttpPost()]
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
