using System;

namespace SalesStatistics.BusinessLogic
{
    public interface IController : IDisposable
    {
        void Start();
        
        void Stop();
    }
}