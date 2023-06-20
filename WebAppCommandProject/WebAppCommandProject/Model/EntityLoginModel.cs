using System.ComponentModel.DataAnnotations;

namespace WebAppCommandProject.Models
{
    public class EntityLoginModel
    {
        [Required(ErrorMessage = "Email not set")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password not set")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
