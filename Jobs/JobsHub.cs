using Microsoft.AspNetCore.SignalR;

namespace NanoCorpWebAppMVCNoAuth.Jobs
{
    public class JobsHub : Hub
    {
        //message used for concurrent job message
        public Task SendConcurrentJobsMessage(string message)
        {
            return Clients.All.SendAsync("ConcurrentJobs", message);
        }

        //message used for nonconcurrent job message
        public Task SendNonConcurrentJobsMessage(string message)
        {
            return Clients.All.SendAsync("NonConcurrentJobs", message);
        }

    }
}
