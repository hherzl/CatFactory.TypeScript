using System;
using System.Collections.Generic;
using CatFactory.CodeFactory;
using CatFactory.OOP;
using Xunit;

namespace CatFactory.TypeScript.Tests
{
    public class CodeGenerationTests
    {
        [Fact]
        public void TestTypeScriptInterfaceWithFieldsGeneration()
        {
            var interfaceDefinition = new TypeScriptInterfaceDefinition()
            {
                Namespace = "ContactManager",
                Name = "IContact"
            };

            interfaceDefinition.Fields.Add(new FieldDefinition("string", "firstName"));
            interfaceDefinition.Fields.Add(new FieldDefinition("string", "middleName"));
            interfaceDefinition.Fields.Add(new FieldDefinition("string", "lastName"));
            interfaceDefinition.Fields.Add(new FieldDefinition("string", "gender"));
            interfaceDefinition.Fields.Add(new FieldDefinition("Date", "birthDate"));

            var classBuilder = new TypeScriptInterfaceBuilder()
            {
                ObjectDefinition = interfaceDefinition,
                OutputDirectory = "C:\\Temp\\CatFactory.TypeScript"
            };

            classBuilder.CreateFile();
        }

        [Fact]
        public void TestTypeScriptClassWithFieldsGeneration()
        {
            var classDefinition = new TypeScriptClassDefinition()
            {
                Namespace = "ContactManager",
                Name = "Contact",
                BaseClass = "Entity",
                Implements = new List<String>() { "IContact" }
            };

            classDefinition.Fields = new List<FieldDefinition>()
            {
                new FieldDefinition("string", "firstName"),
                new FieldDefinition("string", "middleName"),
                new FieldDefinition("string", "lastName"),
                new FieldDefinition("string", "gender"),
                new FieldDefinition("Date", "birthDate")
            };

            var classBuilder = new TypeScriptClassBuilder()
            {
                ObjectDefinition = classDefinition,
                OutputDirectory = "C:\\Temp\\CatFactory.TypeScript"
            };

            classBuilder.CreateFile();
        }

        [Fact]
        public void TestTypeScriptClassComponentGeneration()
        {
            var classDefinition = new TypeScriptClassDefinition()
            {
                Name = "OrderListComponent",
                Implements = new List<String>() { "OnInit" }
            };

            classDefinition.Namespaces.Add(new TypeScriptImport(new String[] { "Injectable " }, "@angular/core").ToString());

            classDefinition.Attributes.Add(new MetadataAttribute("Injectable"));

            classDefinition.Constructors.Add(new ClassConstructorDefinition());

            classDefinition.Fields = new List<FieldDefinition>()
            {
                new FieldDefinition("number", "pageSize"),
                new FieldDefinition("number", "pageNumber"),
                new FieldDefinition("string", "salesOrderNumber"),
                new FieldDefinition("string", "customerName"),
                new FieldDefinition("IListResponse<OrderSummary>", "result")
            };

            classDefinition.Methods.Add(new MethodDefinition("void", "ngOnInit"));
            classDefinition.Methods.Add(new MethodDefinition("void", "search"));
            classDefinition.Methods.Add(new MethodDefinition("void", "details")
            {
                Parameters = new List<ParameterDefinition>()
                {
                    new ParameterDefinition("OrderSummary", "order")
                }
            });

            var classBuilder = new TypeScriptClassBuilder()
            {
                ObjectDefinition = classDefinition,
                OutputDirectory = "C:\\Temp\\CatFactory.TypeScript"
            };

            classBuilder.CreateFile();
        }

        [Fact]
        public void TestTypeScriptClassWithPropertiesGeneration()
        {
            var classDefinition = new TypeScriptClassDefinition()
            {
                Namespace = "HumanResources",
                Name = "Employee"
            };

            classDefinition.Properties = new List<PropertyDefinition>()
            {
                new PropertyDefinition("string", "firstName"),
                new PropertyDefinition("string", "middleName"),
                new PropertyDefinition("string", "lastName"),
                new PropertyDefinition("string", "gender"),
                new PropertyDefinition("Date", "birthDate")
            };

            var classBuilder = new TypeScriptClassBuilder()
            {
                ObjectDefinition = classDefinition,
                OutputDirectory = "C:\\Temp\\CatFactory.TypeScript"
            };

            classBuilder.CreateFile();
        }

        [Fact]
        public void TestTypeScriptClassWithPropertiesAndMethodsGeneration()
        {
            var classDefinition = new TypeScriptClassDefinition()
            {
                Namespace = "School",
                Name = "Student"
            };

            classDefinition.Properties.Add(new PropertyDefinition("string", "firstName"));
            classDefinition.Properties.Add(new PropertyDefinition("string", "middleName"));
            classDefinition.Properties.Add(new PropertyDefinition("string", "lastName"));
            classDefinition.Properties.Add(new PropertyDefinition("string", "gender"));
            classDefinition.Properties.Add(new PropertyDefinition("Date", "birthDate"));
            classDefinition.Properties.Add(new PropertyDefinition("string", "fullName"));
            classDefinition.Properties.Add(new PropertyDefinition("number", "age"));

            classDefinition.Methods.Add(new MethodDefinition("boolean", "equals")
            {
                Parameters = new List<ParameterDefinition>()
                {
                    new ParameterDefinition("any", "obj")
                },
                Lines = new List<CodeLine>()
                {
                    new CodeLine("return false;")
                }
            });

            classDefinition.Methods.Add(new MethodDefinition("number", "getHashCode")
            {
                Lines = new List<CodeLine>()
                {
                    new CodeLine("return 0;")
                }
            });

            classDefinition.Methods.Add(new MethodDefinition("string", "toString")
            {
                Lines = new List<CodeLine>()
                {
                    new CodeLine("return \"\";")
                }
            });

            var classBuilder = new TypeScriptClassBuilder()
            {
                ObjectDefinition = classDefinition,
                OutputDirectory = "C:\\Temp\\CatFactory.TypeScript"
            };

            classBuilder.CreateFile();
        }
    }
}
