using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Statics
{
    public class StaticProps : IStaticProps
    {
        private readonly AppDbContext _context;
        public StaticProps(AppDbContext context)
        {
            _context = context;
        }

        private static ICollection<int> Ids = null;

        public async Task<ICollection<int>> GetIds()
        {
            if (Ids == null)
                Ids = await _context.Teachers.OrderByDescending(e => e.Id).Select(e => e.Id).ToListAsync();
            return Ids;
        }

    }
}
