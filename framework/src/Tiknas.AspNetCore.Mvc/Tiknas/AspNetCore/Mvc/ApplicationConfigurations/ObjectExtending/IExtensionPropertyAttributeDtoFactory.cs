using System;

namespace Tiknas.AspNetCore.Mvc.ApplicationConfigurations.ObjectExtending;

public interface IExtensionPropertyAttributeDtoFactory
{
    ExtensionPropertyAttributeDto Create(Attribute attribute);
}
