using CatFactory.TypeScript.CodeFactory;
using Xunit;

namespace CatFactory.TypeScript.Tests
{
    public class NamingConventionTests
    {
        [Fact]
        public void TestGetClassName()
        {
            var namingConvention = new TypeScriptNamingConvention();

            var name = namingConvention.GetClassName("dbService");

            Assert.True(name == "DbService");
        }

        [Fact]
        public void TestGetInterfaceName()
        {
            var namingConvention = new TypeScriptNamingConvention();

            var name = namingConvention.GetInterfaceName("repository");

            Assert.True(name == "IRepository");
        }
    }
}
