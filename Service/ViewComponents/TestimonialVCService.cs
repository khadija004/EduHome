using Domain;
using Domain.Entities.ViewComponentModel;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.ViewComponents
{
    public class TestimonialVCService : ITestimonialVCService
    {
        private readonly AppDbContext _context;
        public TestimonialVCService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<TestimonialVC>> GetTestimonials()
        {
            try
            {
                var testimonials = await _context.TestimonialVC.Where(n => !n.IsDeleted).ToListAsync();
                return testimonials;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
