using System;
using System.Collections.Generic;
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
    public IActionResult GetJobs()
    {
        var jobs = translationJobService.GetJobs();
        if (jobs.Length == 0) return NotFound();

        return Ok(jobs);
    }


    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<TranslationJobDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateJob(CreateTranslationJobDto job)
    {
        var retval = translationJobService.CreateTranslationJob(job);
        if (!retval) return BadRequest();

        var notificationSvc = new UnreliableNotificationService();
        const int maxRetries = 3;
        var attempts = 0;
        while (attempts < maxRetries)
        {
            try
            {
                var result = notificationSvc.SendNotification("Job created for customer: " + job.CustomerName).Result;
                if (result) return Ok();
            }
            catch (ApplicationException ex)
            {
                logger.LogError(ex, "Attempt {Attempt} failed with error: {ErrorMessage}", attempts + 1,
                    ex.Message);
            }

            attempts++;
        }

        return Ok();

    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateJobWithFile(IFormFile file, string customer)
    {
        try
        {
            var result = translationJobService.CreateJobWithFile(file, customer);
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
    public IActionResult UpdateJobStatus(int jobId, int translatorId, JobStatus newStatus = JobStatus.New)
    {
        logger.LogInformation(
            "Job status update request received: {NewStatus} for job {JobId} by translator {TranslatorId}",
            newStatus, jobId, translatorId);
        var result = translationJobService.UpdateJobStatus(jobId, newStatus);
        return result ? Ok() : BadRequest();

    }
}
