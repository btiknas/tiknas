﻿using LibGit2Sharp;
using Tiknas.DependencyInjection;

namespace Tiknas.Cli.Build;

public class GitRepositoryHelper : IGitRepositoryHelper, ITransientDependency
{
    public string GetLastCommitId(GitRepository repository)
    {
        using (var repo = new Repository(string.Concat(repository.RootPath, @"\.git")))
        {
            return repo.Head.Tip.Id.ToString();
        }
    }

    public string GetFriendlyName(GitRepository repository)
    {
        using (var repo = new Repository(string.Concat(repository.RootPath, @"\.git")))
        {
            return repo.Head.FriendlyName;
        }
    }
}
