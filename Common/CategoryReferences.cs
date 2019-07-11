using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CategoryReferences: IEntity
    {
        public int Id { get; set; }
        public List<Category> Refs { get; set; }
    }
}
