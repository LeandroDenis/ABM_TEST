using BusinessObject;
using System.Data.Entity;

namespace Proyecto_ABM.BusinessObject
{
    public class EmpleadosDbContext : DbContext
    {
        public EmpleadosDbContext() : base ("ABM")
        {
        }

        public DbSet<EmpleadoBO> Empleados { get; set; }
    }
}