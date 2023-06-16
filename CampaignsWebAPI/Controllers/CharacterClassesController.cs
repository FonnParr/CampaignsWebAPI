using AutoMapper;
using CampaignsWebAPI.Dto;
using CampaignsWebAPI.Interfaces;
using CampaignsWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CampaignsWebAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CharacterClassesController : ControllerBase
	{
		private readonly ICharacterClassRepository _repository;
		private readonly IMapper _mapper;

		public CharacterClassesController(ICharacterClassRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK, Type=typeof(IEnumerable<CharacterClassDto>))]
		public IActionResult Get()
		{
			var classes = _repository.GetCharacterClasses();

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(_mapper.Map<IEnumerable<CharacterClassDto>>(classes));
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type=typeof(CharacterClassDto))]
		public IActionResult Get(int id) 
		{
			var characterClass = _repository.GetCharacterClassById(id);

			if (characterClass == null)
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(_mapper.Map<CharacterClassDto>(characterClass));
		}

		[HttpGet("{id}/characters")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CharacterDto>))]
		public IActionResult GetCharacters(int id)
		{
			var characters = _repository.GetCharactersByClassId(id);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(_mapper.Map<IEnumerable<CharacterDto>>(characters));
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			return Ok(_repository.Delete(id));
		}

		[HttpPut]
		public IActionResult AddClass(CharacterClassDto dto)
		{
			var characterClass = _mapper.Map<CharacterClass>(dto);
			return Ok(_repository.AddClass(characterClass));
		}

		[HttpPut("{id}")]
		public IActionResult Update(int id, CharacterClassDto dto)
		{
			var characterClass = _mapper.Map<CharacterClass>(dto);
			return Ok(_repository.Update(id, characterClass));
		}
	}
}
