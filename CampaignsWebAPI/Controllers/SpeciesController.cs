using AutoMapper;
using CampaignsWebAPI.Dto;
using CampaignsWebAPI.Interfaces;
using CampaignsWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CampaignsWebAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class SpeciesController : ControllerBase
	{
		private readonly ISpeciesRepository _repository;
		private readonly IMapper _mapper;

		public SpeciesController(ISpeciesRepository repository, IMapper mapper)
        {
			_repository = repository;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK, Type=typeof(IEnumerable<SpeciesDto>))]
		public IActionResult Get()
		{
			var allSpecies = _repository.GetAllSpecies();

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(_mapper.Map<IEnumerable<SpeciesDto>>(allSpecies));
		}


		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SpeciesDto))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetSpecies(int id)
		{
			var species = _repository.GetSpeciesById(id);

			if (species == null)
				return NotFound();

            if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(_mapper.Map<SpeciesDto>(species));
		}

		[HttpPut()]
		public IActionResult Put(SpeciesDto species)
		{
			var success = _repository.AddSpecies(species.Name);

			return Ok(success);
		}

		[HttpPut("{id}")]
		public IActionResult Overwrite(int id, SpeciesDto species)
		{
			_repository.UpdateSpecies(id, _mapper.Map<Species>(species));

			return NoContent();
		}


		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			_repository.DeleteSpecies(id);

			return NoContent();
		}
	}
}
