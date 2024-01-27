﻿using System.ComponentModel.DataAnnotations;

namespace EduHome.ViewModels.Account
{
    public class LoginVM
    {
        [Required]
        public string UserNameOrEmail { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
