﻿using System;
using NSubstitute;
using Tiknas.MultiTenancy;
using Tiknas.Timing;
using Tiknas.Users;

namespace Tiknas.Auditing;

public class AuditPropertySetterTestBase
{
    protected Guid? CurrentUserId = null;
    protected Guid? CurrentUserTenantId = null;
    protected Guid? CurrentTenantId = null;

    protected DateTime Now = DateTime.Now;

    protected MyAuditedObject TargetObject;

    protected readonly AuditPropertySetter AuditPropertySetter;

    public AuditPropertySetterTestBase()
    {
        AuditPropertySetter = CreateAuditPropertySetter();
        TargetObject = new MyAuditedObject();
    }

    private AuditPropertySetter CreateAuditPropertySetter()
    {
        var currentUser = Substitute.For<ICurrentUser>();
        currentUser.Id.Returns(ci => CurrentUserId);
        currentUser.TenantId.Returns(ci => CurrentUserTenantId);

        var currentTenant = Substitute.For<ICurrentTenant>();
        currentTenant.Id.Returns(ci => CurrentTenantId);

        var clock = Substitute.For<IClock>();
        clock.Now.Returns(Now);

        return new AuditPropertySetter(
            currentUser,
            currentTenant,
            clock
        );
    }

    public class MyEmptyObject
    {

    }

    public class MyAuditedObject : IMultiTenant, IFullAuditedObject, IHasEntityVersion
    {
        public Guid? TenantId { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid? CreatorId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? LastModifierId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletionTime { get; set; }
        public Guid? DeleterId { get; set; }
        public int EntityVersion { get; set; }
    }
}
