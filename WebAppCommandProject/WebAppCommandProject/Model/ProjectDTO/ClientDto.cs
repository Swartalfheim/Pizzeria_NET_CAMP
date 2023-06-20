using System.ComponentModel.DataAnnotations;

namespace WebAppCommandProject.Model.ProjectDTO
{
    public class ClientDto
    {
        [Required(ErrorMessage = "Name not set")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Amount not set")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "PaymentCategory not set")]
        public int PaymentCategory { get; set; }

        [Required(ErrorMessage = "DishesId not set")]
        public int[] DishesId { get; set; }
    }
}
