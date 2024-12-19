using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Dtos;
using BusinessLayer.Services;
using DataAccessLayer.Enums;
using External.ThirdParty.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TranslationManagement.Api.Controllers;

[ApiController]
[Route("api/jobs/[action]")]
public class TranslationJobController(
    ITranslationJobService translationJobService,
    ILogger<TranslatorManagementController> logger)
    : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TranslationJobDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetJobs()
    {
        var jobs = await translationJobService.GetJobsAsync();
        if (!jobs.Any()) return NotFound();

        return Ok(jobs);
    }

    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<TranslationJobDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateJob(CreateTranslationJobDto job)
    {
        var retval = await translationJobService.CreateTranslationJobAsync(job);
        if (!retval) return BadRequest();

        _ = Task.Run(async () => { await NotifyJobCreationAsync(job.CustomerName); });

        return Ok();
    }

    private async Task NotifyJobCreationAsync(string customerName, int maxRetries = 5)
    {
        var notificationSvc = new UnreliableNotificationService();
        var attempts = 0;
        while (attempts < maxRetries)
        {
            try
            {
                var result = await notificationSvc.SendNotification("Job created for customer: " + customerName);
                if (result)
                {
                    logger.LogInformation("Notification sent successfully");
                    return;
                }
            }
            catch (ApplicationException ex)
            {
                logger.LogError(ex, "Attempt {Attempt} failed with error: {ErrorMessage}", attempts + 1, ex.Message);
            }

            attempts++;
        }

        logger.LogError("Failed to send notification after {MaxRetries} attempts", maxRetries);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateJobWithFile(IFormFile file, string customer)
    {
        try
        {
            if (file.Length == 0) return BadRequest("File is empty");

            var result = await translationJobService.CreateJobWithFileAsync(file, customer);
            return result ? Ok() : BadRequest();
        }
        catch (InvalidOperationException e)
        {
            logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
        catch (NotSupportedException e)
        {
            logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateJobStatus(int jobId, JobStatus newStatus = JobStatus.New)
    {
        logger.LogInformation("Job status update request received: {NewStatus} for job {JobId}", newStatus, jobId);
        var result = await translationJobService.UpdateJobStatusAsync(jobId, newStatus);
        return result ? Ok() : BadRequest();
    }
}
