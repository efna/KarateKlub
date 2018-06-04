using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateKlub.Data.Helper
{
    public interface IEntity
    {
        int Id { get; set; }
        bool isDeleted { get; set; }
    }
}
