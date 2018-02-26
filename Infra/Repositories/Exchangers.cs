using Domain.Abstractions.Entitys;
using Infra.Abstractions.Repositories;
using Infra.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Domain.Entitys;

namespace Infra.Repositories
{
	public class Exchangers : Repository<IExchanger, Exchanger>, IExchangers 
	{
		public Exchangers(SAARAContext saaraContext) : base(saaraContext, saaraContext.Exchangers)
		{
		}
	}
}
