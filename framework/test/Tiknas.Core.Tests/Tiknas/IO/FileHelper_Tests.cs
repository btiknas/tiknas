using Shouldly;
using Xunit;

namespace Tiknas.IO;

public class FileHelper_Tests
{
    [Fact]
    public void GetExtension()
    {
        FileHelper.GetExtension("test").ShouldBeNull();
        FileHelper.GetExtension("te.st").ShouldBe("st");
    }
}
