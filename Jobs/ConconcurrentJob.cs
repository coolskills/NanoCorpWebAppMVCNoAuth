using Microsoft.AspNetCore.SignalR;
using Quartz;

namespace NanoCorpWebAppMVCNoAuth.Jobs
{
    public class ConconcurrentJob : IJob
    {
        private static int _counter = 0;
        private readonly IHubContext<JobsHub> _hubContext;

        public ConconcurrentJob(IHubContext<JobsHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var count = _counter++;

            //start the job
            var beginMessage = $"Conconcurrent Job BEGIN {count} {DateTime.UtcNow}";
            await _hubContext.Clients.All.SendAsync("ConcurrentJobs", beginMessage);

            //we have long running job
            Thread.Sleep(5000);

            //complete the job
            var endMessage = $"Conconcurrent Job END {count} {DateTime.UtcNow}";
            await _hubContext.Clients.All.SendAsync("ConcurrentJobs", endMessage);
        }
    }
}
