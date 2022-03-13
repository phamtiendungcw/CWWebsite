using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class VideoDTO
    {
        public int ID { get; set; }
        public string VideoPath { get; set; }
        [Required(ErrorMessage ="Hãy nhập mục video path")]
        public string OriginalVideoPath { get; set; }
        [Required(ErrorMessage ="Hãy nhập mục title")]
        public string Title { get; set; }
        public DateTime AddDate { get; set; }
    }
}
