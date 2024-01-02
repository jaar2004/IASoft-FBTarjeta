// IAR dom 29OCT2023

namespace FBTarjeta.Models
{
  public partial class Tarjeta
  {
    public int ID { get; set; }
    public string Titulo { get; set; }
    public string NumeroTarjeta { get; set; }
    public string FechaExpiracion { get; set; }
    public string CVV { get; set; }

  }
}
