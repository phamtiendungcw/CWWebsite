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
    }
}
