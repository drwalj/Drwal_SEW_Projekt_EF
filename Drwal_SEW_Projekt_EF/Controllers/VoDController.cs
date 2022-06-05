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


        [HttpGet("/Movie/{Genre}")]
        public ActionResult<List<Movie>> GetMovieWithGenre(string Genre)
        {
            Console.WriteLine($"Recieved request for: GetMovieWithGenre({Genre})...");
            return Ok(context.Movie.Where(a=>a.Type.Contains(Genre)));
        }

        [HttpGet("/Client/{id}")]
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

        [HttpPatch("/PatchClient/{clientId}")]
        public ActionResult PatchClient([FromBody] Client meinNeuerClient, int clientId)
        {
            Console.WriteLine($"Recieved Request for: PatchClient({clientId})");
            Client clientToPatch = context.Client.FirstOrDefault(a=>a.ClientId == clientId);

            if (clientToPatch != null)
            {
                clientToPatch = meinNeuerClient;
                return Ok("Sucessfully Patched Client!");

            }
            else
            {
                return NotFound($"Client with Id {clientId} not found!");
            }

        }


        [HttpPost("/AddOrder/{clientId}")]
        public ActionResult PostNewOrder([FromBody] Order myNewOrder, int clientId)
        {
            var clientXorder = context.Client.Where(a=> a.ClientId.Equals(clientId)).FirstOrDefault();
            Console.WriteLine($"Recieved request for:  PostNewOrder for ClientId {clientId}");
            if (clientXorder == null)
            {
                return NotFound($"Client with Id {clientId} not found");
            }
            else
            {

                clientXorder.Order.Add(myNewOrder);
                return Ok("Sucessfully Added Order!");  
            }


        }


        [HttpPost("/AddClient")]
        public ActionResult PostNewClient([FromBody] Client meinNeuerClient)
        {
            context.Client.Add(meinNeuerClient);
            return Ok("Sucessfully Added Client!");
        }

        
        [HttpDelete("/DeleteClient/{id}")]
        public ActionResult DeleteClient(int id)
        {
            try
            {
                Client a = context.Client.FirstOrDefault(a => a.ClientId == id);
                context.Client.Remove(a);
                return Ok("Sucessfully removed Client");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
