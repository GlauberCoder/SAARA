using Domain.Abstractions.Entitys;
using Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Contexts
{
	public class SAARAContext : DbContext
	{
		public SAARAContext(DbContextOptions<SAARAContext> options)
		: base(options)
		{
		}

		public DbSet<Exchanger> Exchangers { get; set; }

		public DbSet<Symbol> Symbols { get; set; }

		public DbSet<Account> Accounts { get; set; }
	}
}
