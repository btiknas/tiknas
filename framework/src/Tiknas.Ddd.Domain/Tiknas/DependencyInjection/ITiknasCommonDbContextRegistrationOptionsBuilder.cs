using System;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.MultiTenancy;

namespace Tiknas.DependencyInjection;

public interface ITiknasCommonDbContextRegistrationOptionsBuilder
{
    IServiceCollection Services { get; }

    /// <summary>
    /// Registers default repositories for all the entities in this DbContext.
    /// </summary>
    /// <param name="includeAllEntities">
    /// Registers repositories only for aggregate root entities by default.
    /// Set <paramref name="includeAllEntities"/> to true to include all entities.
    /// </param>
    ITiknasCommonDbContextRegistrationOptionsBuilder AddDefaultRepositories(bool includeAllEntities = false);

    /// <summary>
    /// Registers default repositories for all the entities in this DbContext.
    /// Default repositories will use the provided DbContext type.
    /// </summary>
    /// <typeparam name="TDefaultRepositoryDbContext">DbContext type that will be used by default repositories</typeparam>
    /// <param name="includeAllEntities">
    /// Registers repositories only for aggregate root entities by default.
    /// Set <c>includeAllEntities</c> to true to include all entities.
    /// </param>
    ITiknasCommonDbContextRegistrationOptionsBuilder AddDefaultRepositories<TDefaultRepositoryDbContext>(bool includeAllEntities = false);

    /// <summary>
    /// Registers default repositories for all the entities in this DbContext.
    /// Default repositories will use the provided DbContext type.
    /// </summary>
    /// <param name="defaultRepositoryDbContextType">DbContext type that will be used by default repositories</param>
    /// <param name="includeAllEntities">
    /// Registers repositories only for aggregate root entities by default.
    /// Set <c>includeAllEntities</c> to true to include all entities.
    /// </param>
    ITiknasCommonDbContextRegistrationOptionsBuilder AddDefaultRepositories(Type defaultRepositoryDbContextType, bool includeAllEntities = false);

    /// <summary>
    /// Registers default repository for a specific entity.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    ITiknasCommonDbContextRegistrationOptionsBuilder AddDefaultRepository<TEntity>();

    /// <summary>
    /// Registers default repository for a specific entity.
    /// </summary>
    /// <param name="entityType"></param>
    /// <returns></returns>
    ITiknasCommonDbContextRegistrationOptionsBuilder AddDefaultRepository(Type entityType);

    /// <summary>
    /// Registers custom repository for a specific entity.
    /// Custom repositories overrides default repositories.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TRepository">Repository type</typeparam>
    ITiknasCommonDbContextRegistrationOptionsBuilder AddRepository<TEntity, TRepository>();

    /// <summary>
    /// Uses given class(es) for default repositories.
    /// </summary>
    /// <param name="repositoryImplementationType">Repository implementation type</param>
    /// <param name="repositoryImplementationTypeWithoutKey">Repository implementation type (without primary key)</param>
    /// <returns></returns>
    ITiknasCommonDbContextRegistrationOptionsBuilder SetDefaultRepositoryClasses([NotNull] Type repositoryImplementationType, [NotNull] Type repositoryImplementationTypeWithoutKey);

    /// <summary>
    /// Replaces given DbContext type with this DbContext type.
    /// </summary>
    /// <typeparam name="TOtherDbContext">The DbContext type to be replaced</typeparam>
    /// <param name="multiTenancySides">MultiTenancy side</param>
    ITiknasCommonDbContextRegistrationOptionsBuilder ReplaceDbContext<TOtherDbContext>(MultiTenancySides multiTenancySides = MultiTenancySides.Both);

    /// <summary>
    /// Replaces given DbContext type with the target DbContext type.
    /// </summary>
    /// <typeparam name="TOtherDbContext">The DbContext type to be replaced</typeparam>
    /// <typeparam name="TTargetDbContext">The target DbContext type</typeparam>
    /// <param name="multiTenancySides">MultiTenancy side</param>
    ITiknasCommonDbContextRegistrationOptionsBuilder ReplaceDbContext<TOtherDbContext, TTargetDbContext>(MultiTenancySides multiTenancySides = MultiTenancySides.Both);

    /// <summary>
    /// Replaces given DbContext type with the given or this DbContext type.
    /// </summary>
    /// <param name="otherDbContextType">The DbContext type to be replaced</param>
    /// <param name="targetDbContextType">The target DbContext type (optional, used this DbContext type if not provided)</param>
    /// <param name="multiTenancySides">MultiTenancy side</param>
    ITiknasCommonDbContextRegistrationOptionsBuilder ReplaceDbContext(Type otherDbContextType, Type? targetDbContextType = null, MultiTenancySides multiTenancySides = MultiTenancySides.Both);
}
