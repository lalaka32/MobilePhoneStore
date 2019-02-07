using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
	public class User
	{
		public int Id { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

		public List<UserPhone> UserPhones { get; set; }

		public User()
		{
			UserPhones = new List<UserPhone>();
		}
	}
}
