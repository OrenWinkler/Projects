using Common.BusinessObjects;

namespace DBService.Connectors
{
    public interface IDBConnector
    {
        void AddContest(Contest contest);
    }
}