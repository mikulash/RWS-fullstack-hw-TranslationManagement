using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Dtos;
using BusinessLayer.Services;
using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TranslationManagement.Api.Controllers
{
    [ApiController]
    [Route("api/TranslatorsManagement/[action]")]
    public class TranslatorManagementController(ITranslatorService translatorService, ILogger<TranslatorManagementController> logger)
        : ControllerBase
    {
        public static readonly string[] TranslatorStatuses = { "Applicant", "Certified", "Deleted" };


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Translator>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTranslators()
        {
            var translators = translatorService.GetAllTranslators();
            if(translators.Any())
            {
                return Ok(translators);
            }
            return NotFound();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Translator>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTranslatorsByName(string name)
        {
            var translators = translatorService.GetTranslatorsByName(name);
            if (translators.Any())
            {
                return Ok(translators);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult AddTranslator(TranslatorDto translator)
        {
            var result = translatorService.AddTranslator(translator);
            if (result)
            {
                return Ok();
            }
            return BadRequest();

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
