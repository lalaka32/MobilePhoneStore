using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBServices.Interfaces
{
	public interface IPhonesStore
	{
		IEnumerable<PhoneDTO> Catalog(int userId);
		PhoneDTO GetPhoneDTO(int phoneId, int userId);
		void MarkAsFavourite(int phoneId, int userId);
		void DeleteFromFavourite(int phoneId, int userId);
	}
}
