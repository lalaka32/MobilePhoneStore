using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
	public interface IMobilePhoneRepository : IRepository<MobilePhone>
	{
		List<MobilePhone> GetUserPhones(int userId);

		void AddPhoneToUser(int userId, MobilePhone phone);

		void DeletePhoneFromUser(int userId, MobilePhone phone);

		bool PhoneIsFavourite(int userId, int id);
	}
}
