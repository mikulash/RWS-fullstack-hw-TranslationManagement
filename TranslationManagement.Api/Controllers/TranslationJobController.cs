﻿using System.Collections.Generic;
using BusinessLayer.Dtos;
using BusinessLayer.Enums;
using BusinessLayer.Services;
using External.ThirdParty.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TranslationManagement.Api.Controllers
{
    [ApiController]
    [Route("api/jobs/[action]")]
    public class TranslationJobController(
        TranslationJobService translationJobService,
        ILogger<TranslatorManagementController> logger)
        : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TranslationJobDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetJobs()
        {
            var jobs = translationJobService.GetJobs();
            if (jobs.Length == 0)
            {
                return NotFound();
            }

            return Ok(jobs);
        }


        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<TranslationJobDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateJob(CreateTranslationJobDto job)
        {
            var retval = translationJobService.CreateTranslationJob(job);
            if (retval)
            {
                var notificationSvc = new UnreliableNotificationService();
                while (!notificationSvc.SendNotification("Job created: " + job.Id).Result)
                {
                    // todo fix this retry logic
                }

                logger.LogInformation("New job notification sent");
                return Ok();
            }

            return BadRequest();

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateJobWithFile(IFormFile file, string customer)
        {
            var result = translationJobService.CreateJobWithFile(file, customer);
            return result ? Ok() : BadRequest();

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
}
