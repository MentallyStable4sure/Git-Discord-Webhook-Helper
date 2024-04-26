using MentallyStable.GitHelper.Data.Development;

namespace MentallyStable.GitHelper.Services.Development
{
    public class Debugger : IDebugger
    {
        public void Log(string message, DebugOptions options = default)
        {
            string fullMessage = "";
            if (options.CustomPrefix != null && options.CustomPrefix.Length >= 1) fullMessage += options.CustomPrefix;

            fullMessage += $" {message}";
            if (options.IsShowSource) fullMessage += $"\n >>> [Source: {options.Source}]\n";
            IDebugger.Log(fullMessage);
        }

        public void TryExecute(Action codeBlock, DebugOptions options = default)
        {
            string result = StateToResult(false);
            if (codeBlock == null)
            {
                LogResult(result, options);
                return;
            }

            try
            {
                codeBlock.Invoke();
                result = StateToResult(true);
                LogResult(result, options);
                return;
            }
            catch (Exception exception)
            {
                Log(result + $"\nDETAILS:\n{exception.Message}", options);
                return;
            }
        }
        public async Task TryExecuteAsync(Task codeBlock, DebugOptions options = default)
        {
            string result = StateToResult(false);
            if (codeBlock == null)
            {
                LogResult(result, options);
                return;
            }

            try
            {
                await codeBlock;
                result = StateToResult(true);
                LogResult(result, options);
                return;
            }
            catch (Exception exception)
            {
                Log(result + $"\nDETAILS:\n{exception.Message}", options);
                return;
            }
        }

        public async void TryExecuteAsync(Action codeBlock, DebugOptions options = default)
        {
            string result = StateToResult(false);
            if (codeBlock == null)
            {
                LogResult(result, options);
                return;
            }

            try
            {
                await Task.Run(() => codeBlock);
                result = StateToResult(true);
                LogResult(result, options);
                return;
            }
            catch (Exception exception)
            {
                Log(result + $"\nDETAILS:\n{exception.Message}", options);
                return;
            }
        }

        public void CheckResult(string check, DebugOptions options = default)
        {
            string result = StateToResult(false);
            if (check != null && check.Length > 0) result = StateToResult(true);
            
            LogResult(result, options);
        }

        public void CheckResult(bool check, DebugOptions options = default) => LogResult(StateToResult(check), options);
        public void CheckResult(object check, DebugOptions options = default) => LogResult(StateToResult(check != null), options);

        private string StateToResult(bool state) => state ? "[OK]: CODE BLOCK DONE" : "[ERROR]: CODE BLOCK FAIL";

        private void LogResult(string result, DebugOptions options = default)
        {
            if (options.IsSilent) return;

            Log(result, options);
        }
    }
}
