using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TranslationManagement.Api.Controllers
{
    [ApiController]
    [Route("api/TranslatorsManagement/[action]")]
    public class TranslatorManagementController(IUnitOfWork unitOfWork, ILogger<TranslatorManagementController> logger)
        : ControllerBase
    {
        public static readonly string[] TranslatorStatuses = { "Applicant", "Certified", "Deleted" };


        [HttpGet]
        public Translator[] GetTranslators()
        {
            return unitOfWork.Translators.GetAllAsync().Result.ToArray();
        }

        [HttpGet]
        public Translator[] GetTranslatorsByName(string name)
        {
            return unitOfWork.Translators.FindByNameAsync(name).Result.ToArray();
        }

        [HttpPost]
        public bool AddTranslator(Translator translator)
        {
            unitOfWork.Translators.Add(translator);
            return unitOfWork.Commit().Result;
        }

        // todo change retval to OK or not ok
        [HttpPost]
        public string UpdateTranslatorStatus(int translatorId, string newStatus = "")
        {
            logger.LogInformation("User status update request: " + newStatus + " for user " + translatorId.ToString());
            // todo change status to enum
            if (TranslatorStatuses.Where(status => status == newStatus).Count() == 0)
            {
                throw new ArgumentException("unknown status");
            }

            var translator = unitOfWork.Translators.GetByIdAsync(translatorId).Result;
            if (translator == null)
            {
                throw new KeyNotFoundException("Translator not found");
            }

            translator.Status = newStatus;
            unitOfWork.Translators.Update(translator);
            return unitOfWork.Commit().Result ? "updated" : "failed";

        }
    }
}
