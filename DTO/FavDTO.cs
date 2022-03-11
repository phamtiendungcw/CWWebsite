using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DTO
{
    public class FavDTO
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Hãy nhập mục title")]
        public string Title { get; set; }
        public string Fav { get; set; }
        public string Logo { get; set; }
        [Display(Name = "Logo image")]
        public HttpPostedFileBase LogoImage { get; set; }
        [Display(Name = "Fav image")]
        public HttpPostedFileBase FavImage { get; set; }
    }
}
