using Common.DTO;
using Common.Entities;
using DBServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBServices.Implementations
{
    public class PhonesStore : IPhonesStore
    {
		readonly StoreContext storeContext;

		public PhonesStore(StoreContext storeContext)
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

		public IEnumerable<PhoneDTO> Catalog(int userId)
		{
			var phonesDTO = storeContext.MobilePhones.Select(phone => new PhoneDTO
			{
				Phone = phone,
				IsFavourite = phone.UserPhones.Any(up => up.UserId == userId)
			}).ToList();
			return phonesDTO;
			//throw new NotImplementedException();
		}

		public void DeleteFromFavourite(int phoneId, int userId)
		{
			storeContext.MobilePhones.Include(p => p.UserPhones)
				.ThenInclude(up => up.User)
				.Single(p => p.Id == phoneId)
				.UserPhones
				.Remove(storeContext.MobilePhones.Include(p => p.UserPhones)
				.Single(p => p.Id == phoneId)
				.UserPhones
				.Single(up => up.PhoneId == phoneId && up.UserId == userId));

			storeContext.SaveChanges();
			//throw new NotImplementedException();
		}

		public PhoneDTO GetPhoneDTO(int phoneId, int userId)
		{
			var phoneDTO = storeContext.MobilePhones.Select(phone => new PhoneDTO
			{
				Phone = phone,
				IsFavourite = phone.UserPhones.Any(up => up.UserId == userId)
			}).Single(pDTO=> pDTO.Phone.Id == phoneId);
			return phoneDTO;
			//throw new NotImplementedException();
		}

		public void MarkAsFavourite(int phoneId, int userId)
		{
			storeContext.MobilePhones.Include(p => p.UserPhones)
				.ThenInclude(up => up.User)
				.Single(p => p.Id == phoneId)
				.UserPhones.Add(new UserPhone() { PhoneId = phoneId, UserId = userId });

			storeContext.SaveChanges();
			//throw new NotImplementedException();
		}
	}
}
