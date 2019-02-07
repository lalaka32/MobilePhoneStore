using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileStore.Models.MobilePhoneModels
{
    public class MobilePhoneViewModel
    {
		public MobilePhone Phone{ get; set; }

		public bool IsFavourite { get; set; }
	}
}
