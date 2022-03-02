using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UserStatic
    {
        public static int UserID { get; set; } = 1;
        public static bool IsAdmin { get; set; }
        public static string NameSurname { get; set; }
        public static string ImagePath { get; set; }
    }
}
