﻿using Tiknas.TestApp.Testing;
using Xunit;

namespace Tiknas.MongoDB.Domain;

[Collection(MongoTestCollection.Name)]
public class ConcurrencyStamp_Tests : ConcurrencyStamp_Tests<TiknasMongoDbTestModule>
{

}
