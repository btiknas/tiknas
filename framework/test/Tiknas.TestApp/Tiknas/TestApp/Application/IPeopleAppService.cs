using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiknas.Application.Dtos;
using Tiknas.Application.Services;
using Tiknas.Content;
using Tiknas.TestApp.Application.Dto;

namespace Tiknas.TestApp.Application;

public interface IPeopleAppService : ICrudAppService<PersonDto, Guid>
{
    Task<ListResultDto<PhoneDto>> GetPhones(Guid id, GetPersonPhonesFilter filter);

    Task<List<string>> GetParams(IEnumerable<Guid> ids, string[] names);

    Task<PhoneDto> AddPhone(Guid id, PhoneDto phoneDto);

    Task RemovePhone(Guid id, string number);

    Task GetWithAuthorized();

    Task<GetWithComplexTypeInput> GetWithComplexType(GetWithComplexTypeInput input);

    Task<IRemoteStreamContent> DownloadAsync();

    Task<string> UploadAsync(IRemoteStreamContent streamContent);

    Task<string> UploadMultipleAsync(IEnumerable<IRemoteStreamContent> streamContents);

    Task<string> CreateFileAsync(CreateFileInput input);

    Task<string> CreateMultipleFileAsync(CreateMultipleFileInput input);

    Task<string> GetParamsFromQueryAsync(GetParamsInput input);

    Task<string> GetParamsFromFormAsync(GetParamsInput input);
}
