using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
	public class MobilePhone
	{
		public int Id { get; set; }

		public string Brand { get; set; }

		public string ModelName { get; set; }

		public double Price { get; set; }

		public List<UserPhone> UserPhones { get; set; }

		public MobilePhone()
		{
			UserPhones = new List<UserPhone>();
		}
	}
}
