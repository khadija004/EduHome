using Domain;
using Service.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Service.BaseModels
{
    public class BackgroundImagesService : IBackgroundImagesService
    {
        private readonly AppDbContext _context;
        public BackgroundImagesService(AppDbContext context)
        {
            _context = context;
        }
        public Dictionary<string, string> GetBackgroundImages()
        {
            Dictionary<string, string> images = _context.BackgroundImages.AsEnumerable().ToDictionary(s => s.Key, s => s.Value);
            return images;
        }
    }
}
