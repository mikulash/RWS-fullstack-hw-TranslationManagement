﻿using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Dtos;
using BusinessLayer.Services;
using DataAccessLayer.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TranslationManagement.Api.Controllers;

[ApiController]
[Route("api/TranslatorsManagement/[action]")]
public class TranslatorManagementController(
    ITranslatorService translatorService,
    ILogger<TranslatorManagementController> logger)
    : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TranslatorDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetTranslators()
    {
        var translators = translatorService.GetAllTranslators();
        if (translators.Any()) return Ok(translators);

        return NotFound();
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TranslatorDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetTranslatorsByName(string name)
    {
        var translators = translatorService.GetTranslatorsByName(name);
        if (translators.Any()) return Ok(translators);

        return NotFound();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AddTranslator(CreateTranslatorDto translator)
    {
        var result = translatorService.AddTranslator(translator);
        if (result) return Ok();

        return BadRequest();

    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateTranslatorStatus(int translatorId, TranslatorStatus newStatus)
    {
        logger.LogInformation("User status update request: {NewStatus} for user {TranslatorId}", newStatus,
            translatorId);
        var result = translatorService.UpdateTranslatorStatus(translatorId, newStatus);
        return result ? Ok() : BadRequest();
    }
}
