﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tiknas.DependencyInjection;
using Tiknas.Http;
using Tiknas.Json;

namespace Tiknas.Cli.ProjectBuilding;

public class RemoteServiceExceptionHandler : IRemoteServiceExceptionHandler, ITransientDependency
{
    private readonly IJsonSerializer _jsonSerializer;

    public RemoteServiceExceptionHandler(IJsonSerializer jsonSerializer)
    {
        _jsonSerializer = jsonSerializer;
    }

    public async Task EnsureSuccessfulHttpResponseAsync(HttpResponseMessage responseMessage)
    {
        if (responseMessage == null)
        {
            return;
        }

        if (responseMessage.IsSuccessStatusCode)
        {
            return;
        }

        var exceptionMessage = "Remote server returns '" + (int)responseMessage.StatusCode + "-" +
                               responseMessage.ReasonPhrase + "'. ";

        var remoteServiceErrorMessage = await GetTiknasRemoteServiceErrorAsync(responseMessage);
        if (remoteServiceErrorMessage != null)
        {
            exceptionMessage += remoteServiceErrorMessage;
        }

        throw new Exception(exceptionMessage);
    }

    public async Task<string> GetTiknasRemoteServiceErrorAsync(HttpResponseMessage responseMessage)
    {
        RemoteServiceErrorResponse errorResult;
        try
        {
            errorResult = _jsonSerializer.Deserialize<RemoteServiceErrorResponse>
            (
                await responseMessage.Content.ReadAsStringAsync()
            );
        }
        catch (JsonReaderException)
        {
            return null;
        }

        if (errorResult?.Error == null)
        {
            return null;
        }

        var sbError = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(errorResult.Error.Code))
        {
            sbError.Append("Code: " + errorResult.Error.Code);
        }

        if (!string.IsNullOrWhiteSpace(errorResult.Error.Message))
        {
            if (sbError.Length > 0)
            {
                sbError.Append(" - ");
            }

            sbError.Append("Message: " + errorResult.Error.Message);
        }

        if (errorResult.Error.ValidationErrors != null && errorResult.Error.ValidationErrors.Any())
        {
            if (sbError.Length > 0)
            {
                sbError.Append(" - ");
            }

            sbError.AppendLine("Validation Errors: ");
            for (var i = 0; i < errorResult.Error.ValidationErrors.Length; i++)
            {
                var validationError = errorResult.Error.ValidationErrors[i];
                sbError.AppendLine("Validation error #" + i + ": " + validationError.Message + " - Members: " +
                                   validationError.Members.JoinAsString(", ") + ".");
            }
        }

        return sbError.ToString();
    }
}
