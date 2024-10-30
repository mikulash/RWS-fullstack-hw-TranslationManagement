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
    public class TranslatorManagementController : ControllerBase
    {


        public static readonly string[] TranslatorStatuses = { "Applicant", "Certified", "Deleted" };

        private readonly ILogger<TranslatorManagementController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public TranslatorManagementController(IUnitOfWork unitOfWork, ILogger<TranslatorManagementController> logger)
        {
           _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public Translator[] GetTranslators()
        {
            return _unitOfWork.Translators.GetAllAsync().Result.ToArray();
        }

        [HttpGet]
        public Translator[] GetTranslatorsByName(string name)
        {
            return _unitOfWork.Translators.FindByNameAsync(name).Result.ToArray();
        }

        [HttpPost]
        public bool AddTranslator(Translator translator)
        {
            _unitOfWork.Translators.Add(translator);
            return _unitOfWork.Commit().Result;
        }

        // todo change status to enum
        // todo change retval to OK or not ok
        [HttpPost]
        public string UpdateTranslatorStatus(int Translator, string newStatus = "")
        {
            _logger.LogInformation("User status update request: " + newStatus + " for user " + Translator.ToString());
            // if (TranslatorStatuses.Where(status => status == newStatus).Count() == 0)
            // {
            //     throw new ArgumentException("unknown status");
            // }

            var translator = _unitOfWork.Translators.GetByIdAsync(Translator).Result;
            if (translator == null)
            {
                throw new KeyNotFoundException("Translator not found");
            }
            translator.Status = newStatus;
            _unitOfWork.Translators.Update(translator);
            return _unitOfWork.Commit().Result ? "updated" : "failed";

        }
    }
}
