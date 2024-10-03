using System;
using JetBrains.Annotations;
using Tiknas.Data;

namespace Tiknas.MultiTenancy;

[Serializable]
public class TenantConfiguration
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string NormalizedName { get; set; } = default!;

    public ConnectionStrings? ConnectionStrings { get; set; }

    public bool IsActive { get; set; }

    public TenantConfiguration()
    {
        IsActive = true;
    }

    public TenantConfiguration(Guid id, [NotNull] string name)
        : this()
    {
        Check.NotNull(name, nameof(name));

        Id = id;
        Name = name;

        ConnectionStrings = new ConnectionStrings();
    }

    public TenantConfiguration(Guid id, [NotNull] string name, [NotNull] string normalizedName)
        : this(id, name)
    {
        Check.NotNull(normalizedName, nameof(normalizedName));

        NormalizedName = normalizedName;
    }
}
