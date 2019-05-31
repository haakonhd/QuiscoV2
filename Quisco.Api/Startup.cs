using System.Data.SqlClient;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quisco.DataAccess;

namespace Quisco.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public static void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			SqlConnectionStringBuilder connStringBuilder = new SqlConnectionStringBuilder
			{
				DataSource = "donau.hiof.no",
				InitialCatalog = "haakonhd",
				UserID = "haakonhd",
				Password = "BJe8ER3h"
			};
			services.AddDbContext<QuiscoContext>(options => options.UseSqlServer(connStringBuilder.ConnectionString.ToString(CultureInfo.InvariantCulture)));

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
