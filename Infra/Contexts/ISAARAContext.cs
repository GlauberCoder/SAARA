using Domain.Abstractions.Entitys;
using Microsoft.EntityFrameworkCore;

namespace Infra.Contexts
{
	public interface ISAARAContext
	{
		DbSet<IExchanger> Exchangers { get; set; }
	}
}