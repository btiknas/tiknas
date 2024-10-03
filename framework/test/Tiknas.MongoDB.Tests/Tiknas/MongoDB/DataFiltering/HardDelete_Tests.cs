using System;
using System.Collections.Generic;
using System.Text;
using Tiknas.TestApp.Testing;
using Xunit;

namespace Tiknas.MongoDB.DataFiltering;

[Collection(MongoTestCollection.Name)]
public class HardDelete_Tests : HardDelete_Tests<TiknasMongoDbTestModule>
{
}
