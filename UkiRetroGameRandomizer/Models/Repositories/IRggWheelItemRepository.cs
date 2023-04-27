using UkiRetroGameRandomizer.Models.Data;

namespace UkiRetroGameRandomizer.Models.Repositories
{
    public interface IRggWheelItemRepository : IRepository<WheelItem>
    {
        WheelItem FindByName(string name);        
    }
}