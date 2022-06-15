﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VodLib.data;
using VodLib.models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Drwal_SEW_Projekt_EF.Controllers
{
    [Route("api/VoD")]
    [ApiController]

    public class VoDController : ControllerBase
    {
        public static vod_drwalContext context = new vod_drwalContext();

        [HttpGet("movies")]
        public ActionResult<List<Movie>> GetAllMovies()
        {
            Console.WriteLine("Recieved request for: GetAllMovies...");
            return Ok(context.Movie);
        }

        [HttpGet("clients")]
        public ActionResult<List<Client>> GetAllClients()
        {
            Console.WriteLine("Recieved request for: GetAllClients...");
            return Ok(context.Client);
        }


        [HttpGet("movies/{genre}/{title}")]
        public ActionResult<List<Movie>> GetMoviesWithGenreAndTitle(string title, string genre)
        {
            Console.WriteLine($"Recieved request for: GetMovieWithGenre({title}{genre})...");
            if (String.IsNullOrWhiteSpace(title))
                return Ok(context.Movie.Where(a=>a.type.Contains(genre)));

            if (genre == "All")
                return Ok(context.Movie.Where(a => a.name.Trim().ToLower().Contains(title.ToLower())));

            return Ok(context.Movie.Where(a=>a.type.Contains(genre) && a.name.Trim().ToLower().Contains(title.ToLower())));
        }

        [HttpGet("movies/{genre}/")]
        public ActionResult<List<Movie>> GetMovieWithGenre(string genre)
        {
            Console.WriteLine($"Recieved request for: GetMovieWithGenre({genre})...");
            return Ok(context.Movie.Where(a => a.type == genre));
        }

        [HttpGet("clients/{id}")]
        public ActionResult<Client> GetClientWithId(int id)
        {
            Console.WriteLine($"Recieved request for: GetClientWithId({id})");
            Client suspect = context.Client.FirstOrDefault(a => a.client_id == id);
            if (suspect!=null)
            {
                Console.WriteLine("Client Found!");
                return Ok(suspect);
            }

            Console.WriteLine("Client NOT Found!");
            return NotFound($"Client {id} not found...");
            

        }


        [HttpGet("clients/{firstname}/{lastname}")]
        public ActionResult<Client> GetClientWithName(string firstname, string lastname)
        {
            Console.WriteLine($"Recieved request for: GetClientWithName({firstname},{lastname})");
            Client suspect = context.Client.FirstOrDefault(a => a.firstname == firstname && a.lastname == lastname);

            if (suspect != null)
            {
                Console.WriteLine("Client Found!");
                return Ok(suspect);
            }

            Console.WriteLine("Client NOT Found!");
            return NotFound($"Client {firstname} {lastname} not found...");
            

        }

        [HttpPatch("clients/{clientId}")]
        public async Task<ActionResult> PatchClient([FromBody] Client meinNeuerClient, int clientId)
        {
            Console.WriteLine($"Recieved Request for: PatchClient({clientId})");
            Client clientToPatch = context.Client.FirstOrDefault(a=>a.client_id == clientId);

            if (clientToPatch == null)
            {
                return NotFound($"Client with Id {clientId} not found!");

            }

            clientToPatch.client_id = clientId;
            clientToPatch.firstname = meinNeuerClient.firstname;
            clientToPatch.lastname = meinNeuerClient.lastname;
            clientToPatch.address = meinNeuerClient.address;
            clientToPatch.Order = meinNeuerClient.Order;
            clientToPatch.dateofbirth = meinNeuerClient.dateofbirth;
            clientToPatch.postalcode = meinNeuerClient.postalcode;

            await context.SaveChangesAsync();
            return Ok("Sucessfully Patched Client!");

        }


        [HttpPost("clients/{clientid}/orders")]
        public async Task<ActionResult> PostNewOrder([FromBody] Order myNewOrder, int clientId)
        {
            var clientXorder = context.Client.Where(a=> a.client_id.Equals(clientId)).FirstOrDefault();
            Console.WriteLine($"Recieved request for:  PostNewOrder for ClientId {clientId}");
            if (clientXorder == null)
            {
                await context.SaveChangesAsync();
                return NotFound($"Client with Id {clientId} not found");
            }

            clientXorder.Order.Add(myNewOrder);
            return Ok("Sucessfully Added Order!");  
            

        }


        [HttpPost("clients")]
        public async Task<ActionResult<int>> PostNewClient([FromBody] Client meinNeuerClient)
        {
            Console.WriteLine($"Recieved Request for: PostNewClient");
            var exists = context.Client.Where(a => a.firstname == meinNeuerClient.firstname).FirstOrDefault();
            if (exists != default)
            {
                return NotFound("Nutzer mit diesem Vornamen existiert schon.");
            }
            
            meinNeuerClient.client_id = (context.Client.Select(a => a.client_id).Max()+1);
            context.Client.Add(meinNeuerClient);
            await context.SaveChangesAsync();
            Console.WriteLine("Successfully posted new client!");
            return Ok(context.Client.Where(a=>a.firstname ==meinNeuerClient.firstname && a.lastname==meinNeuerClient.lastname).Select(a=>a.client_id).FirstOrDefault());
        }

        
        [HttpDelete("clients/{id}")]
        public async Task<ActionResult> DeleteClient(int id)
        {
            Client suspect = context.Client.FirstOrDefault(a => a.client_id == id);

            if (suspect != default)
            {
                context.Client.Remove(suspect);
                await context.SaveChangesAsync();
                return Ok("Sucessfully removed Client (he was removed on your behalf... he is gone now... no coming back... have you thought about his family?... why would you do that to them?...)");

            }

            return NotFound("Client with that id doesnt exist");

        }
    }
}
