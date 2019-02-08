using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBServices.Interfaces
{
	public interface IUserStore
	{
		User GetUserByLogin(string login);
		void AddUser(User user);
	}
}
