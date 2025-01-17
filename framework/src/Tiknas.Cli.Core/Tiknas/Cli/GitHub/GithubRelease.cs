using System;
using Newtonsoft.Json;

namespace Tiknas.Cli.GitHub;

[JsonObject]
[Serializable]
public class GithubRelease
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("prerelease")]
    public bool IsPrerelease { get; set; }

    [JsonProperty("published_at")]
    public DateTime PublishTime { get; set; }
}