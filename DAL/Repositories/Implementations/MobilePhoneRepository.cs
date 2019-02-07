using Common.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implementations
{
	public class MobilePhoneRepository : IMobilePhoneRepository
	{
		readonly StoreContext storeContext;

		public MobilePhoneRepository(StoreContext storeContext)
		{
			this.storeContext = storeContext;

			if (!storeContext.MobilePhones.Any())
			{

				storeContext.MobilePhones.AddRange(
					new MobilePhone[]
					{
						new MobilePhone {  ModelName ="IPhone 10",   Brand="Apple", Price = 9999},
						new MobilePhone {  ModelName ="S9",  Brand="SAMSUNG", Price = 999},
						new MobilePhone {  ModelName ="Redmi 5",   Brand="XIAOMI", Price = 99}
					});
			}
			if (!storeContext.Users.Any())
			{
				User user1 = new User()
				{
					Login = "User1",
					Password = "User1"
				};
				user1.UserPhones.Add(new UserPhone { UserId = 1, PhoneId = 1 });
				user1.UserPhones.Add(new UserPhone { UserId = 1, PhoneId = 2 });
				user1.UserPhones.Add(new UserPhone { UserId = 1, PhoneId = 3 });

				User user2 = new User()
				{
					Login = "User2",
					Password = "User2"
				};
				user2.UserPhones.Add(new UserPhone { UserId = 2, PhoneId = 1 });
				user2.UserPhones.Add(new UserPhone { UserId = 2, PhoneId = 2 });
				user2.UserPhones.Add(new UserPhone { UserId = 2, PhoneId = 3 });

				storeContext.Users.AddRange(
					new User[]
					{
				user1,
				user2
					});
			}
			storeContext.SaveChanges();
		}

		List<MobilePhone> GetJoined()
		{
			return storeContext.MobilePhones.Include(p => p.UserPhones).ThenInclude(up => up.User).ToList();
		}

		public void Create(MobilePhone data)
		{
			storeContext.MobilePhones.Add(data);
			storeContext.SaveChanges();
		}

		public void Delete(int id)
		{
			storeContext.MobilePhones.Remove(Read(id));
			storeContext.SaveChanges();
		}

		public List<MobilePhone> GetUserPhones(int userId)
		{

			var query = from mobile in storeContext.MobilePhones
						where mobile.UserPhones.Any(c => c.UserId == userId)
						select mobile;

			return query.ToList();
		}

		public List<MobilePhone> List()
		{
			return storeContext.MobilePhones.ToList();
		}

		public MobilePhone Read(int id)
		{
			return storeContext.MobilePhones.Single(x => x.Id == id);
		}

		public void Update(MobilePhone data)
		{
			storeContext.MobilePhones.Update(data);
			storeContext.SaveChanges();
		}

		public void AddPhoneToUser(int userId, MobilePhone phone)
		{
			var phoneJoined = GetJoined().Single(p => p.Id == phone.Id);

			phoneJoined.UserPhones.Add(new UserPhone() { PhoneId = phone.Id, UserId = userId });

			storeContext.SaveChanges();
		}

		public void DeletePhoneFromUser(int userId, MobilePhone phone)
		{
			var phoneJoined = GetJoined().Single(p => p.Id == phone.Id);

			phoneJoined.UserPhones.Remove(phoneJoined.UserPhones.Single(up => up.PhoneId == phone.Id && up.UserId == userId));

			storeContext.SaveChanges();
		}

		public bool PhoneIsFavourite(int userId, int id)
		{
			return GetJoined().Single(p => p.Id == id).UserPhones.Any(up => up.PhoneId == id && up.UserId == userId);
		}
	}
}
