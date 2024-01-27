using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface ISettingService
    {
        Dictionary<string, string> GetSettings();
    }
}
