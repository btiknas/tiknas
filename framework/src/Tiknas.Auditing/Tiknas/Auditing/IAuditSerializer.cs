namespace Tiknas.Auditing;

public interface IAuditSerializer
{
    string Serialize(object obj);
}
