using DigitalSkynet.DotnetCore.TestInfrastructure;
using DigitalSkynet.DotnetCore.Tests.Fakes.DataAccess;

namespace DigitalSkynet.DotnetCore.Tests.Infrastructure;

public class BaseDbTest : BaseDbTest<Startup, DummyDbContext>
{ }
