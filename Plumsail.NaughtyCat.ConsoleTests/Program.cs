using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Plumsail.NaughtyCat.DataAccess;

namespace Plumsail.NaughtyCat.ConsoleTests
{
	class Program
	{
		private static IConfigurationRoot ConfigurationRoot;

		static void Main(string[] args)
		{
			ConfigurationRoot = new ConfigurationBuilder()
				.SetBasePath(Path.Combine(AppContext.BaseDirectory))
				.AddJsonFile("appsettings.json")
				.Build();

			var connectionString = ConfigurationRoot.GetConnectionString("DefaultConnection");

			var optionsBuilder = new DbContextOptionsBuilder();
			optionsBuilder.UseSqlServer(connectionString);

			TestWhetherOrderingLoadDbData(optionsBuilder.Options);
		}

		static void TestWhetherOrderingLoadDbData(DbContextOptions options)
		{
			using (var context = new NaughtyCatDbContext(options))
			{
				var data = context.Rabbits.OrderByDescending(x => x.Color);
				var test = data;
				// data not loaded
			}
		}
	}
}