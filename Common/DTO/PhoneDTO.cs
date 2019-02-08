using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
	public class PhoneDTO 
	{
		public MobilePhone Phone { get; set; }

		public bool IsFavourite { get; set; }
	}
}
