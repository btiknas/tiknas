using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Tiknas.Data;

public class TiknasDatabaseInfoDictionary : Dictionary<string, TiknasDatabaseInfo>
{
    private Dictionary<string, TiknasDatabaseInfo> ConnectionIndex { get; set; }

    public TiknasDatabaseInfoDictionary()
    {
        ConnectionIndex = new Dictionary<string, TiknasDatabaseInfo>();
    }

    public TiknasDatabaseInfo? GetMappedDatabaseOrNull(string connectionStringName)
    {
        return ConnectionIndex.GetOrDefault(connectionStringName);
    }

    public TiknasDatabaseInfoDictionary Configure(string databaseName, Action<TiknasDatabaseInfo> configureAction)
    {
        var databaseInfo = this.GetOrAdd(
            databaseName,
            () => new TiknasDatabaseInfo(databaseName)
        );

        configureAction(databaseInfo);

        return this;
    }

    /// <summary>
    /// This method should be called if this dictionary changes.
    /// It refreshes indexes for quick access to the connection informations.
    /// </summary>
    public void RefreshIndexes()
    {
        ConnectionIndex = new Dictionary<string, TiknasDatabaseInfo>();

        foreach (var databaseInfo in Values)
        {
            foreach (var mappedConnection in databaseInfo.MappedConnections)
            {
                if (ConnectionIndex.ContainsKey(mappedConnection))
                {
                    throw new TiknasException(
                        $"A connection name can not map to multiple databases: {mappedConnection}."
                    );
                }

                ConnectionIndex[mappedConnection] = databaseInfo;
            }
        }
    }
}
