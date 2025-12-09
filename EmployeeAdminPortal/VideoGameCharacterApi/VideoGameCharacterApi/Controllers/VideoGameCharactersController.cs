using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoGameCharacterApi.Models;
using VideoGameCharacterApi.Services;

namespace VideoGameCharacterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameCharactersController(IVideoGameCharacterService service): ControllerBase
    {
        

        [HttpGet]
        public async Task <ActionResult<List<Character>>> GetCharacters()

            //var characters = new[]
            //{
            //    new Models.Character { Id = 1, Name = "Mario", Game = "super Mario Bros", Role = "Protagonist" },
            //    new Models.Character { Id = 2, Name = "Link", Game = "The Legend of Zelda", Role = "Protagonist" },
            //    new Models.Character { Id = 3, Name = "Master Chief", Game = "Halo", Role = "Protagonist" },
            //};

            => Ok(await service.GetAllCharacterAsync());

        //return Ok(characters);

        [HttpGet("{id}")]

        public async Task<ActionResult<Character>> GetCharacter(int id)
        {

            var character = await service.GetCharacterByIdAsync(id);
            if(character is null)
            {
                return NotFound();
            }
            return Ok(character);


        }

    }
}
