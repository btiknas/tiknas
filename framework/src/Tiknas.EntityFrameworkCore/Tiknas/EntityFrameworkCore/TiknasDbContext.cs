using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Tiknas.Auditing;
using Tiknas.Data;
using Tiknas.DependencyInjection;
using Tiknas.Domain.Entities;
using Tiknas.Domain.Entities.Events;
using Tiknas.Domain.Repositories;
using Tiknas.EntityFrameworkCore.ChangeTrackers;
using Tiknas.EntityFrameworkCore.EntityHistory;
using Tiknas.EntityFrameworkCore.GlobalFilters;
using Tiknas.EntityFrameworkCore.Modeling;
using Tiknas.EntityFrameworkCore.ValueConverters;
using Tiknas.EventBus.Distributed;
using Tiknas.EventBus.Local;
using Tiknas.Guids;
using Tiknas.MultiTenancy;
using Tiknas.ObjectExtending;
using Tiknas.Reflection;
using Tiknas.Timing;
using Tiknas.Uow;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Tiknas.EntityFrameworkCore;

public abstract class TiknasDbContext<TDbContext> : DbContext, ITiknasEfCoreDbContext, ITiknasEfCoreDbFunctionContext, ITransientDependency
    where TDbContext : DbContext
{
    public ITiknasLazyServiceProvider LazyServiceProvider { get; set; } = default!;

    protected virtual Guid? CurrentTenantId => CurrentTenant?.Id;

    protected virtual bool IsMultiTenantFilterEnabled => DataFilter?.IsEnabled<IMultiTenant>() ?? false;

    protected virtual bool IsSoftDeleteFilterEnabled => DataFilter?.IsEnabled<ISoftDelete>() ?? false;

    public ICurrentTenant CurrentTenant => LazyServiceProvider.LazyGetRequiredService<ICurrentTenant>();

    public IGuidGenerator GuidGenerator => LazyServiceProvider.LazyGetService<IGuidGenerator>(SimpleGuidGenerator.Instance);

    public IDataFilter DataFilter => LazyServiceProvider.LazyGetRequiredService<IDataFilter>();

    public IEntityChangeEventHelper EntityChangeEventHelper => LazyServiceProvider.LazyGetService<IEntityChangeEventHelper>(NullEntityChangeEventHelper.Instance);

    public IOptions<TiknasEntityChangeOptions> EntityChangeOptions => LazyServiceProvider.LazyGetRequiredService<IOptions<TiknasEntityChangeOptions>>();

    public IAuditPropertySetter AuditPropertySetter => LazyServiceProvider.LazyGetRequiredService<IAuditPropertySetter>();

    public IEntityHistoryHelper EntityHistoryHelper => LazyServiceProvider.LazyGetService<IEntityHistoryHelper>(NullEntityHistoryHelper.Instance);

    public IAuditingManager AuditingManager => LazyServiceProvider.LazyGetRequiredService<IAuditingManager>();

    public IUnitOfWorkManager UnitOfWorkManager => LazyServiceProvider.LazyGetRequiredService<IUnitOfWorkManager>();

    public IClock Clock => LazyServiceProvider.LazyGetRequiredService<IClock>();

    public IDistributedEventBus DistributedEventBus => LazyServiceProvider.LazyGetRequiredService<IDistributedEventBus>();

    public ILocalEventBus LocalEventBus => LazyServiceProvider.LazyGetRequiredService<ILocalEventBus>();

    public ILogger<TiknasDbContext<TDbContext>> Logger => LazyServiceProvider.LazyGetService<ILogger<TiknasDbContext<TDbContext>>>(NullLogger<TiknasDbContext<TDbContext>>.Instance);

    public TiknasEfCoreNavigationHelper TiknasEfCoreNavigationHelper => LazyServiceProvider.LazyGetRequiredService<TiknasEfCoreNavigationHelper>();

    public IOptions<TiknasDbContextOptions> Options => LazyServiceProvider.LazyGetRequiredService<IOptions<TiknasDbContextOptions>>();

    public IOptions<TiknasEfCoreGlobalFilterOptions> GlobalFilterOptions => LazyServiceProvider.LazyGetRequiredService<IOptions<TiknasEfCoreGlobalFilterOptions>>();

    private static readonly MethodInfo ConfigureBasePropertiesMethodInfo
        = typeof(TiknasDbContext<TDbContext>)
            .GetMethod(
                nameof(ConfigureBaseProperties),
                BindingFlags.Instance | BindingFlags.NonPublic
            )!;

    private static readonly MethodInfo ConfigureValueConverterMethodInfo
        = typeof(TiknasDbContext<TDbContext>)
            .GetMethod(
                nameof(ConfigureValueConverter),
                BindingFlags.Instance | BindingFlags.NonPublic
            )!;

    private static readonly MethodInfo ConfigureValueGeneratedMethodInfo
        = typeof(TiknasDbContext<TDbContext>)
            .GetMethod(
                nameof(ConfigureValueGenerated),
                BindingFlags.Instance | BindingFlags.NonPublic
            )!;

    protected readonly DbContextOptions DbContextOptions;

    protected TiknasDbContext(DbContextOptions<TDbContext> options)
        : base(options)
    {
        DbContextOptions = options;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(c => c.Ignore(RelationalEventId.PendingModelChangesWarning));
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        TrySetDatabaseProvider(modelBuilder);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            ConfigureBasePropertiesMethodInfo
                .MakeGenericMethod(entityType.ClrType)
                .Invoke(this, new object[] { modelBuilder, entityType });

            ConfigureValueConverterMethodInfo
                .MakeGenericMethod(entityType.ClrType)
                .Invoke(this, new object[] { modelBuilder, entityType });

            ConfigureValueGeneratedMethodInfo
                .MakeGenericMethod(entityType.ClrType)
                .Invoke(this, new object[] { modelBuilder, entityType });
        }

        if (LazyServiceProvider == null || Options == null)
        {
            return;
        }

        Options.Value.DefaultOnModelCreatingAction?.Invoke(this, modelBuilder);
        foreach (var onModelCreatingAction in Options.Value.OnModelCreatingActions.GetOrDefault(typeof(TDbContext)) ?? [])
        {
            onModelCreatingAction.As<Action<DbContext, ModelBuilder>>().Invoke(this, modelBuilder);
        }
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        if (LazyServiceProvider == null || Options == null)
        {
            return;
        }

        Options.Value.DefaultConventionAction?.Invoke(this, configurationBuilder);
        foreach (var conventionAction in Options.Value.ConventionActions.GetOrDefault(typeof(TDbContext)) ?? [])
        {
            conventionAction.As<Action<DbContext, ModelConfigurationBuilder>>().Invoke(this, configurationBuilder);
        }
    }

    protected virtual void TrySetDatabaseProvider(ModelBuilder modelBuilder)
    {
        var provider = GetDatabaseProviderOrNull(modelBuilder);
        if (provider != null)
        {
            modelBuilder.SetDatabaseProvider(provider.Value);
        }
    }

    protected virtual EfCoreDatabaseProvider? GetDatabaseProviderOrNull(ModelBuilder modelBuilder)
    {
        switch (Database.ProviderName)
        {
            case "Microsoft.EntityFrameworkCore.SqlServer":
                return EfCoreDatabaseProvider.SqlServer;
            case "Npgsql.EntityFrameworkCore.PostgreSQL":
                return EfCoreDatabaseProvider.PostgreSql;
            case "Pomelo.EntityFrameworkCore.MySql":
                return EfCoreDatabaseProvider.MySql;
            case "Oracle.EntityFrameworkCore":
            case "Devart.Data.Oracle.Entity.EFCore":
                return EfCoreDatabaseProvider.Oracle;
            case "Microsoft.EntityFrameworkCore.Sqlite":
                return EfCoreDatabaseProvider.Sqlite;
            case "Microsoft.EntityFrameworkCore.InMemory":
                return EfCoreDatabaseProvider.InMemory;
            case "FirebirdSql.EntityFrameworkCore.Firebird":
                return EfCoreDatabaseProvider.Firebird;
            case "Microsoft.EntityFrameworkCore.Cosmos":
                return EfCoreDatabaseProvider.Cosmos;
            default:
                return null;
        }
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        try
        {
            foreach (var entityEntry in TiknasEfCoreNavigationHelper.GetChangedEntityEntries())
            {
                if (EntityChangeOptions.Value.PublishEntityUpdatedEventWhenNavigationChanges)
                {
                    if (entityEntry.State == EntityState.Unchanged)
                    {
                        ApplyTiknasConceptsForModifiedEntity(entityEntry, true);
                    }

                    if (entityEntry.Entity is ISoftDelete && entityEntry.Entity.As<ISoftDelete>().IsDeleted)
                    {
                        EntityChangeEventHelper.PublishEntityDeletedEvent(entityEntry.Entity);
                    }
                    else
                    {
                        EntityChangeEventHelper.PublishEntityUpdatedEvent(entityEntry.Entity);
                    }
                }
                else if (entityEntry.Properties.Any(x => x.IsModified && (x.Metadata.ValueGenerated == ValueGenerated.Never || x.Metadata.ValueGenerated == ValueGenerated.OnAdd)))
                {
                    if (entityEntry.Properties.Where(x => x.IsModified).All(x => x.Metadata.IsForeignKey()))
                    {
                        // Skip `PublishEntityDeletedEvent/PublishEntityUpdatedEvent` if only foreign keys have changed.
                        break;
                    }

                    if (entityEntry.Entity is ISoftDelete && entityEntry.Entity.As<ISoftDelete>().IsDeleted)
                    {
                        EntityChangeEventHelper.PublishEntityDeletedEvent(entityEntry.Entity);
                    }
                    else
                    {
                        EntityChangeEventHelper.PublishEntityUpdatedEvent(entityEntry.Entity);
                    }
                }
            }

            var auditLog = AuditingManager?.Current?.Log;
            List<EntityChangeInfo>? entityChangeList = null;
            if (auditLog != null)
            {
                EntityHistoryHelper.InitializeNavigationHelper(TiknasEfCoreNavigationHelper);
                entityChangeList = EntityHistoryHelper.CreateChangeList(ChangeTracker.Entries().ToList());
            }

            HandlePropertiesBeforeSave();

            var eventReport = CreateEventReport();

            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            PublishEntityEvents(eventReport);

            if (entityChangeList != null)
            {
                EntityHistoryHelper.UpdateChangeList(entityChangeList);
                auditLog!.EntityChanges.AddRange(entityChangeList);
                Logger.LogDebug($"Added {entityChangeList.Count} entity changes to the current audit log");
            }

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            if (ex.Entries.Count > 0)
            {
                var sb = new StringBuilder();
                sb.AppendLine(ex.Entries.Count > 1
                    ? "There are some entries which are not saved due to concurrency exception:"
                    : "There is an entry which is not saved due to concurrency exception:");
                foreach (var entry in ex.Entries)
                {
                    sb.AppendLine(entry.ToString());
                }

                Logger.LogWarning(sb.ToString());
            }

            throw new TiknasDbConcurrencyException(ex.Message, ex);
        }
        finally
        {
            ChangeTracker.AutoDetectChangesEnabled = true;
            TiknasEfCoreNavigationHelper.RemoveChangedEntityEntries();
        }
    }

    protected virtual void PublishEntityEvents(EntityEventReport changeReport)
    {
        foreach (var localEvent in changeReport.DomainEvents)
        {
            UnitOfWorkManager.Current?.AddOrReplaceLocalEvent(
                new UnitOfWorkEventRecord(localEvent.EventData.GetType(), localEvent.EventData, localEvent.EventOrder)
            );
        }

        foreach (var distributedEvent in changeReport.DistributedEvents)
        {
            UnitOfWorkManager.Current?.AddOrReplaceDistributedEvent(
                new UnitOfWorkEventRecord(distributedEvent.EventData.GetType(), distributedEvent.EventData, distributedEvent.EventOrder)
            );
        }
    }

    /// <summary>
    /// This method will call the DbContext <see cref="SaveChangesAsync(bool, CancellationToken)"/> method directly of EF Core, which doesn't apply concepts of tiknas.
    /// </summary>
    public virtual Task<int> SaveChangesOnDbContextAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public virtual void Initialize(TiknasEfCoreDbContextInitializationContext initializationContext)
    {
        if (initializationContext.UnitOfWork.Options.Timeout.HasValue &&
            Database.IsRelational() &&
            !Database.GetCommandTimeout().HasValue)
        {
            Database.SetCommandTimeout(TimeSpan.FromMilliseconds(initializationContext.UnitOfWork.Options.Timeout.Value));
        }

        ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;

        ChangeTracker.Tracked += ChangeTracker_Tracked;
        ChangeTracker.StateChanged += ChangeTracker_StateChanged;

        if (UnitOfWorkManager is AlwaysDisableTransactionsUnitOfWorkManager)
        {
            Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;
        }
    }

    protected virtual void ChangeTracker_Tracked(object? sender, EntityTrackedEventArgs e)
    {
        TiknasEfCoreNavigationHelper.ChangeTracker_Tracked(sender, e);
        FillExtraPropertiesForTrackedEntities(e);
        PublishEventsForTrackedEntity(e.Entry);
    }

    protected virtual void ChangeTracker_StateChanged(object? sender, EntityStateChangedEventArgs e)
    {
        TiknasEfCoreNavigationHelper.ChangeTracker_StateChanged(sender, e);
        PublishEventsForTrackedEntity(e.Entry);
    }

    protected virtual void FillExtraPropertiesForTrackedEntities(EntityTrackedEventArgs e)
    {
        var entityType = e.Entry.Metadata.ClrType;
        if (entityType == null)
        {
            return;
        }

        if (!(e.Entry.Entity is IHasExtraProperties entity))
        {
            return;
        }

        if (!e.FromQuery)
        {
            return;
        }

        var objectExtension = ObjectExtensionManager.Instance.GetOrNull(entityType);
        if (objectExtension == null)
        {
            return;
        }

        foreach (var property in objectExtension.GetProperties())
        {
            if (!property.IsMappedToFieldForEfCore())
            {
                continue;
            }

            /* Checking "currentValue != null" has a good advantage:
             * Assume that you we already using a named extra property,
             * then decided to create a field (entity extension) for it.
             * In this way, it prevents to delete old value in the JSON and
             * updates the field on the next save!
             */

            var currentValue = e.Entry.CurrentValues[property.Name];
            if (currentValue != null)
            {
                entity.ExtraProperties[property.Name] = currentValue;
            }
        }
    }

    protected virtual void PublishEventsForTrackedEntity(EntityEntry entry)
    {
        switch (entry.State)
        {
            case EntityState.Added:
                ApplyTiknasConceptsForAddedEntity(entry);
                EntityChangeEventHelper.PublishEntityCreatedEvent(entry.Entity);
                break;

            case EntityState.Modified:
                if (entry.Properties.Any(x => x.IsModified && (x.Metadata.ValueGenerated == ValueGenerated.Never || x.Metadata.ValueGenerated == ValueGenerated.OnAdd)))
                {
                    if (entry.Properties.Where(x => x.IsModified).All(x => x.Metadata.IsForeignKey()))
                    {
                        // Skip `PublishEntityDeletedEvent/PublishEntityUpdatedEvent` if only foreign keys have changed.
                        break;
                    }

                    ApplyTiknasConceptsForModifiedEntity(entry);
                    if (entry.Entity is ISoftDelete && entry.Entity.As<ISoftDelete>().IsDeleted)
                    {
                        EntityChangeEventHelper.PublishEntityDeletedEvent(entry.Entity);
                    }
                    else
                    {
                        EntityChangeEventHelper.PublishEntityUpdatedEvent(entry.Entity);
                    }
                }
                else if (EntityChangeOptions.Value.PublishEntityUpdatedEventWhenNavigationChanges && TiknasEfCoreNavigationHelper.IsNavigationEntryModified(entry))
                {
                    ApplyTiknasConceptsForModifiedEntity(entry, true);
                    if (entry.Entity is ISoftDelete && entry.Entity.As<ISoftDelete>().IsDeleted)
                    {
                        EntityChangeEventHelper.PublishEntityDeletedEvent(entry.Entity);
                    }
                    else
                    {
                        EntityChangeEventHelper.PublishEntityUpdatedEvent(entry.Entity);
                    }
                }
                break;

            case EntityState.Deleted:
                ApplyTiknasConceptsForDeletedEntity(entry);
                EntityChangeEventHelper.PublishEntityDeletedEvent(entry.Entity);
                break;
        }
    }

    protected virtual void HandlePropertiesBeforeSave()
    {
        var entries = ChangeTracker.Entries().ToList();
        foreach (var entry in entries)
        {
            HandleExtraPropertiesOnSave(entry);

            if (entry.State.IsIn(EntityState.Modified, EntityState.Deleted))
            {
                UpdateConcurrencyStamp(entry);
            }
        }

        if (EntityChangeOptions.Value.PublishEntityUpdatedEventWhenNavigationChanges)
        {
            foreach (var entry in TiknasEfCoreNavigationHelper.GetChangedEntityEntries().Where(x => x.State == EntityState.Unchanged))
            {
                UpdateConcurrencyStamp(entry);
            }
        }
    }

    protected virtual EntityEventReport CreateEventReport()
    {
        var eventReport = new EntityEventReport();

        foreach (var entry in ChangeTracker.Entries().ToList())
        {
            var generatesDomainEventsEntity = entry.Entity as IGeneratesDomainEvents;
            if (generatesDomainEventsEntity == null)
            {
                continue;
            }

            var localEvents = generatesDomainEventsEntity.GetLocalEvents()?.ToArray();
            if (localEvents != null && localEvents.Any())
            {
                eventReport.DomainEvents.AddRange(
                    localEvents.Select(
                        eventRecord => new DomainEventEntry(
                            entry.Entity,
                            eventRecord.EventData,
                            eventRecord.EventOrder
                        )
                    )
                );
                generatesDomainEventsEntity.ClearLocalEvents();
            }

            var distributedEvents = generatesDomainEventsEntity.GetDistributedEvents()?.ToArray();
            if (distributedEvents != null && distributedEvents.Any())
            {
                eventReport.DistributedEvents.AddRange(
                    distributedEvents.Select(
                        eventRecord => new DomainEventEntry(
                            entry.Entity,
                            eventRecord.EventData,
                            eventRecord.EventOrder)
                    )
                );
                generatesDomainEventsEntity.ClearDistributedEvents();
            }
        }

        return eventReport;
    }

    protected virtual void HandleExtraPropertiesOnSave(EntityEntry entry)
    {
        if (entry.State.IsIn(EntityState.Deleted, EntityState.Unchanged))
        {
            return;
        }

        var entityType = entry.Metadata.ClrType;
        if (entityType == null)
        {
            return;
        }

        if (!(entry.Entity is IHasExtraProperties entity))
        {
            return;
        }

        var objectExtension = ObjectExtensionManager.Instance.GetOrNull(entityType);
        if (objectExtension == null)
        {
            return;
        }

        var efMappedProperties = ObjectExtensionManager.Instance
            .GetProperties(entityType)
            .Where(p => p.IsMappedToFieldForEfCore());

        foreach (var property in efMappedProperties)
        {
            if (!entity.HasProperty(property.Name))
            {
                continue;
            }

            var entryProperty = entry.Property(property.Name);
            var entityProperty = entity.GetProperty(property.Name);
            if (entityProperty == null)
            {
                entryProperty.CurrentValue = null;
                continue;
            }

            if (entryProperty.Metadata.ClrType == entityProperty.GetType())
            {
                entryProperty.CurrentValue = entityProperty;
            }
            else
            {
                if (TypeHelper.IsPrimitiveExtended(entryProperty.Metadata.ClrType, includeEnums: true))
                {
                    var conversionType = entryProperty.Metadata.ClrType;
                    if (TypeHelper.IsNullable(conversionType))
                    {
                        conversionType = conversionType.GetFirstGenericArgumentIfNullable();
                    }

                    if (conversionType == typeof(Guid))
                    {
                        entryProperty.CurrentValue = TypeDescriptor.GetConverter(conversionType).ConvertFromInvariantString(entityProperty.ToString()!);
                    }
                    else if (conversionType.IsEnum)
                    {
                        entryProperty.CurrentValue = Enum.Parse(conversionType, entityProperty.ToString()!, ignoreCase: true);
                    }
                    else
                    {
                        entryProperty.CurrentValue = Convert.ChangeType(entityProperty, conversionType, CultureInfo.InvariantCulture);
                    }
                }
            }
        }
    }

    protected virtual void ApplyTiknasConceptsForAddedEntity(EntityEntry entry)
    {
        CheckAndSetId(entry);
        SetConcurrencyStampIfNull(entry);
        SetCreationAuditProperties(entry);
    }

    protected virtual void ApplyTiknasConceptsForModifiedEntity(EntityEntry entry, bool forceApply = false)
    {
        if (forceApply ||
            entry.Properties.Any(x => x.IsModified && (x.Metadata.ValueGenerated == ValueGenerated.Never || x.Metadata.ValueGenerated == ValueGenerated.OnAdd)))
        {
            IncrementEntityVersionProperty(entry);
            SetModificationAuditProperties(entry);

            if (entry.Entity is ISoftDelete && entry.Entity.As<ISoftDelete>().IsDeleted)
            {
                SetDeletionAuditProperties(entry);
            }
        }
    }

    protected virtual void ApplyTiknasConceptsForDeletedEntity(EntityEntry entry)
    {
        if (!(entry.Entity is ISoftDelete))
        {
            return;
        }

        if (IsHardDeleted(entry))
        {
            return;
        }

        ExtraPropertyDictionary? originalExtraProperties = null;
        if (entry.Entity is IHasExtraProperties)
        {
            originalExtraProperties = entry.OriginalValues.GetValue<ExtraPropertyDictionary>(nameof(IHasExtraProperties.ExtraProperties));
        }

        //TODO: Reload will throw an exception. Check it when new EF Core versions released.
        //entry.Reload();

        var storeValues = entry.OriginalValues;
        entry.CurrentValues.SetValues(storeValues);
        entry.OriginalValues.SetValues(storeValues);
        entry.State = EntityState.Unchanged;

        if (entry.Entity is IHasExtraProperties)
        {
            ObjectHelper.TrySetProperty(entry.Entity.As<IHasExtraProperties>(), x => x.ExtraProperties, () => originalExtraProperties);
        }

        ObjectHelper.TrySetProperty(entry.Entity.As<ISoftDelete>(), x => x.IsDeleted, () => true);
        SetDeletionAuditProperties(entry);
    }

    protected virtual bool IsHardDeleted(EntityEntry entry)
    {
        var hardDeletedEntities = UnitOfWorkManager?.Current?.Items.GetOrDefault(UnitOfWorkItemNames.HardDeletedEntities) as HashSet<IEntity>;
        if (hardDeletedEntities == null)
        {
            return false;
        }

        return hardDeletedEntities.Contains(entry.Entity);
    }

    protected virtual void UpdateConcurrencyStamp(EntityEntry entry)
    {
        var entity = entry.Entity as IHasConcurrencyStamp;
        if (entity == null)
        {
            return;
        }

        Entry(entity).Property(x => x.ConcurrencyStamp).OriginalValue = entity.ConcurrencyStamp;
        entity.ConcurrencyStamp = Guid.NewGuid().ToString("N");
    }

    protected virtual void SetConcurrencyStampIfNull(EntityEntry entry)
    {
        var entity = entry.Entity as IHasConcurrencyStamp;
        if (entity == null)
        {
            return;
        }

        if (entity.ConcurrencyStamp != null)
        {
            return;
        }

        entity.ConcurrencyStamp = Guid.NewGuid().ToString("N");
    }

    protected virtual void CheckAndSetId(EntityEntry entry)
    {
        if (entry.Entity is IEntity<Guid> entityWithGuidId)
        {
            TrySetGuidId(entry, entityWithGuidId);
        }
    }

    protected virtual void TrySetGuidId(EntityEntry entry, IEntity<Guid> entity)
    {
        if (entity.Id != default)
        {
            return;
        }

        var idProperty = entry.Property("Id").Metadata.PropertyInfo!;

        //Check for DatabaseGeneratedAttribute
        var dbGeneratedAttr = ReflectionHelper
            .GetSingleAttributeOrDefault<DatabaseGeneratedAttribute>(
                idProperty
            );

        if (dbGeneratedAttr != null && dbGeneratedAttr.DatabaseGeneratedOption != DatabaseGeneratedOption.None)
        {
            return;
        }

        EntityHelper.TrySetId(
            entity,
            () => GuidGenerator.Create(),
            true
        );
    }

    protected virtual void SetCreationAuditProperties(EntityEntry entry)
    {
        AuditPropertySetter?.SetCreationProperties(entry.Entity);
    }

    protected virtual void SetModificationAuditProperties(EntityEntry entry)
    {
        AuditPropertySetter?.SetModificationProperties(entry.Entity);
    }

    protected virtual void SetDeletionAuditProperties(EntityEntry entry)
    {
        AuditPropertySetter?.SetDeletionProperties(entry.Entity);
    }

    protected virtual void IncrementEntityVersionProperty(EntityEntry entry)
    {
        AuditPropertySetter?.IncrementEntityVersionProperty(entry.Entity);
    }

    protected virtual void ConfigureBaseProperties<TEntity>(ModelBuilder modelBuilder, IMutableEntityType mutableEntityType)
        where TEntity : class
    {
        if (mutableEntityType.IsOwned())
        {
            return;
        }

        if (!typeof(IEntity).IsAssignableFrom(typeof(TEntity)))
        {
            return;
        }

        modelBuilder.Entity<TEntity>().ConfigureByConvention();

        ConfigureGlobalFilters<TEntity>(modelBuilder, mutableEntityType);
    }

    protected virtual void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder, IMutableEntityType mutableEntityType)
        where TEntity : class
    {
        if (mutableEntityType.BaseType == null && ShouldFilterEntity<TEntity>(mutableEntityType))
        {
            var filterExpression = CreateFilterExpression<TEntity>(modelBuilder);
            if (filterExpression != null)
            {
                modelBuilder.Entity<TEntity>().HasTiknasQueryFilter(filterExpression);
            }
        }
    }

    protected virtual void ConfigureValueConverter<TEntity>(ModelBuilder modelBuilder, IMutableEntityType mutableEntityType)
        where TEntity : class
    {
        if (mutableEntityType.BaseType == null &&
            !typeof(TEntity).IsDefined(typeof(DisableDateTimeNormalizationAttribute), true) &&
            !typeof(TEntity).IsDefined(typeof(OwnedAttribute), true) &&
            !mutableEntityType.IsOwned())
        {
            if (LazyServiceProvider == null || Clock == null)
            {
                return;
            }

            TiknasDateTimeValueConverter.Clock = Clock;
            TiknasNullableDateTimeValueConverter.Clock = Clock;

            foreach (var property in mutableEntityType.GetProperties().
                         Where(property => property.PropertyInfo != null &&
                                           (property.PropertyInfo.PropertyType == typeof(DateTime) || property.PropertyInfo.PropertyType == typeof(DateTime?)) &&
                                           property.PropertyInfo.CanWrite &&
                                           ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<DisableDateTimeNormalizationAttribute>(property.PropertyInfo) == null))
            {
				modelBuilder
                    .Entity<TEntity>()
                    .Property(property.Name)
                    .HasConversion(property.ClrType == typeof(DateTime)
                        ? new TiknasDateTimeValueConverter()
                        : new TiknasNullableDateTimeValueConverter());
            }
        }
    }

    protected virtual void ConfigureValueGenerated<TEntity>(ModelBuilder modelBuilder, IMutableEntityType mutableEntityType)
        where TEntity : class
    {
        if (!typeof(IEntity<Guid>).IsAssignableFrom(typeof(TEntity)))
        {
            return;
        }

        var idPropertyBuilder = modelBuilder.Entity<TEntity>().Property(x => ((IEntity<Guid>)x).Id);
        if (idPropertyBuilder.Metadata.PropertyInfo!.IsDefined(typeof(DatabaseGeneratedAttribute), true))
        {
            return;
        }

        idPropertyBuilder.ValueGeneratedNever();
    }

    protected virtual bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType) where TEntity : class
    {
        if (typeof(IMultiTenant).IsAssignableFrom(typeof(TEntity)))
        {
            return true;
        }

        if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
        {
            return true;
        }

        return false;
    }

    protected virtual Expression<Func<TEntity, bool>>? CreateFilterExpression<TEntity>(ModelBuilder modelBuilder)
        where TEntity : class
    {
        Expression<Func<TEntity, bool>>? expression = null;

        if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
        {
            var softDeleteColumnName = modelBuilder.Entity<TEntity>().Metadata.FindProperty(nameof(ISoftDelete.IsDeleted))?.GetColumnName() ?? "IsDeleted";
            expression = e => !IsSoftDeleteFilterEnabled || !EF.Property<bool>(e, softDeleteColumnName);
            if (UseDbFunction())
            {
                expression = e => TiknasEfCoreDataFilterDbFunctionMethods.SoftDeleteFilter(((ISoftDelete)e).IsDeleted, true);
                modelBuilder.ConfigureSoftDeleteDbFunction(TiknasEfCoreDataFilterDbFunctionMethods.SoftDeleteFilterMethodInfo, this.GetService<TiknasEfCoreCurrentDbContext>());
            }
        }

        if (typeof(IMultiTenant).IsAssignableFrom(typeof(TEntity)))
        {
            var multiTenantColumnName = modelBuilder.Entity<TEntity>().Metadata.FindProperty(nameof(IMultiTenant.TenantId))?.GetColumnName() ?? "TenantId";
            Expression<Func<TEntity, bool>> multiTenantFilter = e => !IsMultiTenantFilterEnabled || EF.Property<Guid>(e, multiTenantColumnName) == CurrentTenantId;
            if (UseDbFunction())
            {
                multiTenantFilter = e => TiknasEfCoreDataFilterDbFunctionMethods.MultiTenantFilter(((IMultiTenant)e).TenantId, CurrentTenantId, true);
                modelBuilder.ConfigureMultiTenantDbFunction(TiknasEfCoreDataFilterDbFunctionMethods.MultiTenantFilterMethodInfo, this.GetService<TiknasEfCoreCurrentDbContext>());
            }
            expression = expression == null ? multiTenantFilter : QueryFilterExpressionHelper.CombineExpressions(expression, multiTenantFilter);
        }

        return expression;
    }

    protected virtual bool UseDbFunction()
    {
        return LazyServiceProvider != null && GlobalFilterOptions.Value.UseDbFunction && DbContextOptions.FindExtension<TiknasDbContextOptionsExtension>() != null;
    }

    public virtual string GetCompiledQueryCacheKey()
    {
        return $"{CurrentTenantId?.ToString() ?? "Null"}:{IsSoftDeleteFilterEnabled}:{IsMultiTenantFilterEnabled}";
    }
}
