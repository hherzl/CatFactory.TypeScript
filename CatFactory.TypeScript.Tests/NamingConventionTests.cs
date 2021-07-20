using CatFactory.TypeScript.CodeFactory;
using Xunit;

namespace CatFactory.TypeScript.Tests
{
    public class NamingConventionTests
    {
        [Fact]
        public void GetClassName()
        {
            var namingConvention = new TypeScriptNamingConvention();

            var name = namingConvention.GetClassName("dbService");

            Assert.True(name == "DbService");
        }

        [Fact]
        public void GetInterfaceName()
        {
            var namingConvention = new TypeScriptNamingConvention();

            var name = namingConvention.GetInterfaceName("repository");

            Assert.True(name == "IRepository");
        }
    }
}
