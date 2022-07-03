using System;
using Microsoft.AspNetCore.Mvc;
using ContactList.Data;
using Microsoft.EntityFrameworkCore;
using ContactList.ViewModels;
using ContactList.Models;

namespace ContactList.Contrellers
{
    [ApiController]
    [Route("v1")]
    public class ContactListController : ControllerBase
    {
        [HttpGet]
        [Route("contactList")]
        public async Task<IActionResult> GetAsync(
            [FromServices] AppDbContext context)
        {
            var users = await context.Users
                .Include(x => x.Contacts)
                    .ThenInclude(x => x.Phone)
                .Include(x => x.Contacts)
                    .ThenInclude(x => x.Email)
                .Include(x => x.Contacts)
                    .ThenInclude(x => x.Whatsapp)
                .AsNoTracking().ToListAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("contactList/{id}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] AppDbContext context, [FromRoute] int id)
        {
            var user = await context.Users
                .Include(x => x.Contacts)
                    .ThenInclude(x => x.Phone)
                .Include(x => x.Contacts)
                    .ThenInclude(x => x.Email)
                .Include(x => x.Contacts)
                    .ThenInclude(x => x.Whatsapp)
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        [Route("contactList")]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext context, [FromBody] CreateContactListViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = new User
            {
                Name = model.Name,
                Contacts = model.Contacts.Select(x => new Models.Contacts
                {
                    Email = x.Email.Select(x => new Models.Email
                    {
                        EmailText = x.EmailText
                    }).ToList(),
                    Phone = x.Phone.Select(x => new Models.Phone
                    {
                        PhoneNumber = x.PhoneNumber
                    }).ToList(),
                    Whatsapp = x.Whatsapp.Select(x => new Models.Whatsapp
                    {
                        WhatsappText = x.WhatsappText
                    }).ToList()
                }).ToList()
            };

            try
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
                return Created($"v1/contactList/{user.Id}", user);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut]
        [Route("contactList/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] AppDbContext context, [FromBody] CreateContactListViewModel model, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await context.Users
                .Include(x => x.Contacts)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return NotFound();

            try
            {
                user.Name = model.Name;
                user.Contacts = model.Contacts.Select(x => new Models.Contacts
                {
                    Email = x.Email.Select(x => new Models.Email
                    {
                        EmailText = x.EmailText
                    }).ToList(),
                    Phone = x.Phone.Select(x => new Models.Phone
                    {
                        PhoneNumber = x.PhoneNumber
                    }).ToList(),
                    Whatsapp = x.Whatsapp.Select(x => new Models.Whatsapp
                    {
                        WhatsappText = x.WhatsappText
                    }).ToList()
                }).ToList();

                context.Users.Update(user);
                await context.SaveChangesAsync();
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete]
        [Route("contactList/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] AppDbContext context, [FromRoute] int id)
        {
            var user = await context.Users
                .Include(x => x.Contacts)
                    .ThenInclude(x => x.Email)
                .Include(x => x.Contacts)
                    .ThenInclude(x => x.Phone)
                .Include(x => x.Contacts)
                    .ThenInclude(x => x.Whatsapp)
                .FirstOrDefaultAsync(x => x.Id == id);

            try
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete]
        [Route("contactList/phone/{id}")]
        public async Task<IActionResult> DeletePhoneAsync(
            [FromServices] AppDbContext context, [FromRoute] int id)
        {
            var phone = await context.Phone
                .FirstOrDefaultAsync(x => x.Id == id);

            try
            {
                context.Phone.Remove(phone);
                await context.SaveChangesAsync();
                return Ok(phone);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete]
        [Route("contactList/email/{id}")]
        public async Task<IActionResult> DeleteEmailAsync(
            [FromServices] AppDbContext context, [FromRoute] int id)
        {
            var email = await context.Email
                .FirstOrDefaultAsync(x => x.Id == id);

            try
            {
                context.Email.Remove(email);
                await context.SaveChangesAsync();
                return Ok(email);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete]
        [Route("contactList/whatsapp/{id}")]
        public async Task<IActionResult> DeleteWhatsappAsync(
            [FromServices] AppDbContext context, [FromRoute] int id)
        {
            var whatsapp = await context.Whatsapp
                .FirstOrDefaultAsync(x => x.Id == id);

            try
            {
                context.Whatsapp.Remove(whatsapp);
                await context.SaveChangesAsync();
                return Ok(whatsapp);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
