﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Tiknas.Cli.ProjectBuilding.Building.Steps;

public class RemoveProjectFromTyeStep : ProjectBuildPipelineStep
{
    private readonly string _name;

    public RemoveProjectFromTyeStep(string name)
    {
        _name = name;
    }

    public override void Execute(ProjectBuildContext context)
    {
        var tyeFile = context.Files.FirstOrDefault(f => f.Name == "/tye.yaml");

        if (tyeFile == null)
        {
            return;
        }

        var lines = tyeFile.GetLines();
        var newLines = new List<string>();

        var nameLine = $"- name:";
        var isOneOfTargetLines = false;

        foreach (var line in lines)
        {
            if (line.Trim().Equals($"{nameLine} {_name}"))
            {
                isOneOfTargetLines = true;
                continue;
            }

            if (line.Trim().StartsWith(nameLine))
            {
                isOneOfTargetLines = false;
            }

            if (!isOneOfTargetLines)
            {
                newLines.Add(line);
            }
        }

        tyeFile.SetContent(String.Join(Environment.NewLine, newLines));
    }

}
