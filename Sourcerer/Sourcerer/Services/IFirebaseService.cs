using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sourcerer.Services
{
    public interface IFirebaseService<T>
    {
        void Init();
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
