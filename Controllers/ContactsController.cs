using ContactsAPI.Data;
using ContactsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ContactsAPI.Controllers
{
    // telling this is API Controller 
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        // create db context to 
        private readonly ContactsAPIDbContext dbContext;

        //inject db context
        public ContactsController(ContactsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        // this method for add contacts 
        [HttpPost]
        [Route("AddContact")]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            // create a new contact object
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = addContactRequest.Address,
                Email = addContactRequest.Email,
                FullName = addContactRequest.FullName,
                Phone = addContactRequest.Phone
            };

            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                //update the full properties
                contact.FullName = updateContactRequest.FullName;
                contact.Address = updateContactRequest.Address;
                contact.Phone = updateContactRequest.Phone;
                contact.Email = updateContactRequest.Email;

                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);

            if (contact != null)
            {
                dbContext.Contacts.Remove(contact);
                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct(Product addProduct)
        {
            // create a new contact object
            var product = new Product()
            {
                Name = addProduct.Name,
                price = addProduct.price,
            };

            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();

            return Ok(product);
        }
    }
}
