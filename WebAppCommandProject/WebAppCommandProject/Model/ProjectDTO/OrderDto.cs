using System.ComponentModel.DataAnnotations;

namespace WebAppCommandProject.Model.ProjectDTO
{
    public class OrderDto
    {
        [Required(ErrorMessage = "CasReg: not set")]
        public int CasReg { get; set; }
        [Required(ErrorMessage = "Name not set")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Amount not set")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Payment not set")]
        public int Payment { get; set; }

        [Required(ErrorMessage = "DishDict not set")]
        public Dictionary<int, int> DishDict { get; set; }
    }
}
