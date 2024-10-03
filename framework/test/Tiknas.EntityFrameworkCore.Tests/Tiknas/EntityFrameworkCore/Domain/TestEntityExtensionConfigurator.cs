using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiknas.ObjectExtending;
using Tiknas.TestApp.Domain;
using Tiknas.TestApp.EntityFrameworkCore;
using Tiknas.Threading;

namespace Tiknas.EntityFrameworkCore.Domain;

public static class TestEntityExtensionConfigurator
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
            ObjectExtensionManager.Instance
                .MapEfCoreProperty<City, string>(
                    "PhoneCode",
                    (e, p) =>
                    {
                        e.HasIndex(p.Metadata.Name).IsUnique();
                        p.HasMaxLength(8);
                    }
                ).MapEfCoreProperty<City, string>(
                    "ZipCode"
                ).MapEfCoreProperty<City, int>(
                    "Rank"
                ).MapEfCoreProperty<City, DateTime?>(
                    "Established"
                ).MapEfCoreProperty<City, Guid>(
                    "Guid"
                ).MapEfCoreProperty<City, ExtraProperties_Tests.Color?>(
                    "EnumNumber"
                ).MapEfCoreProperty<City, ExtraProperties_Tests.Color>(
                    "EnumNumberString"
                ).MapEfCoreProperty<City, ExtraProperties_Tests.Color>(
                    "EnumLiteral"
                ).MapEfCoreEntity<City>(b =>
                {
                    b.As<EntityTypeBuilder<City>>()
                        .Property(x => x.Name).IsRequired();

                }).MapEfCoreEntity<City>(b =>
                {
                    b.As<EntityTypeBuilder<City>>()
                        .Property(x => x.Name).HasMaxLength(200);

                }).MapEfCoreEntity(typeof(Person), b =>
                {
                    b.As<EntityTypeBuilder<Person>>()
                        .HasIndex(x => x.Birthday);
                });

            ObjectExtensionManager.Instance.MapEfCoreDbContext<TestAppDbContext>(b =>
            {
                b.Entity<City>().Property(x => x.Name).IsRequired();
            });

            ObjectExtensionManager.Instance.MapEfCoreDbContext<TestAppDbContext>(b =>
            {
                b.Entity<Author>().Property(x => x.Name).IsRequired();
            });
        });
    }
}
