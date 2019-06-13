using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Article
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [StringLength(15)]
        public string Language { get; set; }
        [StringLength(256)]
        public string Picture { get; set; }
        [Required]
        [StringLength(256)]
        public string Video { get; set; }
    }
}
