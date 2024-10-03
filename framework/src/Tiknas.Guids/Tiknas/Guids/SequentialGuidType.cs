namespace Tiknas.Guids;

/// <summary>
/// Describes the type of a sequential GUID value.
/// </summary>
public enum SequentialGuidType
{
    /// <summary>
    /// The GUID should be sequential when formatted using the <c>Guid.ToString</c> method.
    /// Used by MySql and PostgreSql.
    /// </summary>
    SequentialAsString,

    /// <summary>
    /// The GUID should be sequential when formatted using the <c>Guid.ToByteArray</c> method.
    /// Used by Oracle.
    /// </summary>
    SequentialAsBinary,

    /// <summary>
    /// The sequential portion of the GUID should be located at the end of the Data4 block.
    /// Used by SqlServer.
    /// </summary>
    SequentialAtEnd
}
