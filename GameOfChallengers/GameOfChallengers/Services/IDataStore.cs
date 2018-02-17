using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameOfChallengers.Services
{
    public interface IDataStore
    {
        Task<bool> AddAsync_Item(Item item);
        Task<bool> UpdateAsync_Item(Item item);
        Task<bool> DeleteAsync_Item(Item item);
        Task<Item> GetAsync_Item(string id);
        Task<IEnumerable<Item>> GetAllAsync_Item(bool forceRefresh = false);

        Task<bool> AddAsync_Creature(Creature data);
        Task<bool> UpdateAsync_Creature(Creature data);
        Task<bool> DeleteAsync_Creature(Creature data);
        Task<Character> GetAsync_Creature(string id);
        Task<IEnumerable<Character>> GetAllAsync_Creature(bool forceRefresh = false);


        Task<bool> AddAsync_GameItem(GameItem data);
        Task<bool> UpdateAsync_GameItem(GameItem data);
        Task<bool> DeleteAsync_GameItem(GameItem data);
        Task<Score> GetAsync_GameItem(string id);
        Task<IEnumerable<Score>> GetAllAsync_GameItem(bool forceRefresh = false);

    }
}
