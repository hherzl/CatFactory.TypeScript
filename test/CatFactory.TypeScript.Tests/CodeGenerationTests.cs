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
        public void TestTypeScriptInterfaceWithPropertiesGeneration()
        {
            var interfaceDefinition = new TypeScriptInterfaceDefinition()
            {
                Name = "IPerson"
            };

            interfaceDefinition.Properties.Add(new PropertyDefinition("string", "firstName"));
            interfaceDefinition.Properties.Add(new PropertyDefinition("string", "middleName"));
            interfaceDefinition.Properties.Add(new PropertyDefinition("string", "lastName"));
            interfaceDefinition.Properties.Add(new PropertyDefinition("string", "gender"));
            interfaceDefinition.Properties.Add(new PropertyDefinition("Date", "birthDate"));

            var classBuilder = new TypeScriptInterfaceBuilder()
            {
                ObjectDefinition = interfaceDefinition,
                OutputDirectory = "C:\\Temp\\CatFactory.TypeScript"
            };

            classBuilder.CreateFile();
        }

        [Fact]
        public void TestTypeScriptInterfaceWithNamespaceAndPropertiesGeneration()
        {
            var interfaceDefinition = new TypeScriptInterfaceDefinition()
            {
                Namespace = "ContactManager",
                Name = "IContact"
            };

            interfaceDefinition.Properties.Add(new PropertyDefinition("string", "firstName"));
            interfaceDefinition.Properties.Add(new PropertyDefinition("string", "middleName"));
            interfaceDefinition.Properties.Add(new PropertyDefinition("string", "lastName"));
            interfaceDefinition.Properties.Add(new PropertyDefinition("string", "gender"));
            interfaceDefinition.Properties.Add(new PropertyDefinition("Date", "birthDate"));

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

            classDefinition.AddImport(new String[] { "IContact" }, "./IContact");

            classDefinition.Fields.Add(new FieldDefinition("string", "firstName"));
            classDefinition.Fields.Add(new FieldDefinition("string", "middleName"));
            classDefinition.Fields.Add(new FieldDefinition("string", "lastName"));
            classDefinition.Fields.Add(new FieldDefinition("string", "gender"));
            classDefinition.Fields.Add(new FieldDefinition("Date", "birthDate"));

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

            classDefinition.AddImport(new String[] { "Injectable" }, "@angular/core");
            classDefinition.AddImport(new String[] { "Component", "OnInit" }, "@angular/core");
            classDefinition.AddImport(new String[] { "Router" }, "@angular/router");
            classDefinition.AddImport(new String[] { "IListResponse" }, "../../responses/list.response");
            classDefinition.AddImport(new String[] { "OrderSummary" }, "../../models/order.summary");
            classDefinition.AddImport(new String[] { "SalesService" }, "../../services/sales.service");

            classDefinition.Attributes.Add(new MetadataAttribute("Component")
            {
                Sets = new List<MetadataAttributeSet>()
                {
                    new MetadataAttributeSet("selector", "\"order-list\""),
                    new MetadataAttributeSet("template", "require(\"./order-list.component.html\")")
                }
            });

            classDefinition.Constructors.Add(new ClassConstructorDefinition(
                new ParameterDefinition(AccessModifier.Private, "Router", "router"),
                new ParameterDefinition(AccessModifier.Private, "SalesService", "service"))
            );

            classDefinition.Fields.Add(new FieldDefinition("number", "pageSize"));
            classDefinition.Fields.Add(new FieldDefinition("number", "pageNumber"));
            classDefinition.Fields.Add(new FieldDefinition("string", "salesOrderNumber"));
            classDefinition.Fields.Add(new FieldDefinition("string", "customerName"));
            classDefinition.Fields.Add(new FieldDefinition("IListResponse<OrderSummary>", "result"));

            classDefinition.Methods.Add(new MethodDefinition("void", "ngOnInit"));
            classDefinition.Methods.Add(new MethodDefinition("void", "search"));
            classDefinition.Methods.Add(new MethodDefinition("void", "details", new ParameterDefinition("OrderSummary", "order")));

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

            classDefinition.Properties.Add(new PropertyDefinition("string", "firstName"));
            classDefinition.Properties.Add(new PropertyDefinition("string", "middleName"));
            classDefinition.Properties.Add(new PropertyDefinition("string", "lastName"));
            classDefinition.Properties.Add(new PropertyDefinition("string", "gender"));
            classDefinition.Properties.Add(new PropertyDefinition("Date", "birthDate"));

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

            classDefinition.Methods.Add(new MethodDefinition("boolean", "equals", new ParameterDefinition("any", "obj"))
            {
                Lines = new List<ILine>()
                {
                    new CodeLine("return false;")
                }
            });

            classDefinition.Methods.Add(new MethodDefinition("number", "getHashCode")
            {
                Lines = new List<ILine>()
                {
                    new CodeLine("return 0;")
                }
            });

            classDefinition.Methods.Add(new MethodDefinition("string", "toString")
            {
                Lines = new List<ILine>()
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

        [Fact]
        public void TestTypeScriptClassWithReadOnlyFieldsGeneration()
        {
            var classDefinition = new TypeScriptClassDefinition()
            {
                Name = "Tokens"
            };

            classDefinition.Fields.Add(new FieldDefinition("number", "foo") { IsStatic = true, IsReadOnly = true, Value = "123" });
            classDefinition.Fields.Add(new FieldDefinition("string", "bar") { IsStatic = true, IsReadOnly = true, Value = "\"hello\"" });

            var classBuilder = new TypeScriptClassBuilder()
            {
                ObjectDefinition = classDefinition,
                OutputDirectory = "C:\\Temp\\CatFactory.TypeScript"
            };

            classBuilder.CreateFile();
        }
    }
}
