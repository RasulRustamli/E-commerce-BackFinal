using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_BackFinal.ViewModels
{
    public class RegisterVm
    {
        [Required, StringLength(40)]
        public string UserName { get; set; }
        [Required, StringLength(27)]
        public string FirstName { get; set; }
        [Required, StringLength(27)]
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        [Required, StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        public bool Subscribe { get; set; }
    }
}
