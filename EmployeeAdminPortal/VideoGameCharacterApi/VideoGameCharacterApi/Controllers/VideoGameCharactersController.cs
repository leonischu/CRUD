using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoGameCharacterApi.Dtos;
using VideoGameCharacterApi.Models;
using VideoGameCharacterApi.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace VideoGameCharacterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameCharactersController(IVideoGameCharacterService service): ControllerBase
    {
        

        [HttpGet]
        public async Task <ActionResult<List<CharacterResponse>>> GetCharacters()    
            => Ok(await service.GetAllCharacterAsync());

        [HttpGet("{id}")]

        public async Task<ActionResult<CharacterResponse>> GetCharacter(int id)
        {

            var character = await service.GetCharacterByIdAsync(id);
            if(character is null)
            {
                return NotFound();
            }
            return Ok(character);
        }
        [HttpPost]

        public async Task<ActionResult<CharacterResponse>> AddCharacter(CreateCharacterRequest character)
        {
            var createdCharacter = await service.AddCharacterAsync(character);

            return CreatedAtAction(
                nameof(GetCharacter),
                new { id = createdCharacter.Id },  
                createdCharacter);
        }

        [HttpPut]

        public async Task<ActionResult> UpdateCharacter(int id, UpdateCharacterRequest character)
        {
            var updated = await service.UpdateCharacterAsync(id, character);
            return updated ? NoContent() : NotFound();
        }
        public async Task<ActionResult>DeleteCharacter(int id)
        {
            var deleted = await service.DeleteCharacterAsync(id);
            return deleted ? NoContent() : NotFound();

        }

    }
}
