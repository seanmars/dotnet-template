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

        var jobKey = new JobKey("HelloWorldJob");

        // Check if the job already exists
        if (await scheduler.CheckExists(jobKey))
        {
            return BadRequest("Job already exists");
        }

        // Create a JobKey for the HelloWorldJob
        var jobDetail = JobBuilder.Create<HelloWorldJob>()
            .WithIdentity(jobKey)
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