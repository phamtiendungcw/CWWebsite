using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Hãy nhập username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Hãy nhập password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Hãy nhập email")]
        public string Email { get; set; }
        public string ImagePath { get; set; }
        [Required(ErrorMessage = "Hãy nhập name")]
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        [Display(Name = "User Image")]
        public HttpPostedFileBase UserImage { get; set; }
    }
}
