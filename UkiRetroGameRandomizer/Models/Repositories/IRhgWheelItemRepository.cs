using UkiRetroGameRandomizer.Models.Data;

namespace UkiRetroGameRandomizer.Models.Repositories
{
    public interface IRhgWheelItemRepository : IRepository<WheelItem>
    {
        WheelItem FindByName(string name);        
    }
}