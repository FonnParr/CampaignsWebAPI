using AutoMapper;
using CampaignsWebAPI.Dto;
using CampaignsWebAPI.Interfaces;
using CampaignsWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CampaignsWebAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CharacterController : ControllerBase
	{
		private readonly ICharacterRepository _repository;
		private readonly IMapper _mapper;

		public CharacterController(ICharacterRepository repository, IMapper mapper)
        {
			_repository = repository;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK, Type=typeof(IEnumerable<CharacterDto>))]
		public IActionResult Get()
		{
			var characters = _repository.GetCharacters();

			if (!ModelState.IsValid)
				return BadRequest();

			return Ok(_mapper.Map<IEnumerable<CharacterDto>>(characters));
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CharacterDto))]
		public IActionResult Get(int id)
		{
			var character = _repository.GetCharacterById(id);

			if (character == null)
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest();

			return Ok(_mapper.Map<CharacterDto>(character));
		}

		[HttpGet("{id}/levels")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClassLevelDto>))]
		public IActionResult GetClassLevels(int id)
		{
			var levels = _repository.GetClassLevels(id);

			if (!ModelState.IsValid)
				return BadRequest();

			return Ok(_mapper.Map< IEnumerable<ClassLevelDto>>(levels));
		}

		[HttpGet("{id}/level")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
		public IActionResult GetCharacterLevel(int id)
		{
			return Ok(_repository.GetCharacterLevel(id));
		}

		[HttpPost("{charId}/addlevel")]
		public IActionResult AddLevel(int charId, int classId)
		{
			return Ok(_repository.AddLevel(charId, classId));
		}

		[HttpPost("{charId}/removelevel")]
		public IActionResult RemoveLevel(int charId, int classId)
		{
			return Ok(_repository.RemoveLevel(charId, classId));
		}

		[HttpPut]
		public IActionResult AddCharacter(CharacterDto dto)
		{
			var character = _mapper.Map<Character>(dto);
			return Ok(_repository.AddCharacter(character));			
		}

		[HttpPut("{id}")]
		public IActionResult UpdateCharacter(int id, CharacterDto dto)
		{
			var character = _mapper.Map<Character>(dto);
			return Ok(_repository.UpdateCharacter(id, character));
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteCharacter(int id)
		{
			return Ok(_repository.DeleteCharacter(id));
		}

	}
}
