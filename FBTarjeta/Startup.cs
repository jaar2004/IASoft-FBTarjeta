// IAR dom 29OCT2023
/*
 Con este link configuré Swagger
 Add Swagger to ASP.NET Core 2.1 Web API (configurar)
https://www.linkedin.com/pulse/add-swagger-aspnet-core-21-web-api-salman-tariq
 */
using FBTarjeta.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FBTarjeta
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      // Agregar servicio de Swagger. IAR dom 29OCT2023
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
        {
          Version = "v1",
          Title = "Checkout FETarjetaCredito api documentation",
          Description = "This documentation provide the information about check out FETarjetaCredito api endpoints.",
          TermsOfService = "None",
          Contact = new Swashbuckle.AspNetCore.Swagger.Contact()
          {
            Name = "Isidro Almaguer",
            Email = "iarsan_@hotmail.com",
            Url = ""
          }
        });
      });

      // IAR sab 11NOV2023
      services.AddDbContext<AplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));

      // IAR dom 24DIC2023
      /*
        What is the use of CORS in Web API?
        Cross Origin Resource Sharing (CORS) is a W3C standard that allows a server to relax the same-origin
        policy. Using CORS, a server can explicitly allow some cross-origin requests while rejecting others.
        CORS is safer and more flexible than earlier techniques such as JSONP.
      */
      services.AddCors(options => options.AddPolicy("AllowWebApp",
            builder => builder.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod()));

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }

      app.UseCors("AllowWebApp"); // IAR dom 24DIC2023

      app.UseHttpsRedirection();
            
      app.UseMvc();
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Check out FETarjetaCredito API");
      });
    }
  }
}
