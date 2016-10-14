namespace RadioFrequencyCenter.DataBrowser.DataAccessLayer
{
    public class Repository
    {
        protected Repository()
        {
            RepositoryData = new RadioFrequencyCenterDataContext();
        }
        public RadioFrequencyCenterDataContext RepositoryData { get; }
    }
}