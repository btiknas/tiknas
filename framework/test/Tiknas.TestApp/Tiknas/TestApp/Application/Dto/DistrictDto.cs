using System;
using Tiknas.Application.Dtos;

namespace Tiknas.TestApp.Application.Dto;

public class DistrictDto : EntityDto
{
    public Guid CityId { get; set; }

    public string Name { get; set; }

    public int Population { get; set; }
}
