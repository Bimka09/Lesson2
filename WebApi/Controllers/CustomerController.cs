using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Database;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("clients")]
    public class CustomerController : Controller
    {
        private readonly ApplicationContext _context;
        public CustomerController(ApplicationContext context)
        {
            this._context = context;
        }
        [HttpGet("{id:long}")]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        [ProducesResponseType (StatusCodes.Status200OK)]
        public ActionResult<Client> GetCustomerAsync([FromRoute] long id)
        {
            //using var db = new ApplicationContext();

            var result = _context.clients.FirstOrDefault(c => c.id == id);
            if(result == null)
            {
                return NotFound();
            }
            else
            {
                return result;
            }
        }

        [HttpPost("")]
        [ProducesResponseType (StatusCodes.Status200OK)]
        [ProducesResponseType (StatusCodes.Status409Conflict)]
        public ActionResult<Client> CreateCustomerAsync([FromBody] Client client)
        {
            //using var db = new ApplicationContext();

            var result = _context.clients.FirstOrDefault(c => c.id == client.id);

            if(result == null)
            {
                var newCustomer = _context.clients.Add(new Client
                {
                    id = client.id,
                    first_name = client.first_name,
                    last_name = client.last_name,
                    middle_name = client.middle_name,
                    email = client.email
                }
                );

                _context.SaveChanges();

                return newCustomer.Entity;
            }
            else
            {
                return StatusCode(409);
            }
        }

    }
}