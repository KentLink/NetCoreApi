using System.Threading;
using System.Threading.Tasks;

namespace ScheduleJob.Code
{
    public interface IScheduledTask
    {
        string Schedule { get; }
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}