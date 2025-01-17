﻿using System;
using System.IO;
using System.Text;

namespace Tiknas.Cli;

public static class CliPaths
{
    public static string TemplateCache => Path.Combine(TiknasRootPath, "templates"); //TODO: Move somewhere else?
    public static string Log => Path.Combine(TiknasRootPath, "cli", "logs");
    public static string Root => Path.Combine(TiknasRootPath, "cli");
    public static string AccessToken => Path.Combine(TiknasRootPath, "cli", "access-token.bin");
    public static string ComputerId => Path.Combine(TiknasRootPath, "cli", "computer-id.bin");
    public static string Memory => Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!, "memory.bin");
    public static string Build => Path.Combine(TiknasRootPath, "build");
    public static string Lic => Path.Combine(Path.GetTempPath(), Encoding.ASCII.GetString(new byte[] { 65, 98, 112, 76, 105, 99, 101, 110, 115, 101, 46, 98, 105, 110 }));

    public static readonly string TiknasRootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".tiknas");
}
