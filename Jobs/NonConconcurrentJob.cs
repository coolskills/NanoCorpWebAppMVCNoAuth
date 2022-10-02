using Microsoft.AspNetCore.SignalR;
using Quartz;

namespace NanoCorpWebAppMVCNoAuth.Jobs
{
    public class NonConconcurrentJob : IJob
    {
        private static int _counter = 0;
        private readonly IHubContext<JobsHub> _hubContext;

        public NonConconcurrentJob(IHubContext<JobsHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var count = _counter++;

            //start the job
            var beginMessage = $"NonConconcurrentJob Job BEGIN {count} {DateTime.UtcNow}";
            await _hubContext.Clients.All.SendAsync("nonConcurrentJobs", beginMessage);

            //we have long running job
            Thread.Sleep(5000);

            //complete the job
            var endMessage = $"NonConconcurrentJob Job END {count} {DateTime.UtcNow}";
            await _hubContext.Clients.All.SendAsync("nonConcurrentJobs", endMessage);
        }
    }
}
