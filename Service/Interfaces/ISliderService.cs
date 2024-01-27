using Domain.Entities.HomeModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ISliderService
    {
        Task<List<Slider>> GetSliders();
    }
}
