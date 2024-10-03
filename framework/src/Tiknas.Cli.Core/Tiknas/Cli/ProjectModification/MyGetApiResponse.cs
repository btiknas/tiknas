using System.Collections.Generic;

namespace Tiknas.Cli.ProjectModification;

public class MyGetApiResponse
{
    public string _date { get; set; }

    public List<MyGetPackage> Packages { get; set; }
}
