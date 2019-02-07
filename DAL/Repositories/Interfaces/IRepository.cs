using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
	public interface IRepository<T>
	{
		List<T> List();
		void Create(T data);
		T Read(int id);
		void Update(T data);
		void Delete(int id);
	}
}
