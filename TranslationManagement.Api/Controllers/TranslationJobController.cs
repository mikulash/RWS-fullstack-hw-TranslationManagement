using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using BusinessLayer.Dtos;
using BusinessLayer.Enums;
using BusinessLayer.Services;
using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.UnitOfWork;
using External.ThirdParty.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TranslationManagement.Api.Enums;

namespace TranslationManagement.Api.Controllers
{
    [ApiController]
    [Route("api/jobs/[action]")]
    public class TranslationJobController(TranslationJobService translationJobService, ILogger<TranslatorManagementController> logger)
        : ControllerBase
    {
        [HttpGet]
        public TranslationJobDto[] GetJobs()
        {
            return translationJobService.GetJobs();
        }


        [HttpPost]
        public bool CreateJob(CreateTranslationJobDto job)
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
            }

            return retval;
        }

        [HttpPost]
        public bool CreateJobWithFile(IFormFile file, string customer)
        {
            var result = translationJobService.CreateJobWithFile(file, customer);
            return result;

        }

        [HttpPost]
        public string UpdateJobStatus(int jobId, int translatorId, JobStatus newStatus = JobStatus.New)
        {
            logger.LogInformation("Job status update request received: " + newStatus + " for job " + jobId + " by translator " + translatorId);

            var result = translationJobService.UpdateJobStatus(jobId, newStatus);
            return result ? "updated" : "not found";

        }
    }
}
