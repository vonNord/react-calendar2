using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace ServerApp
{
	public class Startup
	{
		public Startup( IConfiguration configuration )
		{
			Configuration = configuration;
		}

		readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices( IServiceCollection services )
		{

			services.AddCors( options =>
			{
				options.AddPolicy( MyAllowSpecificOrigins,
				builder =>
				{
					builder.WithOrigins( "*"
										, "http://localhost:3000",
										"https://localhost:3000" )
										//.AllowAnyHeader()
										//.AllowAnyMethod()
										;
				} );
			} );


			services.AddControllers();
			services.AddSwaggerGen( c =>
			{
				c.SwaggerDoc( "v1", new OpenApiInfo { Title = "Appointment_Mock API", Version = "v1" } );
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
		{
			if( env.IsDevelopment() )
			{
				app.UseDeveloperExceptionPage();
			}
			// else here from tutorial? 23:31

			app.UseCors( MyAllowSpecificOrigins );
			//app.UseHttpsRedirection();
			app.UseSwagger();
			app.UseSwaggerUI( c => 
			{
				c.SwaggerEndpoint( "/swagger/v1/swagger.json", "Appointment_Mock API V1" );
			} );

			app.UseRouting();


			app.UseAuthorization();

			app.UseEndpoints( endpoints =>
			 {
				 endpoints.MapControllers();
			 } );
		}
	}
}
