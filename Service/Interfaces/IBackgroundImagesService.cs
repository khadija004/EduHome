using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IBackgroundImagesService
    {
        Dictionary<string, string> GetBackgroundImages();
    }
}
