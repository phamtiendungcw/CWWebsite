using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nhập username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Nhập password")]
        public string Password { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
    }
}
