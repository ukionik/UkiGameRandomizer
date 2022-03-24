using UkiRetroGameRandomizer.Models.Data;

namespace UkiRetroGameRandomizer.Models.Repositories
{
    public interface IDropmaniaWheelItemRepository : IRepository<WheelItem>
    {
        WheelItem FindByName(string name);
    }
}