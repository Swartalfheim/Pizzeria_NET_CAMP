using System.ComponentModel.DataAnnotations;

namespace WebAppCommandProject.Model.ProjectDTO
{
    public class ClientFromAdminDto
    {
        [Required(ErrorMessage = "Name not set")]
        public string Name { get; set; }

        [Required(ErrorMessage = "PaymentCategory not set")]
        public int PaymentCategory { get; set; }

        [Required(ErrorMessage = "VipLevel not set")]
        public int VipLevel { get; set; }

        [Required(ErrorMessage = "Amount not set")]
        public decimal Amount { get; set; }
    }
}
