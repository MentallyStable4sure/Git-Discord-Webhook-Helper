
namespace MentallyStable.GitHelper.Helpers
{
    public static class Converters
    {
        public static string ToImage(this string resposeAction)
        {
            switch (resposeAction)
            {
                case "":
                    return string.Empty;

                case "merge_request":
                    return "https://bunbun.cloud/admin/funkymonke/img/prcreated.png";

                case "comment":
                    return "https://bunbun.cloud/admin/funkymonke/img/prcomment.png";

                case "merged":
                    return "https://bunbun.cloud/admin/funkymonke/img/prmerged.png";

                case "closed":
                    return "https://bunbun.cloud/admin/funkymonke/img/prclosed.png";

                default:
                    return "https://bunbun.cloud/admin/funkymonke/img/pushchanges.png";
            }
        }
    }
}
