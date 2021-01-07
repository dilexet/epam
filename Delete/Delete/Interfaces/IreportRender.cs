using System.Collections.Generic;
using Delete.BillingSystem;
using Delete.Enums;

namespace Delete.Interfaces
{
    public interface IReportRender
    {
        void Render(Report report);
        IEnumerable<ReportRecord> SortCalls(Report report, TypeSort sortType);
    }
}
