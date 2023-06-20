using System.ComponentModel.DataAnnotations;

namespace WebAppCommandProject.Model.ProjectDTO
{
    public class CreateDishDto
    {
        [Required(ErrorMessage = "Name not set")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description not set")]
        public string Description { get; set; }

        [Required(ErrorMessage = "TimePrepare not set")]
        public uint TimePrepare { get; set; }

        [Required(ErrorMessage = "Size not set")]
        public int Size { get; set; }

        [Required(ErrorMessage = "Dough not set")]
        public int Dough { get; set; }

        [Required(ErrorMessage = "Price not set")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "IngrDict not set")]
        public Dictionary<int, uint> IngrDict { get; set; }
    }
}
