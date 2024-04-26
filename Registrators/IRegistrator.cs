namespace MentallyStable.GitHelper.Registrators
{
    public interface IRegistrator
    {
        public Task Register(WebApplicationBuilder builder);
    }
}
