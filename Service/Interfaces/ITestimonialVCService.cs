using Domain.Entities.ViewComponentModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ITestimonialVCService
    {
        Task<List<TestimonialVC>> GetTestimonials();
    }
}
