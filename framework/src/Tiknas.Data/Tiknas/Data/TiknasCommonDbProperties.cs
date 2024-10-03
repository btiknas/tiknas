namespace Tiknas.Data;

public static class TiknasCommonDbProperties
{
    /// <summary>
    /// This table prefix is shared by most of the Tiknas modules.
    /// You can change it to set table prefix for all modules using this.
    /// 
    /// Default value: "Tik".
    /// </summary>
    public static string DbTablePrefix { get; set; } = "Tik";

    /// <summary>
    /// Default value: null.
    /// </summary>
    public static string? DbSchema { get; set; } = null;
}
