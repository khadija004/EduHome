using System.ComponentModel.DataAnnotations;

namespace EduHome.ViewModels.ViewComponentViewModels
{
    public class SubscribeVM
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
