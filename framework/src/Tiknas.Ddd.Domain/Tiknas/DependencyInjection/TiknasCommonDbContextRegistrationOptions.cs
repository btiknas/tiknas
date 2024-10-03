using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.Domain.Entities;
using Tiknas.Domain.Repositories;
using Tiknas.MultiTenancy;

namespace Tiknas.DependencyInjection;

/// <summary>
/// This is a base class for dbcoUse derived
/// </summary>
public abstract class TiknasCommonDbContextRegistrationOptions : ITiknasCommonDbContextRegistrationOptionsBuilder
{
    public Type OriginalDbContextType { get; }

    public IServiceCollection Services { get; }

    public Dictionary<MultiTenantDbContextType, Type?> ReplacedDbContextTypes { get; }

    public Type DefaultRepositoryDbContextType { get; protected set; }

    public Type? DefaultRepositoryImplementationType { get; private set; }

    public Type? DefaultRepositoryImplementationTypeWithoutKey { get; private set; }

    public bool RegisterDefaultRepositories { get; private set; }

    public bool IncludeAllEntitiesForDefaultRepositories { get; private set; }

    public Dictionary<Type, Type> CustomRepositories { get; }

    public List<Type> SpecifiedDefaultRepositories { get; }

    public bool SpecifiedDefaultRepositoryTypes => DefaultRepositoryImplementationType != null && DefaultRepositoryImplementationTypeWithoutKey != null;

    protected TiknasCommonDbContextRegistrationOptions(Type originalDbContextType, IServiceCollection services)
    {
        OriginalDbContextType = originalDbContextType;
        Services = services;
        DefaultRepositoryDbContextType = originalDbContextType;
        CustomRepositories = new Dictionary<Type, Type>();
        ReplacedDbContextTypes = new Dictionary<MultiTenantDbContextType, Type?>();
        SpecifiedDefaultRepositories = new List<Type>();
    }

    public ITiknasCommonDbContextRegistrationOptionsBuilder ReplaceDbContext<TOtherDbContext>(MultiTenancySides multiTenancySides = MultiTenancySides.Both)
    {
        return ReplaceDbContext(typeof(TOtherDbContext), multiTenancySides: multiTenancySides);
    }

    public ITiknasCommonDbContextRegistrationOptionsBuilder ReplaceDbContext<TOtherDbContext, TTargetDbContext>(MultiTenancySides multiTenancySides = MultiTenancySides.Both)
    {
        return ReplaceDbContext(typeof(TOtherDbContext), typeof(TTargetDbContext), multiTenancySides);
    }

    public ITiknasCommonDbContextRegistrationOptionsBuilder ReplaceDbContext(Type otherDbContextType, Type? targetDbContextType = null, MultiTenancySides multiTenancySides = MultiTenancySides.Both)
    {
        if (!otherDbContextType.IsAssignableFrom(OriginalDbContextType))
        {
            throw new TiknasException($"{OriginalDbContextType.AssemblyQualifiedName} should inherit/implement {otherDbContextType.AssemblyQualifiedName}!");
        }

        ReplacedDbContextTypes[new MultiTenantDbContextType(otherDbContextType, multiTenancySides)] = targetDbContextType;

        return this;
    }

    public ITiknasCommonDbContextRegistrationOptionsBuilder AddDefaultRepositories(bool includeAllEntities = false)
    {
        RegisterDefaultRepositories = true;
        IncludeAllEntitiesForDefaultRepositories = includeAllEntities;

        return this;
    }

    public ITiknasCommonDbContextRegistrationOptionsBuilder AddDefaultRepositories(Type defaultRepositoryDbContextType, bool includeAllEntities = false)
    {
        if (!defaultRepositoryDbContextType.IsAssignableFrom(OriginalDbContextType))
        {
            throw new TiknasException($"{OriginalDbContextType.AssemblyQualifiedName} should inherit/implement {defaultRepositoryDbContextType.AssemblyQualifiedName}!");
        }

        DefaultRepositoryDbContextType = defaultRepositoryDbContextType;

        return AddDefaultRepositories(includeAllEntities);
    }

    public ITiknasCommonDbContextRegistrationOptionsBuilder AddDefaultRepositories<TDefaultRepositoryDbContext>(bool includeAllEntities = false)
    {
        return AddDefaultRepositories(typeof(TDefaultRepositoryDbContext), includeAllEntities);
    }

    public ITiknasCommonDbContextRegistrationOptionsBuilder AddDefaultRepository<TEntity>()
    {
        return AddDefaultRepository(typeof(TEntity));
    }

    public ITiknasCommonDbContextRegistrationOptionsBuilder AddDefaultRepository(Type entityType)
    {
        EntityHelper.CheckEntity(entityType);

        SpecifiedDefaultRepositories.AddIfNotContains(entityType);

        return this;
    }

    public ITiknasCommonDbContextRegistrationOptionsBuilder AddRepository<TEntity, TRepository>()
    {
        AddCustomRepository(typeof(TEntity), typeof(TRepository));

        return this;
    }

    public ITiknasCommonDbContextRegistrationOptionsBuilder SetDefaultRepositoryClasses(
        Type repositoryImplementationType,
        Type repositoryImplementationTypeWithoutKey
        )
    {
        Check.NotNull(repositoryImplementationType, nameof(repositoryImplementationType));
        Check.NotNull(repositoryImplementationTypeWithoutKey, nameof(repositoryImplementationTypeWithoutKey));

        DefaultRepositoryImplementationType = repositoryImplementationType;
        DefaultRepositoryImplementationTypeWithoutKey = repositoryImplementationTypeWithoutKey;

        return this;
    }

    private void AddCustomRepository(Type entityType, Type repositoryType)
    {
        if (!typeof(IEntity).IsAssignableFrom(entityType))
        {
            throw new TiknasException($"Given entityType is not an entity: {entityType.AssemblyQualifiedName}. It must implement {typeof(IEntity<>).AssemblyQualifiedName}.");
        }

        if (!typeof(IRepository).IsAssignableFrom(repositoryType))
        {
            throw new TiknasException($"Given repositoryType is not a repository: {entityType.AssemblyQualifiedName}. It must implement {typeof(IBasicRepository<>).AssemblyQualifiedName}.");
        }

        CustomRepositories[entityType] = repositoryType;
    }
}
