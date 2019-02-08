using Common.Entities;
using DBServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBServices.Implementations
{
	public class UserStore : IUserStore
	{
		readonly StoreContext storeContext;

		public UserStore(StoreContext storeContext)
		{
			this.storeContext = storeContext;
		}

		public void AddUser(User data)
		{
			storeContext.Users.Add(data);
			storeContext.SaveChanges();
		}

		public User GetUserByLogin(string login)
		{
			return storeContext.Users.FirstOrDefault(u=>u.Login == login);
		}

	}
}
