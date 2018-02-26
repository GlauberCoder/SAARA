using Domain.Abstractions.Entitys;
using Domain.Entitys;
using Infra.Abstractions.Repositories;
using Infra.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repositories
{
	public class Accounts : Repository<IAccount, Account>, IAccounts
	{
		public Accounts(SAARAContext saaraContext) : base(saaraContext, saaraContext.Accounts)
		{
		}
	}
}
