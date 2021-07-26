using System.Collections.Generic;
using System.Threading.Tasks;

namespace UkiRetroGameRandomizer.Models.Repositories
{
    public interface IRepository<T>
    {
        List<T> Data { get; }
        Task LoadAsync();
    }
}