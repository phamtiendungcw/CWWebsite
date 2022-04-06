using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class ContactDAO : PostContext
    {
        public void AddContact(Contact contact)
        {
            try
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<ContactDTO> GetUnreadMessages()
        {
            List<ContactDTO> dtoList = new List<ContactDTO>();
            List<Contact> list = db.Contacts.Where(x => x.isDeleted == false && x.isRead == false).OrderByDescending(x => x.AddDate).ToList();
            foreach (Contact item in list)
            {
                ContactDTO dto = new ContactDTO();
                dto.ID = item.ID;
                dto.Subject = item.Subject;
                dto.Name = item.NameSurname;
                dto.Email = item.Email;
                dto.Message = item.Message;
                dto.AddDate = item.AddDate;
                dto.isRead = item.isRead;
                dtoList.Add(dto);
            }
            return dtoList;
        }

        public void ReadMessage(int ID)
        {
            try
            {
                Contact contact = db.Contacts.First(x => x.ID == ID);
                contact.isRead = true;
                contact.ReadUserID = UserStatic.UserID;
                contact.LastUpdateDate = DateTime.Now;
                contact.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void DeleteMessage(int ID)
        {
            Contact contact = db.Contacts.First(x => x.ID == ID);
            contact.isDeleted = true;
            contact.DeletedDate = DateTime.Now;
            contact.LastUpdateDate = DateTime.Now;
            contact.LastUpdateUserID = UserStatic.UserID;
            db.SaveChanges();
        }

        public List<ContactDTO> GetAllMessages()
        {
            List<ContactDTO> dtoList = new List<ContactDTO>();
            List<Contact> list = db.Contacts.Where(x => x.isDeleted == false).OrderByDescending(x => x.AddDate).ToList();
            foreach (Contact item in list)
            {
                ContactDTO dto = new ContactDTO();
                dto.ID = item.ID;
                dto.Subject = item.Subject;
                dto.Name = item.NameSurname;
                dto.Email = item.Email;
                dto.Message = item.Message;
                dto.AddDate = item.AddDate;
                dto.isRead = item.isRead;
                dtoList.Add(dto);
            }
            return dtoList;
        }
    }
}
