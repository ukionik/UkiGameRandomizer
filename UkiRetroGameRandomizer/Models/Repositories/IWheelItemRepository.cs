using UkiRetroGameRandomizer.Models.Data;

namespace UkiRetroGameRandomizer.Models.Repositories
{
    public interface IWheelItemRepository : IRepository<WheelItem>
    {
        WheelItem FindByName(string name);
    }
}