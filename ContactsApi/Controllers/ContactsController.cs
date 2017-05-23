using ContactsApi.Models;
using ContactsApi.Repository;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;

namespace ContactsApi.Controllers
{
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        public IContactsRepository ContactsRepo { get; set; }

        public ContactsController(IContactsRepository _repo)
        {
            ContactsRepo = _repo;
        }

        [HttpGet]
        public IEnumerable<Contacts> GetAll()
        {
            return ContactsRepo.GetAll();
        }

        [HttpGet("{id}", Name = "GetContacts")]
        public IActionResult GetById(long id)
        {
            var item = ContactsRepo.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Contacts item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            ContactsRepo.Add(item);
            return Json(new { message = "Contato adicionado com sucesso!" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Contacts item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var contactObj = ContactsRepo.Find(id);
            if (contactObj == null)
            {
                return NotFound();
            }

            contactObj.FirstName = item.FirstName;
            contactObj.LastName = item.LastName;
            contactObj.Company = item.Company;
            contactObj.JobTitle = item.JobTitle;
            contactObj.Email = item.Email;
            contactObj.MobilePhone = item.MobilePhone;
            contactObj.DateOfBirth = item.DateOfBirth;

            ContactsRepo.Update(contactObj);
            return Json(new { message = "Contato atualizado com sucesso!" });            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            ContactsRepo.Remove(id);
            return Json(new { message = "Contato excluído com sucesso!"});
        }
    }
}