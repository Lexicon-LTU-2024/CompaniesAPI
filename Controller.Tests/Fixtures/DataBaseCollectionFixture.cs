using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller.Tests.Fixtures;

[CollectionDefinition("DatabaseCollection", DisableParallelization = true )]
public class DataBaseCollectionFixture : ICollectionFixture<DataBaseFixture>
{
}
