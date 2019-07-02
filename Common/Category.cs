using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Category: IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public CategoryReferences Articles { get; set; }
    }
}
