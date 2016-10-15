namespace RadioFrequencyCenter.DataBrowser.DataAccessLayer
{
    public class Repository
    {
        protected Repository()
        {
            RepositoryData = new RadioFrequencyCenterDataContext();
        }

        protected RadioFrequencyCenterDataContext RepositoryData { get; }
    }
}