using VideoGameCharacterApi.Dtos;
using VideoGameCharacterApi.Models;

namespace VideoGameCharacterApi.Services
{
    public interface IVideoGameCharacterService
    {
        Task<List<CharacterResponse>> GetAllCharacterAsync();
        Task<CharacterResponse?> GetCharacterByIdAsync(int id);
        Task<CharacterResponse> AddCharacterAsync(CreateCharacterRequest character);
        Task<bool> UpdateCharacterAsync(int id , UpdateCharacterRequest charcter);
        
        Task<bool> DeleteCharacterAsync(int id);
        //Task AddCharacterAsync(CreateCharacterRequest character);
    }
}
