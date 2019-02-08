using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobileStore.Authorization.AuthExtantions;
using DBServices.Interfaces;
using Common.DTO;

namespace MobileStore.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MobilePhoneController : ControllerBase
	{

		IPhonesStore _phonesStore;

		public MobilePhoneController(IPhonesStore phonesStore)
		{
			_phonesStore = phonesStore;
		}

		[HttpGet("[action]")]
		public IEnumerable<PhoneDTO> Catalog()
		{
			IEnumerable<PhoneDTO> catalog = null;
			try
			{
				var userId = User.Id();
				catalog = _phonesStore.Catalog(userId);
			}
			catch (MissingFieldException e)
			{
				return null;
			}
			catch (InvalidOperationException e)
			{
				return null;
			}
			return catalog;
		}

		[HttpGet("{id}")]
		public PhoneDTO Get(int id)
		{
			PhoneDTO phone = null;
			try
			{
				var userId = User.Id();
				phone = _phonesStore.GetPhoneDTO(id, userId);
			}
			catch (InvalidOperationException e)
			{
				return null;
			}

			return phone;
		}

		[Authorize]
		[HttpGet("[action]/{phoneId}")]
		public ActionResult MarkAsFavourite(int phoneId)
		{
			int userId = 0;
			try
			{
				userId = User.Id();
				_phonesStore.MarkAsFavourite(phoneId, userId);
			}
			catch (MissingFieldException e)
			{
				return BadRequest();
			}
			catch (InvalidOperationException e)
			{
				return BadRequest();
			}

			return Ok();
		}

		[Authorize]
		[HttpDelete("[action]/{phoneId}")]
		public ActionResult DeleteFromFavourite(int phoneId)
		{
			int userId = 0;
			try
			{
				userId = User.Id();
				_phonesStore.DeleteFromFavourite(phoneId, userId);
			}
			catch (MissingFieldException e)
			{
				return BadRequest();
			}
			catch (InvalidOperationException e)
			{
				return BadRequest();
			}

			return Ok();
		}
	}
}