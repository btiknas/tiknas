using System;
using System.Threading.Tasks;
using Shouldly;
using Tiknas.Data;
using Tiknas.Domain.Repositories;
using Tiknas.TestApp.Domain;
using Tiknas.TestApp.Testing;
using Xunit;

namespace Tiknas.EntityFrameworkCore.DataFiltering;

public class EfCore_Custom_Filter_Tests : TestAppTestBase<TiknasEntityFrameworkCoreTestModule>
{
    private readonly IBasicRepository<Category, Guid> _categoryRepository;

    public EfCore_Custom_Filter_Tests()
    {
        _categoryRepository = GetRequiredService<IBasicRepository<Category, Guid>>();
    }

    [Fact]
    public async Task Should_Combine_Tiknas_And_Custom_QueryFilter_Test()
    {
        var categories = await _categoryRepository.GetListAsync();
        categories.Count.ShouldBe(2);
        categories[0].Name.ShouldBe("tiknas.cli");

        using (GetRequiredService<IDataFilter<ISoftDelete>>().Disable())
        {
            categories = await _categoryRepository.GetListAsync();
            categories.Count.ShouldBe(3);
            categories.ShouldContain(x => x.Name == "tiknas.cli" && x.IsDeleted == false);
            categories.ShouldContain(x => x.Name == "tiknas.core" && x.IsDeleted == true);
        }
    }
}
