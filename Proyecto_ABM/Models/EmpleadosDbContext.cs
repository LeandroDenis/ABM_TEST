using System.Data.Entity;

namespace Proyecto_ABM.WebSite.Models
{
    public class EmpleadosDbContext : DbContext
    {
        public EmpleadosDbContext() : base ("ABM")
        {
        }

        public DbSet<Empleado> Empleados { get; set; }
    }
}