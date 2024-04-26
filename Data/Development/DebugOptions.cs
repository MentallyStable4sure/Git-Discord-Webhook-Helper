
namespace MentallyStable.GitHelper.Data.Development
{
    public struct DebugOptions
    {
        public bool IsShowSource;
        public bool IsSilent;

        public string CustomPrefix;
        public Type Source;

        public DebugOptions(object source, string customPrefix = "", bool isSilent = false, bool isShowSource = true)
        {
            IsShowSource = isShowSource;
            IsSilent = isSilent;

            CustomPrefix = customPrefix;
            Source = source.GetType();
        }
    }
}
