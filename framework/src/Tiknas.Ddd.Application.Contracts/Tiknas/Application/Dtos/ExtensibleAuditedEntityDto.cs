using System;
using Tiknas.Auditing;
using Tiknas.Data;

namespace Tiknas.Application.Dtos;

/// <summary>
/// This class can be inherited by DTO classes to implement <see cref="IAuditedObject"/> interface.
/// It also implements the <see cref="IHasExtraProperties"/> interface.
/// </summary>
/// <typeparam name="TPrimaryKey">Type of primary key</typeparam>
[Serializable]
public abstract class ExtensibleAuditedEntityDto<TPrimaryKey> : ExtensibleCreationAuditedEntityDto<TPrimaryKey>, IAuditedObject
{
    /// <inheritdoc />
    public DateTime? LastModificationTime { get; set; }

    /// <inheritdoc />
    public Guid? LastModifierId { get; set; }

    protected ExtensibleAuditedEntityDto()
        : this(true)
    {

    }

    protected ExtensibleAuditedEntityDto(bool setDefaultsForExtraProperties)
        : base(setDefaultsForExtraProperties)
    {

    }
}

/// <summary>
/// This class can be inherited by DTO classes to implement <see cref="IAuditedObject"/> interface.
/// It also implements the <see cref="IHasExtraProperties"/> interface.
/// </summary>
[Serializable]
public abstract class ExtensibleAuditedEntityDto : ExtensibleCreationAuditedEntityDto, IAuditedObject
{
    /// <inheritdoc />
    public DateTime? LastModificationTime { get; set; }

    /// <inheritdoc />
    public Guid? LastModifierId { get; set; }

    protected ExtensibleAuditedEntityDto()
        : this(true)
    {

    }

    protected ExtensibleAuditedEntityDto(bool setDefaultsForExtraProperties)
        : base(setDefaultsForExtraProperties)
    {

    }
}
