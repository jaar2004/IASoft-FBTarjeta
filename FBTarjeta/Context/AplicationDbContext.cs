// IAR sab 11NOV2023

using FBTarjeta.Models;
using Microsoft.EntityFrameworkCore;

namespace FBTarjeta.Context
{
  public class AplicationDbContext: DbContext
  {

    public DbSet<Tarjeta> Tarjetas { get; set; }

    // Constructor
    public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
    {
    }

    // IAR sab 16DIC2023
    /*
     Tenia problema al tratar de acceder a los datos, marcaba el siguiente error
     nombre de objeto 'Dbset' no valido
     No estaba identificando el nombre del dbset, como no hice migración, sino que cree la tabla en
     la base de datos.
     Agregué este método para mapear el modelo de entidad con la tabla de la base de datos
     Texto de búsqueda: nombre de objeto 'Dbset' no valido y luego use dbset object name is not valid

     link de consulta
     Invalid Object Name Error - EntityFrameworkCore 2.0
     https://stackoverflow.com/questions/48267925/invalid-object-name-error-entityframeworkcore-2-0

      Modifiqué tabla tarjeta en columna CVV de CHAR(3) a VARCHAR(3)
      texto búsqueda alter table sql server 2014

      ALTER TABLE (Transact-SQL)
      https://learn.microsoft.com/en-us/sql/t-sql/statements/alter-table-transact-sql?view=sql-server-ver16

      ALTER TABLE dbo.Tarjeta ALTER COLUMN CVV VARCHAR(3)
    */
    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Tarjeta>().ToTable("Tarjeta");
    }

  }
}
