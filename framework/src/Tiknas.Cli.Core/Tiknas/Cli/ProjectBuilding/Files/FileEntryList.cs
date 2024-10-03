using System.Collections.Generic;

namespace Tiknas.Cli.ProjectBuilding.Files;

public class FileEntryList : List<FileEntry>
{
    public FileEntryList(IEnumerable<FileEntry> entries)
        : base(entries)
    {

    }
}
