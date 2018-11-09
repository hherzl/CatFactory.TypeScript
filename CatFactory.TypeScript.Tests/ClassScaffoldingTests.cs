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
                Name = "Service",
                Fields =
                {
                    new FieldDefinition("string", "url")
                },
                Constructors =
                {
                    new TypeScriptClassConstructorDefinition
                    {
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
                Name = "WarehouseService",
                BaseClass = "Service",
                Constructors =
                {
                    new TypeScriptClassConstructorDefinition
                    {
                        Lines =
                        {
                            new CodeLine("super();")
                        }
                    }
                },
                Methods =
                {
                    new MethodDefinition("Response", "getProducts", new ParameterDefinition("string", "productName"))
                    {
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
                Name = "Product",
                Fields =
                {
                    new FieldDefinition("number", "id"),
                    new FieldDefinition("string", "productName"),
                    new FieldDefinition("number", "categoryId"),
                    new FieldDefinition("string", "unitPrice"),
                    new FieldDefinition("string", "description"),
                    new FieldDefinition("string", "tags"),
                    new FieldDefinition("Date", "releaseDate")
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
            var definition = (new { ID = 0, Name = "", UnitPrice = 0m, ReleaseDate = DateTime.Now, Description = "" }).RefactClass(name: "Anonymous");

            foreach (var filePath in TypeScriptClassBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, definition))
            {
                Process.Start(ScaffoldingPaths.TscPath, string.Format("{0} --outDir {1}", Path.Combine(ScaffoldingPaths.TsFilesPath, filePath), ScaffoldingPaths.OutPath));
            }
        }
    }
}
