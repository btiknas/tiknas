﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tiknas.Cli.ProjectBuilding.Files;

namespace Tiknas.Cli.ProjectBuilding.Building.Steps;

public class RemoveRootFolderStep : ProjectBuildPipelineStep
{
    public override void Execute(ProjectBuildContext context)
    {
        foreach (var file in context.Files)
        {
            file.SetName(RemoveRootFolder(file.Name));
        }
    }

    private string RemoveRootFolder(string fileName)
    {
        if (!fileName.Contains('/'))
        {
            return fileName;
        }

        return string.Join("/", fileName.Split('/').Skip(1));
    }
}
