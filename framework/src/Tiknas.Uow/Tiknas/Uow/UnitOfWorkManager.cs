﻿using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.DependencyInjection;

namespace Tiknas.Uow;

public class UnitOfWorkManager : IUnitOfWorkManager, ISingletonDependency
{
    [Obsolete("This will be removed in next versions.")]
    public static AsyncLocal<bool> DisableObsoleteDbContextCreationWarning { get; } = new AsyncLocal<bool>();

    public IUnitOfWork? Current => _ambientUnitOfWork.GetCurrentByChecking();

    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IAmbientUnitOfWork _ambientUnitOfWork;

    public UnitOfWorkManager(
        IAmbientUnitOfWork ambientUnitOfWork,
        IServiceScopeFactory serviceScopeFactory)
    {
        _ambientUnitOfWork = ambientUnitOfWork;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public IUnitOfWork Begin(TiknasUnitOfWorkOptions options, bool requiresNew = false)
    {
        Check.NotNull(options, nameof(options));

        var currentUow = Current;
        if (currentUow != null && !requiresNew)
        {
            return new ChildUnitOfWork(currentUow);
        }

        var unitOfWork = CreateNewUnitOfWork();
        unitOfWork.Initialize(options);

        return unitOfWork;
    }

    public IUnitOfWork Reserve(string reservationName, bool requiresNew = false)
    {
        Check.NotNull(reservationName, nameof(reservationName));

        if (!requiresNew &&
            _ambientUnitOfWork.UnitOfWork != null &&
            _ambientUnitOfWork.UnitOfWork.IsReservedFor(reservationName))
        {
            return new ChildUnitOfWork(_ambientUnitOfWork.UnitOfWork);
        }

        var unitOfWork = CreateNewUnitOfWork();
        unitOfWork.Reserve(reservationName);

        return unitOfWork;
    }

    public void BeginReserved(string reservationName, TiknasUnitOfWorkOptions options)
    {
        if (!TryBeginReserved(reservationName, options))
        {
            throw new TiknasException($"Could not find a reserved unit of work with reservation name: {reservationName}");
        }
    }

    public bool TryBeginReserved(string reservationName, TiknasUnitOfWorkOptions options)
    {
        Check.NotNull(reservationName, nameof(reservationName));

        var uow = _ambientUnitOfWork.UnitOfWork;

        //Find reserved unit of work starting from current and going to outers
        while (uow != null && !uow.IsReservedFor(reservationName))
        {
            uow = uow.Outer;
        }

        if (uow == null)
        {
            return false;
        }

        uow.Initialize(options);

        return true;
    }

    private IUnitOfWork CreateNewUnitOfWork()
    {
        var scope = _serviceScopeFactory.CreateScope();
        try
        {
            var outerUow = _ambientUnitOfWork.UnitOfWork;

            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            unitOfWork.SetOuter(outerUow);

            _ambientUnitOfWork.SetUnitOfWork(unitOfWork);

            unitOfWork.Disposed += (sender, args) =>
            {
                _ambientUnitOfWork.SetUnitOfWork(outerUow);
                scope.Dispose();
            };

            return unitOfWork;
        }
        catch
        {
            scope.Dispose();
            throw;
        }
    }
}
