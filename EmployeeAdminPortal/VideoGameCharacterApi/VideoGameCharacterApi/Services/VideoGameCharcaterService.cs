using Microsoft.EntityFrameworkCore;
using VideoGameCharacterApi.Data;
using VideoGameCharacterApi.Dtos;
using VideoGameCharacterApi.Models;

namespace VideoGameCharacterApi.Services
{
    public class VideoGameCharcaterService(AppDbContext context) : IVideoGameCharacterService
    {
        //static List<Character> characters = new List<Character>()
        //{
        //    new Character {Id = 1, Name = "Mario", Game = "Super Mario Bros.",Role = "Hero"},
        //    new Character {Id = 2, Name = "Link", Game = "The Legend of Zelda.",Role = "Hero"},
        //    new Character {Id = 3, Name = "Bowser", Game = "Super Mario Bros.",Role = "Villain"},
        //    new Character {Id = 4, Name = "Zelda", Game = "The Legend of Zelda.",Role = "Princess"},



        //};


        public async Task<CharacterResponse> AddCharacterAsync(CreateCharacterRequest character)
        {
            var newCharacter = new CharacterResoponse
            {
                Name = character.Name,
                Game = character.Game,
                Role = character.Role
            };
            context.Characters.Add(newCharacter);
            await context.SaveChangesAsync();
            return new CharacterResponse
            {
                Id = newCharacter.Id,
                Name = character.Name,
                Game = character.Game,
                Role = character.Role

            };

        }

        public Task<bool> DeleteCharacterAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CharacterResponse>> GetAllCharacterAsync()
                 => await context.Characters.Select(c => new CharacterResponse
                 {
                     Id = c.Id,
                     Name = c.Name,
                     Game = c.Game,
                     Role = c.Role
                 }).ToListAsync();

        public async Task<CharacterResponse? > GetCharacterByIdAsync(int id)
        {
            var result = await context.Characters
                .Where(c => c.Id == id)
                .Select(c => new CharacterResponse {
                    Id = c.Id,
                    Name = c.Name,
                    Game = c.Game,
                    Role = c.Role
                    
                    }).FirstOrDefaultAsync();

            return result;
        }

        public Task<bool> UpdateCharacterAsync(int id, UpdateCharacterReqest charcter)
        {
            throw new NotImplementedException();
        }
    }
}
