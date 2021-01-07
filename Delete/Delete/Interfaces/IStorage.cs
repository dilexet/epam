using System.Collections.Generic;

namespace Delete.Interfaces
{
    public interface IStorage<T>
    {
        IList<T> GetInfoList();
    }
}
