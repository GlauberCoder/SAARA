using Infra.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Infra.Abstractions.Repositories;
using Infra.Repositories;

namespace Infra.Extensions
{
	public static class IServiceCollectionExtensions
	{
		public static IServiceCollection AddInfra(this IServiceCollection services, string connectionString)
		{
			services
				.AddDbContext<SAARAContext>(options => options.UseSqlServer(connectionString))
				.AddScoped<IExchangers, Exchangers>()
				.AddScoped<ISymbols, Symbols>()
				.AddScoped<IAccounts, Accounts>();

			return services;
		}
	}
}
