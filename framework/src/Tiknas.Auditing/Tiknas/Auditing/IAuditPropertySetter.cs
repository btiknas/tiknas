﻿namespace Tiknas.Auditing;

public interface IAuditPropertySetter
{
    void SetCreationProperties(object targetObject);

    void SetModificationProperties(object targetObject);

    void SetDeletionProperties(object targetObject);

    void IncrementEntityVersionProperty(object targetObject);
}
