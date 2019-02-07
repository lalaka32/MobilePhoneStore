using Common.Entities;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implementations
{
	public class UserRepository : IUserRepository
	{
		readonly StoreContext storeContext;

		public UserRepository(StoreContext storeContext)
		{
			this.storeContext = storeContext;
		}

		public void Create(User data)
		{
			storeContext.Users.Add(data);
			storeContext.SaveChanges();
		}

		public void Delete(int id)
		{
			storeContext.Users.Remove(Read(id));
			storeContext.SaveChanges();
		}

		public User GetUserByLogin(string login)
		{
			return storeContext.Users.FirstOrDefault(u=>u.Login == login);
		}

		public List<User> List()
		{
			return storeContext.Users.ToList();
		}

		public User Read(int id)
		{
			return storeContext.Users.Single(x => x.Id == id);
		}

		public void Update(User data)
		{
			storeContext.Users.Update(data);
			storeContext.SaveChanges();
		}

	}
}
