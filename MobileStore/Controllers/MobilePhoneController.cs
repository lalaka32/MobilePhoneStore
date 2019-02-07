using System;
using System.Collections.Generic;
using Common.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobileStore.Authorization.AuthExtantions;
using MobileStore.Models.MobilePhoneModels;

namespace MobileStore.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MobilePhoneController : ControllerBase
	{
		IMobilePhoneRepository _mobilePhoneRepository;
		IUserRepository _userRepository;

		public MobilePhoneController(IMobilePhoneRepository mobilePhoneRepository, IUserRepository userRepository)
		{
			_mobilePhoneRepository = mobilePhoneRepository;
			_userRepository = userRepository;
		}

		[HttpGet("[action]")]
		public IEnumerable<MobilePhone> Catalog()
		{
			return _mobilePhoneRepository.List();
		}

		[HttpGet("{id}")]
		public MobilePhone Get(int id)
		{
			MobilePhone phone = null;
			try
			{
				phone = _mobilePhoneRepository.Read(id);
			}
			catch (InvalidOperationException e)
			{
				return null;
			}

			return phone;
		}

		[Authorize]
		[HttpGet("[action]")]
		public IEnumerable<MobilePhone> UserFavourite()
		{
			int id = 0;
			IEnumerable<MobilePhone> phone = null;
			try
			{
				id = User.Id();
				phone = _mobilePhoneRepository.GetUserPhones(id);
			}
			catch (MissingFieldException e)
			{
				return null;
			}
			catch (InvalidOperationException e)
			{
				return null;
			}
			return phone;
		}

		[Authorize]
		[HttpGet("[action]")]
		public IEnumerable<MobilePhoneViewModel> UserCatalog()
		{
			int id = 0;
			List<MobilePhoneViewModel> phonesModel = new List<MobilePhoneViewModel>();
			try
			{
				id = User.Id();
				foreach (var phone in Catalog())
				{
					phonesModel.Add(new MobilePhoneViewModel() { Phone = Get(phone.Id), IsFavourite = IsFavourite(phone.Id) });
				}
			}
			catch (MissingFieldException e)
			{
				return null;
			}
			catch (InvalidOperationException e)
			{
				return null;
			}
			return phonesModel;
		}

		[Authorize]
		[HttpGet("[action]/{phoneId}")]
		public bool IsFavourite(int phoneId)
		{
			int id = 0;
			bool isFavorite = false;
			try
			{
				id = User.Id();
				isFavorite = _mobilePhoneRepository.PhoneIsFavourite(id, phoneId);
			}
			catch (MissingFieldException e)
			{
				return false;
			}
			catch (InvalidOperationException e)
			{
				return false;
			}
			return isFavorite;
		}

		[Authorize]
		[HttpGet("[action]/{phoneId}")]
		public ActionResult AddPhoneToUser(int phoneId)
		{
			int id = 0;
			MobilePhone phoneFromRepository = null;
			try
			{
				id = User.Id();
				phoneFromRepository = _mobilePhoneRepository.Read(phoneId);
			}
			catch (MissingFieldException e)
			{
				return BadRequest();
			}
			catch (InvalidOperationException e)
			{
				return BadRequest();
			}
			_mobilePhoneRepository.AddPhoneToUser(id, phoneFromRepository);

			return Ok();
		}

		[Authorize]
		[HttpDelete("[action]/{phoneId}")]
		public ActionResult DeletePhoneUser(int phoneId)
		{
			int id = 0;
			MobilePhone phone = null;
			try
			{
				id = User.Id();
				phone = _mobilePhoneRepository.Read(phoneId);
			}
			catch (MissingFieldException e)
			{
				return BadRequest(new { error = "UserId problem" });
			}
			catch (InvalidOperationException e)
			{
				return BadRequest(new { error = "phoneId problem" });
			}
			_mobilePhoneRepository.DeletePhoneFromUser(id, phone);

			return Ok();
		}
	}
}