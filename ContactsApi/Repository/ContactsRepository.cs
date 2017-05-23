using System.Collections.Generic;
using System.Linq;
using ContactsApi.Models;
using ContactsApi.Contexts;

namespace ContactsApi.Repository
{
    public class ContactsRepository : IContactsRepository
    {
        static List<Contacts> ContactsList = new List<Contacts>();

        ContactsContext _context;

        public ContactsRepository(ContactsContext context)
        {
            _context = context;
        }

        public void Add(Contacts item)
        {
            _context.Contacts.Add(item);
            _context.SaveChanges();
        }

        public Contacts Find(long key)
        {
            return _context.Contacts.Find(key);
        }

        public IEnumerable<Contacts> GetAll()
        {
            return _context.Contacts.ToList();
        }

        public void Remove(long Id)
        {
            var itemToRemove = this.Find(Id);
            if (itemToRemove != null)
            {
                _context.Contacts.Remove(itemToRemove);
                _context.SaveChanges();
            }
        }

        public void Update(Contacts item)
        {
            _context.Contacts.Update(item);
            _context.SaveChanges();
        }
    }
}