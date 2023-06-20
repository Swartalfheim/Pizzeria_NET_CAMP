using System.ComponentModel.DataAnnotations;

namespace WebAppCommandProject.Model.ProjectDTO
{
    public class ChefDto
    {
        [Required(ErrorMessage = "Name not set")]
        public string Name { get; set; }

        [Required(ErrorMessage = "CategoryId not set")]
        public int[] CategoryId { get; set; }
    }
}
