using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
	public class UserPhone
	{
		public int UserId { get; set; }
		public User User { get; set; }

		public int PhoneId { get; set; }
		public MobilePhone Phone { get; set; }

	}
}
