using Domain.Abstractions.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Abstractions.Repositories
{
	public interface IRepository<T>
	{
		T By(long id);
		IList<T> Actives();
		IList<T> ListDeletedes();
		T Save(T entity);
		T Delete(T entity);
		T Delete(long id);

	}
}
