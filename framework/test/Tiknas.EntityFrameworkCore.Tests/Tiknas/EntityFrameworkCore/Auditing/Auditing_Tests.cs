﻿using System;
using System.Threading.Tasks;
using Shouldly;
using Tiknas.TestApp;
using Tiknas.TestApp.Testing;
using Xunit;

namespace Tiknas.EntityFrameworkCore.Auditing;

public class Auditing_Tests : Auditing_Tests<TiknasEntityFrameworkCoreTestModule>
{
    [Fact]
    public async Task Should_Not_Set_Modification_If_Properties_Generated_By_Database()
    {
        await WithUnitOfWorkAsync((async () =>
        {
            var douglas = await PersonRepository.GetAsync(TestDataBuilder.UserDouglasId);
            douglas.LastActiveTime = DateTime.Now;
        }));

        await WithUnitOfWorkAsync((async () =>
        {
            var douglas = await PersonRepository.FindAsync(TestDataBuilder.UserDouglasId);

            douglas.ShouldNotBeNull();
            douglas.LastModificationTime.ShouldBeNull();
            douglas.LastModificationTime.ShouldBeNull();
            douglas.LastModifierId.ShouldBeNull();
        }));
    }

    [Fact]
    public async Task Should_Set_Modification_If_Properties_Changed_With_Default_Value()
    {
        var date = DateTime.Parse("2022-01-01");
        await WithUnitOfWorkAsync((async () =>
        {
            var douglas = await PersonRepository.GetAsync(TestDataBuilder.UserDouglasId);
            douglas.HasDefaultValue = date;
        }));

        await WithUnitOfWorkAsync((async () =>
        {
            var douglas = await PersonRepository.FindAsync(TestDataBuilder.UserDouglasId);

            douglas.ShouldNotBeNull();
            douglas.HasDefaultValue.ShouldBe(date);
            douglas.LastModificationTime.ShouldNotBeNull();
            douglas.LastModificationTime.Value.ShouldBeLessThanOrEqualTo(Clock.Now);
            douglas.LastModifierId.ShouldBe(CurrentUserId);
        }));
    }

    [Fact]
    public async Task Should_Set_Modification_If_Properties_Not_Generated_By_Database()
    {
        await WithUnitOfWorkAsync((async () =>
        {
            var douglas = await PersonRepository.GetAsync(TestDataBuilder.UserDouglasId);
            douglas.LastActiveTime = DateTime.Now;
            douglas.Age = 100;
        }));

        await WithUnitOfWorkAsync((async () =>
        {
            var douglas = await PersonRepository.FindAsync(TestDataBuilder.UserDouglasId);

            douglas.ShouldNotBeNull();
            douglas.LastModificationTime.ShouldNotBeNull();
            douglas.LastModificationTime.Value.ShouldBeLessThanOrEqualTo(Clock.Now);
            douglas.LastModifierId.ShouldBe(CurrentUserId);
        }));
    }
}
