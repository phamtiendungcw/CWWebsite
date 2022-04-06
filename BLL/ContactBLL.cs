using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class ContactBLL
    {
        private ContactDAO dao = new ContactDAO();
        public bool AddContact(GeneralDTO model)
        {
            Contact contact = new Contact();
            contact.Subject = model.Subject;
            contact.NameSurname = model.Name;
            contact.Email = model.Email;
            contact.Message = model.Message;
            contact.AddDate = DateTime.Now;
            contact.LastUpdateDate = DateTime.Now;
            dao.AddContact(contact);
            return true;
        }
    }
}
