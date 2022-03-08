using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Delta.Invoicing.Core.Logging
{
    static class LoggerExtensions
    {
        public static void LogActivityStart(this ILogger logger, string activityName, out Activity activity)
        {
            activity = new Activity(activityName).Start();
            logger.LogDebug($"{activityName} started");
        }

        public static void LogActivityEnd(this ILogger logger, Activity activity, Func<Activity, string>? func = null)
        {
            activity.Stop();
            string? addon = func?.Invoke(activity);
            logger.LogInformation($"{activity.DisplayName} took {activity.Duration.TotalMilliseconds:N0} ms {addon}");
        }

        public static void LogActivityEndObjectsPerSec(this ILogger logger, Activity activity, int count)
        {
            logger.LogActivityEnd(activity, (act) => $"({count / act.Duration.TotalSeconds:N1} objects/sec)");
        }
    }
}
