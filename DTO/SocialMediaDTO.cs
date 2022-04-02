using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DTO
{
    public class SocialMediaDTO
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Hãy nhập mục name!")]
        public string Name { get; set; }
        public string ImagePath { get; set; }
        [Required(ErrorMessage = "Hãy nhập mục link!")]
        public string Link { get; set; }
        [Display(Name = "Image")]
        public HttpPostedFileBase SocialImage { get; set; }
    }
}
