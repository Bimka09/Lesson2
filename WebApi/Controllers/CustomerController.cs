using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Database;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("customers")]
    public class CustomerController : Controller
    {
        [HttpGet("{id:long}")]
        public Client GetCustomerAsync([FromRoute] long id)
        {
            using var db = new ApplicationContext();

            return db.clients.First(c => c.id == id);
        }

        [HttpPost("")]
        public long CreateCustomerAsync([FromBody] Client client)
        {
            using var db = new ApplicationContext();

            var newCustomer = db.clients.Add(new Client
            { 
                first_name = client.first_name,
                last_name = client.last_name,
                middle_name = client.middle_name,
                email = client.email}
            );

            db.SaveChanges();

            return newCustomer.Entity.id;
        }
    }
}