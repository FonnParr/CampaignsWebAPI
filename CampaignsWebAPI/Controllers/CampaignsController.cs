using AutoMapper;
using CampaignsWebAPI.Dto;
using CampaignsWebAPI.Interfaces;
using CampaignsWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CampaignsWebAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CampaignsController : ControllerBase
	{
		private readonly ICampaignRepository _repository;
		private readonly IMapper _mapper;

		public CampaignsController(ICampaignRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}
        [HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK, Type=typeof(IEnumerable<CampaignDto>))]
		public IActionResult Get() {
			var campaigns = _repository.GetCampaigns();

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(_mapper.Map<IEnumerable<CampaignDto>>(campaigns));
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CampaignDto))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult GetById(int id) {
			var campaign = _repository.GetCampaignById(id);

			if (campaign == null)
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(_mapper.Map<CampaignDto>(campaign)); 
		}

		[HttpGet("{id}/players")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PlayerDto>))]
		public IActionResult GetPlayersByCampaignId(int id)
		{
			var players = _repository.GetPlayers(id);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(_mapper.Map<IEnumerable<PlayerDto>>(players));
		}


		[HttpGet("{id}/characters")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CharacterDto>))]
		public IActionResult GetCharactersByCampaignId(int id)
		{
			var characters = _repository.GetCharacters(id);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(_mapper.Map<IEnumerable<CharacterDto>>(characters));
		}

		[HttpPost("{id}/addcharacter")]
		public IActionResult AddCharacter(int id, [FromBody] CharacterDto character)
		{
			var result = _repository.AddCharacter(id, _mapper.Map<Character>(character));
			return Ok(result);
		}

		[HttpPut]
		public IActionResult AddCampaign(CampaignDto dto)
		{
			var campaign = _mapper.Map<Campaign>(dto);
			return Ok(_repository.Add(campaign));
		}

		[HttpPut("{id}")]
		public IActionResult UpdateCampaign(int id, CampaignDto dto)
		{		
			var campaign = _mapper.Map<Campaign>(dto);
			return Ok(_repository.Update(id, campaign));
		}
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			return Ok(_repository.Delete(id));
		}

	}
}
