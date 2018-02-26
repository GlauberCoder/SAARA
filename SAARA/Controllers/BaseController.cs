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
	public class BaseController<TEntity, TConcreteEntity, TRepository> : Controller
		where TRepository : IRepository<TEntity>
		where TConcreteEntity : TEntity
	{
		protected readonly TRepository Repository;
		public BaseController(TRepository repository)
		{
			Repository = repository;
		}

		[HttpGet]
		public IActionResult Get() => Ok(Repository.Actives());

		[HttpGet("{id}")]
		public  IActionResult Get([FromRoute] long id) => Ok(Repository.By(id));

		[HttpPost("save")]
		public IActionResult Save([FromBody] TConcreteEntity entity) => Ok(Repository.Save(entity));

		[HttpDelete("{id}")]
		public IActionResult Delete([FromRoute] long id) => Ok(Repository.Delete(id));
		
	}
}