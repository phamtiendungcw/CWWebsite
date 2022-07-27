using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AddressDTO
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Hãy nhập mục address!")]
        public string AddressContent { get; set; }
        [Required(ErrorMessage = "Hãy nhập mục email!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Hãy nhập mục phone!")]
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        [Required(ErrorMessage = "Hãy nhập mục large map!")]
        public string LargeMapPath { get; set; }
        [Required(ErrorMessage = "Hãy nhập mục small map!")]
        public string SmallMapPath { get; set; }
    }
}
