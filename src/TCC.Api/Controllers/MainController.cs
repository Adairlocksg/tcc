﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using TCC.Api.Base;
using TCC.Business.Base;

namespace TCC.Api.Controllers
{
    //[ApiController]
    public abstract class MainController : ControllerBase
    {
        protected async Task<IActionResult> Execute<T>(Func<Task<Result<T>>> action)
        {
            if (!ModelState.IsValid)
                return BadRequest(new Response("400", false, GetErrorMessagesFromModelState(ModelState)));

            var result = await action();

            return result.IsSuccess
                ? Ok(new Response("200", true, string.Empty, result.Value))
                : BadRequest(new Response(result.Error.Code, false, result.Error.Message));
        }

        protected async Task<IActionResult> Execute(Func<Task<Result>> action)
        {
            if (!ModelState.IsValid)
                return BadRequest(new Response("400", false, GetErrorMessagesFromModelState(ModelState)));

            var result = await action();

            return result.IsSuccess
                ? Ok(new Response("200", true, string.Empty))
                : BadRequest(new Response(result.Error.Code, false, result.Error.Message));
        }

        private static List<string> GetErrorMessagesFromModelState(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors)
                                          .Select(e => e.Exception is null ? e.ErrorMessage : e.Exception.Message);

            return [.. errors];
        }
    }
}
