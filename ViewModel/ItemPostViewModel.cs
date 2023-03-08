using System.ComponentModel.DataAnnotations;

namespace MalmöTradera.api.ViewModel
{
    public class ItemPostViewModel
    {
         [Required(ErrorMessage = "namn måste anges")]
        public string Name { get; set; }
         [Required(ErrorMessage = "pris måste anges")]
        public int value { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}