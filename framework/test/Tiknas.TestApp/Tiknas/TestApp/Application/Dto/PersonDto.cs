using System;
using System.ComponentModel.DataAnnotations;
using Tiknas.Application.Dtos;
using Tiknas.MultiTenancy;

namespace Tiknas.TestApp.Application.Dto;

public class PersonDto : EntityDto<Guid>, IMultiTenant
{
    [Required]
    public string Name { get; set; }

    public int Age { get; set; }

    public Guid? TenantId { get; set; }
}
