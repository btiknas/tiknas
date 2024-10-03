using Tiknas.Content;

namespace Tiknas.TestApp.Application.Dto;

public class CreateFileInput
{
    public string Name { get; set; }

    public IRemoteStreamContent Content { get; set; }
}
