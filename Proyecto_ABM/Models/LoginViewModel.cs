using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Proyecto_ABM.WebSite.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Usuario Requerido")]
        [AllowHtml]
        public string Username { get; set; }

        [Required(ErrorMessage = "Contraseña Requerida")]
        [AllowHtml]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}