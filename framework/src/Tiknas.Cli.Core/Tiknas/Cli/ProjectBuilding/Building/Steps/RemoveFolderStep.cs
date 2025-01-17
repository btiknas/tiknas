﻿using System;
using System.Linq;

namespace Tiknas.Cli.ProjectBuilding.Building.Steps;

public class RemoveFolderStep : ProjectBuildPipelineStep
{
    private readonly string _folderPath;

    public RemoveFolderStep(string folderPath)
    {
        _folderPath = folderPath;
    }

    public override void Execute(ProjectBuildContext context)
    {
        //Remove the folder content
        var folderPathWithSlash = _folderPath.EnsureEndsWith('/');
        context.Files.RemoveAll(file => file.Name.StartsWith(folderPathWithSlash));

        //Remove the folder
        var folder = context.Files.FirstOrDefault(file => file.Name == _folderPath);
        if (folder != null)
        {
            context.Files.Remove(folder);
        }
    }
}
