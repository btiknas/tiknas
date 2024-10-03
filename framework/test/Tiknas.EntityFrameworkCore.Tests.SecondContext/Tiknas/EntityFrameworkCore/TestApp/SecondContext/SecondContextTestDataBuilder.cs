using System;
using System.Threading.Tasks;
using Tiknas.DependencyInjection;
using Tiknas.Domain.Repositories;
using Tiknas.Guids;

namespace Tiknas.EntityFrameworkCore.TestApp.SecondContext;

public class SecondContextTestDataBuilder : ITransientDependency
{
    private readonly IBasicRepository<BookInSecondDbContext, Guid> _bookRepository;
    private readonly IGuidGenerator _guidGenerator;

    public SecondContextTestDataBuilder(IBasicRepository<BookInSecondDbContext, Guid> bookRepository, IGuidGenerator guidGenerator)
    {
        _bookRepository = bookRepository;
        _guidGenerator = guidGenerator;
    }

    public async Task BuildAsync()
    {
        await _bookRepository.InsertAsync(
            new BookInSecondDbContext(
                _guidGenerator.Create(),
                "TestBook1"
            )
        );
    }
}
