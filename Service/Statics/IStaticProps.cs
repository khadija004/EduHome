using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Statics
{
    public interface IStaticProps
    {
        Task<ICollection<int>> GetIds();
    }
}
