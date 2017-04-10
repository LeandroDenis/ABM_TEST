using System.ComponentModel.DataAnnotations;

namespace Proyecto_ABM.WebSite.Models
{
    public class Empleado
    {
        public int Id { get; set; }
        [UIHint("Integer")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Strings))]
        public int Usuario { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Strings))]
        [StringLength(50, ErrorMessageResourceName = "LengthError", ErrorMessageResourceType = typeof(Resources.Strings), MinimumLength = 5)]
        public string Nombre { get; set; }
        [Range(18, 100, ErrorMessageResourceName = "RangeError", ErrorMessageResourceType = typeof(Resources.Strings))]
        public int Edad { get; set; }
        public int Telefono { get; set; }
        [StringLength(200)]
        public string Observaciones { get; set; }
    }
}