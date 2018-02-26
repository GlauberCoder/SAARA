using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entitys;
using Infra.Abstractions.Repositories;
using Domain.Abstractions.Entitys;

namespace SAARA.Controllers
{
	[Produces("application/json")]
	[Route("api/[Controller]")]
	public class ExchangersController : BaseController<IExchanger, Exchanger, IExchangers>
	{
		public ExchangersController(IExchangers exchangers) : base(exchangers)
		{
		}
	}
}