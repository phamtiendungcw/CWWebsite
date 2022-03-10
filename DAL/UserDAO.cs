using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDAO : PostContext
    {
        public UserDTO GetUserWithUsernameAndPassword(UserDTO model)
        {
            UserDTO dto = new UserDTO();
            T_User user = db.T_User.FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);
            if (user != null && user.ID != 0)
            {
                dto.ID = user.ID;
                dto.Username = user.Username;
                dto.Name = user.NameSurname;
                dto.ImagePath = user.ImagePath;
                dto.IsAdmin = user.isAdmin;
            }
            return dto;
        }

        public int AddUser(T_User user)
        {
            try
            {
                db.T_User.Add(user);
                db.SaveChanges();
                return user.ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<UserDTO> GetUsers()
        {
            List<T_User> list = db.T_User.Where(x => x.isAdmin == false).OrderBy(x => x.AddDate).ToList();
            List<UserDTO> userList = new List<UserDTO>();
            foreach (var item in list)
            {
                UserDTO dto = new UserDTO();
                dto.ID = item.ID;
                dto.Name = item.NameSurname;
                dto.Username = item.Username;
                dto.ImagePath = item.ImagePath;
                userList.Add(dto);
            }
            return userList;
        }

        public string UpdateUser(UserDTO model)
        {
            try
            {
                T_User user = db.T_User.First(x => x.ID == model.ID);
                string oldImagePath = user.ImagePath;
                user.NameSurname = model.Name;
                user.Username = model.Username;
                if (model.ImagePath != null)
                    user.ImagePath = model.ImagePath;
                user.Email = model.Email;
                user.Password = model.Password;
                user.LastUpdateDate = DateTime.Now;
                user.LastUpdateUserID = UserStatic.UserID;
                user.isAdmin = model.IsAdmin;
                db.SaveChanges();
                return oldImagePath;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public UserDTO GetUserWithID(int id)
        {
            T_User user = db.T_User.First(x => x.ID == id);
            UserDTO dto = new UserDTO();
            dto.ID = user.ID;
            dto.Name = user.NameSurname;
            dto.Username = user.Username;
            dto.Password = user.Password;
            dto.IsAdmin = user.isAdmin;
            dto.Email = user.Email;
            dto.ImagePath = user.ImagePath;
            return dto;
        }
    }
}
