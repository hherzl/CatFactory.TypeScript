using System.Collections.Generic;
using CatFactory.CodeFactory;
using CatFactory.OOP;
using CatFactory.TypeScript.CodeFactory;
using CatFactory.TypeScript.ObjectOrientedProgramming;
using Xunit;

namespace CatFactory.TypeScript.Tests
{
    public class ModuleScaffoldingTests
    {
        [Fact]
        public void TestTypeScriptModule()
        {
            var definition = new TypeScriptModuleDefinition
            {
                Constants =
                {
                    new ConstantDefinition(AccessModifier.Public, "string", "foo", "'foo value'"),
                    new ConstantDefinition(AccessModifier.Public, "number", "bar", "1000"),
                    new ConstantDefinition(AccessModifier.Public, "boolean", "zaz", "true")
                }
            };

            TypeScriptModuleBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, "Shipping", true, definition);
        }

        [Fact]
        public void TestAngularConfigurationGeneration()
        {
            var definition = new TypeScriptModuleDefinition();

            definition.AddImport("NgModule", "@angular/core");
            definition.AddImport("RouterModule", "@angular/router");
            definition.AddImport("AppComponent", "./components/app/app.component");

            var constantLines = new List<ILine>
            {
                new CodeLine(1, "bootstrap: [],"),
                new CodeLine(1, "declarations: [],"),
                new CodeLine(1, "imports: [],"),
                new CodeLine(1, "providers: []")
            };

            definition.Constants.Add(new ConstantDefinition("NgModule", "sharedConfig", new TypeScriptObjectValue { Value = constantLines }));

            TypeScriptModuleBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, "app.module.shared", true, definition);
        }
    }
}
