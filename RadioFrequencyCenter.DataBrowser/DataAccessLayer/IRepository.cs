using System.Linq;

namespace RadioFrequencyCenter.DataBrowser.DataAccessLayer
{
    public interface IRepository
    {
        #region BroadcastStation
        IQueryable<BroadcastStations> BroadcastStations { get; }

        bool InsertStation(BroadcastStations instance);

        bool UpdateStation(BroadcastStations instance);

        bool DeleteAllStation();

        #endregion

        #region BroadcastFrequency

        IQueryable<BroadcastFrequencies> BroadcastFrequencies { get; }

        bool InsertFrequency(BroadcastFrequencies instance);

        bool UpdateFrequency(BroadcastFrequencies instance);

        bool DeleteAllFrequency();

        #endregion
    }
}
