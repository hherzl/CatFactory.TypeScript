using System;
using System.Diagnostics;
using System.IO;
using CatFactory.CodeFactory;
using CatFactory.ObjectOrientedProgramming;
using CatFactory.TypeScript.CodeFactory;
using CatFactory.TypeScript.ObjectOrientedProgramming;
using CatFactory.TypeScript.Tests.Models;
using Xunit;

namespace CatFactory.TypeScript.Tests
{
    public class ClassScaffoldingTests
    {
        [Fact]
        public void ScaffoldServiceClass()
        {
            var definition = new TypeScriptClassDefinition
            {
                Export = true,
                Name = "Service",
                Fields =
                {
                    new FieldDefinition(AccessModifier.Protected, "string", "url")
                    {
                        Documentation = new Documentation("Gets or sets the url")
                    }
                },
                Constructors =
                {
                    new TypeScriptClassConstructorDefinition
                    {
                        Documentation = new Documentation("Initializes a new instance of Service class"),
                        Lines =
                        {
                            new CodeLine("this.url = 'http://localhost:1234/api/v1';")
                        }
                    }
                }
            };

            foreach (var filePath in TypeScriptClassBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, definition))
            {
                Process.Start(ScaffoldingPaths.TscPath, string.Format("{0} --outDir {1}", Path.Combine(ScaffoldingPaths.TsFilesPath, filePath), ScaffoldingPaths.OutPath));
            }
        }

        [Fact]
        public void ScaffoldResponseClass()
        {
            var definition = new TypeScriptClassDefinition
            {
                Name = "Response"
            };

            foreach (var filePath in TypeScriptClassBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, definition))
            {
                Process.Start(ScaffoldingPaths.TscPath, string.Format("{0} --outDir {1}", Path.Combine(ScaffoldingPaths.TsFilesPath, filePath), ScaffoldingPaths.OutPath));
            }
        }

        [Fact]
        public void ScaffoldWarehouseServiceClass()
        {
            var definition = new TypeScriptClassDefinition
            {
                Documentation = new Documentation("Provides methods to invoke Warehouse controller in OnLineStore Web API"),
                Name = "WarehouseService",
                BaseClass = "Service",
                Constructors =
                {
                    new TypeScriptClassConstructorDefinition
                    {
                        Documentation = new Documentation("Initializes a new instance of WarehouseService class"),
                        Lines =
                        {
                            new CodeLine("super();")
                        }
                    }
                },
                Methods =
                {
                    new MethodDefinition("Response", "getProducts", new ParameterDefinition("string", "productName") { Documentation = new Documentation("Name for product") })
                    {
                        Documentation = new Documentation("Retrieves products from warehouse"),
                        Lines =
                        {
                            new TodoLine("Apply productName parameter to filter products by product name"),
                            new CodeLine(),
                            new CodeLine("return new Response();")
                        }
                    },
                    new MethodDefinition("Response", "getProduct", new ParameterDefinition("Product", "entity"))
                    {
                        Lines =
                        {
                            new TodoLine("Search product by id"),
                            new CodeLine(),
                            new CodeLine("return new Response();")
                        }
                    }
                }
            };

            definition.AddImport("Response", "./Response");
            definition.AddImport("Service", "./Service");
            definition.AddImport("Product", "./Product");

            foreach (var filePath in TypeScriptClassBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, definition))
            {
                Process.Start(ScaffoldingPaths.TscPath, string.Format("{0} --outDir {1}", Path.Combine(ScaffoldingPaths.TsFilesPath, filePath), ScaffoldingPaths.OutPath));
            }
        }

        [Fact]
        public void TestTypeScriptClassWithFieldsGeneration()
        {
            var definition = new TypeScriptClassDefinition
            {
                Documentation = new Documentation("Represents a product in wharehouse"),
                Name = "Product",
                Fields =
                {
                    new FieldDefinition(AccessModifier.Public, "number", "id"),
                    new FieldDefinition(AccessModifier.Public, "string", "productName"),
                    new FieldDefinition(AccessModifier.Public, "number", "categoryId"),
                    new FieldDefinition(AccessModifier.Public, "string", "unitPrice"),
                    new FieldDefinition(AccessModifier.Public, "string", "description"),
                    new FieldDefinition(AccessModifier.Public, "string", "tags"),
                    new FieldDefinition(AccessModifier.Public, "Date", "releaseDate")
                }
            };

            foreach (var filePath in TypeScriptClassBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, definition))
            {
                Process.Start(ScaffoldingPaths.TscPath, string.Format("{0} --outDir {1}", Path.Combine(ScaffoldingPaths.TsFilesPath, filePath), ScaffoldingPaths.OutPath));
            }
        }

        [Fact]
        public void TestRefactClass()
        {
            var definition = (new Customer()).RefactClass();

            foreach (var filePath in TypeScriptClassBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, definition))
            {
                Process.Start(ScaffoldingPaths.TscPath, string.Format("{0} --outDir {1}", Path.Combine(ScaffoldingPaths.TsFilesPath, filePath), ScaffoldingPaths.OutPath));
            }
        }

        [Fact]
        public void TestRefactAnonymous()
        {
            var definition = new
            {
                ID = 0,
                Name = "",
                UnitPrice = 0m,
                ReleaseDate = DateTime.Now,
                Description = ""
            }
            .RefactClass(name: "Anonymous");

            foreach (var filePath in TypeScriptClassBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, definition))
            {
                Process.Start(ScaffoldingPaths.TscPath, string.Format("{0} --outDir {1}", Path.Combine(ScaffoldingPaths.TsFilesPath, filePath), ScaffoldingPaths.OutPath));
            }
        }
    }
}
