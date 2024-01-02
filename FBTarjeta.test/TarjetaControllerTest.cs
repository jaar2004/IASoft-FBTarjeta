// IAR sab 30D2023

/*
 texto busqueda: testing api endpoints  with nunit in asp.net core
 API Testing in ASP.NET Core (este es excelente link de consulta)
 https://medium.com/@rojasjimenezjosea/api-testing-in-asp-net-core-ae0b406f8cd3
 repositorio: https://github.com/rojasjo/ApiTestDemo
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using FBTarjeta.Controllers;


namespace FBTarjeta.test
{
  [TestFixture]
  public class TarjetaControllerTest
  {
    Context.AplicationDbContext _context;
    
    TarjetaController controller = new TarjetaController(_context);

    [Test]
    //public async Task<IActionResult> get_ListadoTarjetas_OK()
    public Task<IActionResult> get_ListadoTarjetas_OK()
    {
      
    }

  }
}
