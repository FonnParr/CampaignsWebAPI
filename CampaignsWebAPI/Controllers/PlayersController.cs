using AutoMapper;
using CampaignsWebAPI.Dto;
using CampaignsWebAPI.Interfaces;
using CampaignsWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CampaignsWebAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PlayersController : ControllerBase
	{
		private readonly IPlayerRepository _repository;
		private readonly IMapper _mapper;

        public PlayersController(IPlayerRepository repository, IMapper mapper)
        {
			_repository = repository;
			_mapper = mapper;
        }

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK, Type=typeof(IEnumerable<PlayerDto>))]
		public IActionResult Get([FromQuery] int? id, [FromQuery] string? filter)
		{
			var players = _repository.GetPlayers();

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (id.HasValue)
				players = players.Where(p => p.ID == id).ToList();

			if (!string.IsNullOrWhiteSpace(filter))
				players = players.Where(p => p.Name.Contains(filter, StringComparison.OrdinalIgnoreCase)).ToList();

			return Ok(_mapper.Map<IEnumerable<PlayerDto>>(players));
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PlayerDto>))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult GetPlayer(int id)
		{
			var player = _repository.GetPlayerById(id);
			if (player == null)
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(_mapper.Map<PlayerDto>(player));
		}

		[HttpGet("{id}/campaigns")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CampaignDto>))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult GetCampaigns(int id)
		{
			var campaigns = _repository.GetCampaignsByPlayerId(id);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(_mapper.Map<IEnumerable<CampaignDto>>(campaigns));
		}


		[HttpGet("{id}/characters")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CharacterDto>))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult GetCharacters(int id)
		{
			var characters = _repository.GetCharactersByPlayerId(id);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(_mapper.Map<IEnumerable<CharacterDto>>(characters));
		}

		[HttpPut]
		public IActionResult Create(PlayerDto dto)
		{
			var player = _mapper.Map<Player>(dto);
			return Ok(_repository.AddPlayer(player));
		}

		[HttpPut("{id}")]
		public IActionResult Update(int id, PlayerDto dto)
		{
			var player = _mapper.Map<Player>(dto);
			return Ok(_repository.UpdatePlayer(id, player));
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			return Ok(_repository.DeletePlayer(id));
		}
	}
}
