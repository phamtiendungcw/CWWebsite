using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DTO
{
    public class AdsDTO
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Hãy nhập tên")]
        public string Name { get; set; }
        public string ImagePath { get; set; }
        [Required(ErrorMessage = "Hãy nhập link")]
        public string Link { get; set; }
        [Required(ErrorMessage = "Hãy nhập size ảnh")]
        public string ImageSize { get; set; }
        [Display(Name = "Ads Image")]
        public HttpPostedFileBase AdsImage { get; set; }
    }
}
