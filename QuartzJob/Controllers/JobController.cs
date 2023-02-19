using Microsoft.AspNetCore.Mvc;
using Quartz;
using QuartzJob.Jobs;

namespace QuartzJob.Controllers;

[ApiController]
[Route("[controller]")]
public class JobController : ControllerBase
{
    private readonly ILogger<JobController> _logger;
    private readonly ISchedulerFactory _factory;

    public JobController(ILogger<JobController> logger, ISchedulerFactory factory)
    {
        _logger = logger;
        _factory = factory;
    }

    [HttpGet("insert")]
    public async Task<IActionResult> AddJob()
    {
        var scheduler = await _factory.GetScheduler();

        // Create a JobKey for the HelloWorldJob
        var jobDetail = JobBuilder.Create<HelloWorldJob>()
            .WithIdentity("HelloWorldJob")
            .Build();

        // Create a trigger for the job
        var trigger = TriggerBuilder.Create()
            .WithIdentity("HelloWorldTrigger")
            .StartNow()
            .WithSimpleSchedule(x => x.WithIntervalInSeconds(10).RepeatForever())
            .Build();

        // Schedule the job with the trigger
        await scheduler.ScheduleJob(jobDetail, trigger);

        return Ok();
    }
}