// IAR dom 29OCT2023

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

// de este link saque como cambiar MetadataType. IAR dom 29OCT2023
// https://www.appsloveworld.com/csharp/100/25/asp-net-core-metadatatype-attribute-not-working
namespace FBTarjeta.Models
{
  //[MetadataType(typeof(TarjetaMetadata))] // NO funcionaba palabra Metadatatype. IAR dom 29OCT2023
  //[Microsoft.AspNetCore.Mvc.ModelMetadataType(typeof(TarjetaMetadata))]
  [ModelMetadataType(typeof(TarjetaMetadata))] // IAR dom 29OCT2023
  public partial class Tarjeta
  {}

  public class TarjetaMetadata
  {
    [Required]
    public string Titulo;

    [Required]
    public string NumeroTarjeta;

    [Required]
    public string FechaExpiracion;

    [Required]
    public string CVV;
  }

}
