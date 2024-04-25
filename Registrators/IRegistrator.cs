namespace MentallyStable.GitlabHelper.Registrators
{
    public interface IRegistrator
    {
        public Task Register(WebApplicationBuilder builder);
    }
}
