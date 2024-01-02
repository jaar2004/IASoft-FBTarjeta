// IAR dom 29OCT2023

using FBTarjeta.Context;
using FBTarjeta.Models;
using Microsoft.AspNetCore.Http; // Para habilitar el decorado "ProducesResponseType". IAR vie 29DIC2023
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

/*
  IAR jue 28DIC2023
  Texto búsqueda: difference between controllerbase and apicontroller
  The Controller class is used for creating controllers in ASP.NET MVC applications, the ControllerBase class
  is used for basic functionality in ASP.NET Core, and the APIController class is used for creating controllers
  in ASP.NET Core Web API applications.

  Understand between Controller, ControllerBase, and APIController Classes in .NET
  https://medium.com/@susithapb/understanding-between-controller-controllerbase-and-apicontroller-classes-in-net-74a53df55d50#:~:text=The%20Controller%20class%20is%20used,NET%20Core%20Web%20API%20applications.

  Controller Class
  The Controller class is a part of the System.Web.Mvc namespace and is used to create controllers for ASP.NET
  MVC applications. It is the base class for all controllers in MVC and provides a wide range of features and
  functionalities such as action filters, model binding, and view rendering.

  ControllerBase Class
  The ControllerBase class is a base class for controllers in ASP.NET Core. It provides basic functionality
  for handling HTTP requests and responses, but does not include any MVC-specific features such as action
  filters or view rendering.

  APIController Class
  The APIController class is also a part of the Microsoft.AspNetCore.Mvc namespace and is used to create
  controllers for ASP.NET Core Web API applications. It is a specialized version of the ControllerBase class
  and includes features specific to Web API development such as content negotiation and routing.

*/
namespace FBTarjeta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarjetaController : ControllerBase
    {

      //private readonly AplicationDbContext _context;
      private readonly AplicationDbContext _context;

      public TarjetaController(AplicationDbContext context)
      {
          _context = context;
      }

      //[HttpGet]
      [HttpGet("[action]")] // Sab 16DIC2023
      //public async Task<IActionResult> Get()
      public async Task<IActionResult> Listar()
      {
          try
          {
              var listTarjetas = await _context.Tarjetas.ToListAsync();
              //List<Tarjeta> listTarjetas = await _context.Tarjetas.ToListAsync();
              //var tarjeta = await _context.Tarjetas.FirstOrDefaultAsync(c => c.ID == id);

          if (listTarjetas is null)
          {
              return NotFound("No existe información de tarjetas.");
          }

          return Ok(listTarjetas);
          
          }
          catch (Exception ex)
          {
              return BadRequest(ex.Message);
          }

      }

      //[HttpGet]
      [HttpGet("[action]/{id}")] // Sab 16DIC2023
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Tarjeta))] // IAR vie 29DIC2023
    [ProducesResponseType(StatusCodes.Status404NotFound)] // IAR vie 29DIC2023
    [ProducesResponseType(StatusCodes.Status400BadRequest)] // IAR vie 29DIC2023
    //public async Task<IActionResult> Get(int id)
    public async Task<IActionResult> Mostrar(int id)
      {
        try
        {

          if (id <= 0)
          {
            return BadRequest("Id de tarjeta debe ser mayor que cero.");
          }

          //var listTarjetas = await _context.Tarjetas.ToListAsync();
          //List<Tarjeta> listTarjetas = await _context.Tarjetas.ToListAsync();
          Tarjeta tarjeta = await _context.Tarjetas.FirstOrDefaultAsync(c => c.ID == id);

          if (tarjeta is null)
          {
            return NotFound("Tarjeta no encontrada.");
          }

          return Ok(tarjeta);

        }
        catch (Exception ex)
        {
          return BadRequest(ex.Message);
        }

      }

      /*
      Se envia en este formato
      {
          "Titulo": "lalo",
          "NumeroTarjeta": "2148752463259651",
          "FechaExpiracion": "10/25",
          "CVV": "254"
      }

      */
      //[HttpPost]
      [HttpPost("[action]")] // IAR 17DIC2023
      [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CrearViewModel))] // IAR vie 29DIC2023
      //[ProducesResponseType(StatusCodes.Status404NotFound)] // IAR vie 29DIC2023
      [ProducesResponseType(StatusCodes.Status400BadRequest)] // IAR vie 29DIC2023

      //public async Task<IActionResult> Post([FromBody] Tarjeta tarjeta)
      //public async Task<IActionResult> Crear([FromBody] Tarjeta tarjeta) // IAR 17DIC2023
      public async Task<IActionResult> Crear([FromBody] CrearViewModel model) // IAR 17DIC2023
      {
          //try
          //{

          //if (tarjeta is null)
          //{
          //    return BadRequest("No hay información de tarjeta para grabar.");
          //}

          //_context.Tarjetas.Add(tarjeta); // Dom 17DIC2023
          ////_context.Add(tarjeta);
          //await _context.SaveChangesAsync();

          //return Ok(tarjeta);

          //catch (Exception ex)
          //{
          //  return BadRequest(ex.Message);
          //}
          if (!ModelState.IsValid)
          {
              // Validar si los campos son nulos ya que en el ModelState al venir completo el objeto
              // y con valores nulos lo da por true, revisar este detalle. IAR mar 26DIC2023
              return BadRequest(ModelState);
          }

          Tarjeta tarjeta = new Tarjeta
          {
            Titulo = model.Titulo,
            NumeroTarjeta = model.NumeroTarjeta,
            FechaExpiracion = model.FechaExpiracion,
            CVV = model.CVV
          };

          _context.Tarjetas.Add(tarjeta);

          try
          {
              await _context.SaveChangesAsync();

          }
          catch (Exception ex)
          {
              return BadRequest();
          }

          return Ok(new { message = "La tarjeta fue creada con éxito" }); // IAR mar 26DIC2023

    }

      // IAR jue 21DIC2023
      [HttpPut("{id}")]
      public async Task<IActionResult> Put(int id, [FromBody] Tarjeta tarjeta)
      {
        try
        {

          if(id != tarjeta.ID)
          {
            return NotFound();
          }

          _context.Tarjetas.Update(tarjeta);
          await _context.SaveChangesAsync();

          return Ok(new { message = "La tarjeta fue actualizada con éxito"});

        }
        catch (Exception ex)
        {
          return BadRequest(ex.Message);
        }

      }

      // IAR jue 21DIC2023
      [HttpDelete("{id}")]
      public async Task<IActionResult> Delete(int id)
      {
        try
        {

          var tarjeta = await _context.Tarjetas.FindAsync(id);
          if (tarjeta == null)
          {
            return NotFound();
          }

          _context.Tarjetas.Remove(tarjeta);
          await _context.SaveChangesAsync();

          return Ok(new { message = "La tarjeta fue eliminada con éxito" });

        }
        catch (Exception ex)
        {
          return BadRequest(ex.Message);
        }

      }



    //[HttpPost]
    //public async Task Post()

    //// GET: api/Tarjeta
    //[HttpGet]
    //public IEnumerable<string> Get()
    //{
    //    return new string[] { "value1", "value2" };
    //}

    //// GET: api/Tarjeta/5
    //[HttpGet("{id}", Name = "Get")]
    //public string Get(int id)
    //{
    //    return "value";
    //}

    //// POST: api/Tarjeta
    //[HttpPost]
    //public void Post([FromBody] string value)
    //{
    //}

    //// PUT: api/Tarjeta/5
    //[HttpPut("{id}")]
    //public void Put(int id, [FromBody] string value)
    //{
    //}

    //// DELETE: api/ApiWithActions/5
    //[HttpDelete("{id}")]
    //public void Delete(int id)
    //{
    //}
  }
}
