using Shouldly;
using Tiknas.TestBase;
using Xunit;

namespace Tiknas.Security.Encryption;

public class StringEncryptionService_Tests : TiknasIntegratedTest<TiknasSecurityTestModule>
{
    private readonly IStringEncryptionService _stringEncryptionService;

    public StringEncryptionService_Tests()
    {
        _stringEncryptionService = GetRequiredService<IStringEncryptionService>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("This is a plain text!")]
    public void Should_Enrypt_And_Decrpyt_With_Default_Options(string plainText)
    {
        _stringEncryptionService
            .Decrypt(_stringEncryptionService.Encrypt(plainText))
            .ShouldBe(plainText);
    }
}
