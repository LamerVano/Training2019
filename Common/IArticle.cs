using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IArticle
    {
        int Id { get; set; }
        string Name { get; set; }
        DateTime Date { get; set; }
        string Language { get; set; }
        string Picture { get; set; }
        string Video { get; set; }
    }
}
