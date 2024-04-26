using MentallyStable.GitHelper.Data.Development;

namespace MentallyStable.GitHelper.Services.Development
{
    public interface IDebugger
    {
        public static void Log(string message) => Console.WriteLine(message);

        public abstract void Log(string message, DebugOptions options);

        public abstract void TryExecute(Action codeBlock, DebugOptions options = default);
        public abstract Task TryExecuteAsync(Task codeBlock, DebugOptions options = default);
        public abstract void TryExecuteAsync(Action codeBlock, DebugOptions options = default);
    }
}
