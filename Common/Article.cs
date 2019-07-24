using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Article : IEntity
    {
        public int Id {
            get { return id; }
            set
            {
                id = value;
                if(ArticleRefs != null)
                    ArticleRefs.Id = value;
                if(CategoryRefs != null)
                    CategoryRefs.Id = value;
            }
        }
        private int id;
        [StringLength(128)]
        public string UserId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public DateTime Date { get; set; }
        [Required]
        [StringLength(15)]
        public string Language { get; set; }
        [StringLength(256)]
        public string Picture { get; set; }
        [Required]
        [StringLength(256)]
        public string Video { get; set; }
        public ArticleReferences ArticleRefs { get; set; }
        public CategoryReferences CategoryRefs { get; set; }
    }
}
