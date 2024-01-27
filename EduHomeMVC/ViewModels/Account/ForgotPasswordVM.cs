using System.ComponentModel.DataAnnotations;

namespace EduHome.ViewModels.Account
{
    public class ForgotPasswordVM
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
