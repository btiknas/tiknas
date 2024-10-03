using Microsoft.Extensions.Localization;
using Shouldly;
using Tiknas.Localization.TestResources.Base.Validation;
using Tiknas.TestBase;
using Xunit;

namespace Tiknas.Localization;

public class TiknasEnumLocalizer_Tests : TiknasIntegratedTest<TiknasLocalizationTestModule>
{
    private readonly ITiknasEnumLocalizer _enumLocalizer;

    public TiknasEnumLocalizer_Tests()
    {
        _enumLocalizer = GetRequiredService<ITiknasEnumLocalizer>();
    }

    [Fact]
    public void GetString_Test()
    {
        using (CultureHelper.Use("en"))
        {
            _enumLocalizer.GetString<BookType>(BookType.Undefined).ShouldBe("Undefined");
            _enumLocalizer.GetString<BookType>(BookType.Adventure).ShouldBe("Adventure");
            _enumLocalizer.GetString<BookType>(0).ShouldBe("Undefined with value 0");
            _enumLocalizer.GetString<BookType>(1).ShouldBe("Adventure with value 1");
            _enumLocalizer.GetString<BookType>(BookType.Biography).ShouldBe("Biography");

            var specifyLocalizer = new[]
            {
                GetRequiredService<IStringLocalizerFactory>().Create<LocalizationTestValidationResource>()
            };
            _enumLocalizer.GetString<BookType>(BookType.Undefined, specifyLocalizer).ShouldBe("Undefined from ValidationResource");
            _enumLocalizer.GetString<BookType>(BookType.Adventure, specifyLocalizer).ShouldBe("Adventure from ValidationResource");
            _enumLocalizer.GetString<BookType>(0, specifyLocalizer).ShouldBe("Undefined with value 0 from ValidationResource");
            _enumLocalizer.GetString<BookType>(1, specifyLocalizer).ShouldBe("Adventure with value 1 from ValidationResource");
            _enumLocalizer.GetString<BookType>(BookType.Biography, specifyLocalizer).ShouldBe("Biography from ValidationResource");
        }
    }
}

enum BookType
{
    Undefined,
    Adventure,
    Biography,
}
