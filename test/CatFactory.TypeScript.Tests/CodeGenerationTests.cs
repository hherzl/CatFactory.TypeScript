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
            var interfaceDefinition = new TypeScriptInterfaceDefinition
            {
                Name = "IPerson"
            };

            interfaceDefinition.Properties.Add(new PropertyDefinition("string", "firstName"));
            interfaceDefinition.Properties.Add(new PropertyDefinition("string", "middleName"));
            interfaceDefinition.Properties.Add(new PropertyDefinition("string", "lastName"));
            interfaceDefinition.Properties.Add(new PropertyDefinition("string", "gender"));
            interfaceDefinition.Properties.Add(new PropertyDefinition("Date", "birthDate"));

            var classBuilder = new TypeScriptInterfaceBuilder
            {
                ObjectDefinition = interfaceDefinition,
                OutputDirectory = "C:\\Temp\\CatFactory.TypeScript"
            };

            classBuilder.CreateFile();
        }

        [Fact]
        public void TestTypeScriptInterfaceWithNamespaceAndPropertiesGeneration()
        {
            var interfaceDefinition = new TypeScriptInterfaceDefinition
            {
                Namespace = "ContactManager",
                Name = "IContact"
            };

            interfaceDefinition.Properties.Add(new PropertyDefinition("string", "firstName"));
            interfaceDefinition.Properties.Add(new PropertyDefinition("string", "middleName"));
            interfaceDefinition.Properties.Add(new PropertyDefinition("string", "lastName"));
            interfaceDefinition.Properties.Add(new PropertyDefinition("string", "gender"));
            interfaceDefinition.Properties.Add(new PropertyDefinition("Date", "birthDate"));

            var classBuilder = new TypeScriptInterfaceBuilder
            {
                ObjectDefinition = interfaceDefinition,
                OutputDirectory = "C:\\Temp\\CatFactory.TypeScript"
            };

            classBuilder.CreateFile();
        }

        [Fact]
        public void TestTypeScriptClassWithFieldsGeneration()
        {
            var classDefinition = new TypeScriptClassDefinition
            {
                Namespace = "ContactManager",
                Name = "Contact",
                BaseClass = "Entity",
                Implements = new List<string>() { "IContact" }
            };

            classDefinition.AddImport("IContact", "./IContact");

            classDefinition.Fields.Add(new FieldDefinition("string", "firstName"));
            classDefinition.Fields.Add(new FieldDefinition("string", "middleName"));
            classDefinition.Fields.Add(new FieldDefinition("string", "lastName"));
            classDefinition.Fields.Add(new FieldDefinition("string", "gender"));
            classDefinition.Fields.Add(new FieldDefinition("Date", "birthDate"));

            var classBuilder = new TypeScriptClassBuilder
            {
                ObjectDefinition = classDefinition,
                OutputDirectory = "C:\\Temp\\CatFactory.TypeScript"
            };

            classBuilder.CreateFile();
        }

        [Fact]
        public void TestTypeScriptClassWithPropertiesGeneration()
        {
            var classDefinition = new TypeScriptClassDefinition
            {
                Namespace = "HumanResources",
                Name = "Employee"
            };

            classDefinition.Properties.Add(new PropertyDefinition("string", "firstName"));
            classDefinition.Properties.Add(new PropertyDefinition("string", "middleName"));
            classDefinition.Properties.Add(new PropertyDefinition("string", "lastName"));
            classDefinition.Properties.Add(new PropertyDefinition("string", "gender"));
            classDefinition.Properties.Add(new PropertyDefinition("Date", "birthDate"));

            var classBuilder = new TypeScriptClassBuilder
            {
                ObjectDefinition = classDefinition,
                OutputDirectory = "C:\\Temp\\CatFactory.TypeScript"
            };

            classBuilder.CreateFile();
        }

        [Fact]
        public void TestTypeScriptClassWithPropertiesAndMethodsGeneration()
        {
            var classDefinition = new TypeScriptClassDefinition
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

            classDefinition.Methods.Add(new MethodDefinition("string", "tostring")
            {
                Lines = new List<ILine>()
                {
                    new CodeLine("return \"\";")
                }
            });

            var classBuilder = new TypeScriptClassBuilder
            {
                ObjectDefinition = classDefinition,
                OutputDirectory = "C:\\Temp\\CatFactory.TypeScript"
            };

            classBuilder.CreateFile();
        }

        [Fact]
        public void TestTypeScriptClassWithReadOnlyFieldsGeneration()
        {
            var classDefinition = new TypeScriptClassDefinition
            {
                Name = "Tokens"
            };

            classDefinition.AddConstant("number", "foo", "123");
            classDefinition.AddConstant("string", "bar", "\"hello\"");
            classDefinition.AddConstant("string", "zaz", "\"ABCDEF\"");

            var classBuilder = new TypeScriptClassBuilder
            {
                ObjectDefinition = classDefinition,
                OutputDirectory = "C:\\Temp\\CatFactory.TypeScript"
            };

            classBuilder.CreateFile();
        }

        [Fact]
        public void TestTypeScriptClassServiceGeneration()
        {
            var classDefinition = new TypeScriptClassDefinition
            {
                Name = "NorthwindService"
            };

            classDefinition.Attributes.Add(new MetadataAttribute("Injectable"));

            classDefinition.AddImport("Injectable", "@angular/core");
            classDefinition.AddImport(new string[] { "Http", "Response" }, "@angular/http");
            classDefinition.AddImport("Observable", "rxjs/Observable");

            classDefinition.Constructors.Add(new ClassConstructorDefinition(new ParameterDefinition(AccessModifier.Private, "Http", "http"))
            {
                Lines = new List<ILine>()
                {
                    new CodeLine("this.api = \"{0}\";", "api/Northwind")
                }
            });

            classDefinition.Fields.Add(new FieldDefinition("string", "api"));

            classDefinition.Methods.Add(new MethodDefinition("Observable<Response>", "getOrders", new ParameterDefinition("number", "pageNumber"), new ParameterDefinition("number", "pageSize"))
            {
                Lines = new List<ILine>()
                {
                    new CodeLine("var url = [this.api, \"Sales\", \"Order\"].join(\"/\");"),
                    new CodeLine(),
                    new CodeLine("return this.http.get(url);")
                }
            });

            classDefinition.Methods.Add(new MethodDefinition("Observable<Response>", "getOrder", new ParameterDefinition("number", "id"))
            {
                Lines = new List<ILine>()
                {
                    new CodeLine("var url = [this.api, \"Sales\", \"Order\", id].join(\"/\");"),
                    new CodeLine(),
                    new CodeLine("return this.http.get(url);")
                }
            });

            var classBuilder = new TypeScriptClassBuilder
            {
                ObjectDefinition = classDefinition,
                OutputDirectory = "C:\\Temp\\CatFactory.TypeScript"
            };

            classBuilder.CreateFile();
        }

        [Fact]
        public void TestTypeScriptClassComponentGeneration()
        {
            var classDefinition = new TypeScriptClassDefinition
            {
                Name = "OrderListComponent",
                Implements = new List<string>() { "OnInit" }
            };

            classDefinition.AddImport(new string[] { "Component", "Injectable", "OnInit" }, "@angular/core");
            classDefinition.AddImport("Router", "@angular/router");
            classDefinition.AddImport("SalesService", "../../services/sales.service");
            classDefinition.AddImport("OrderSummary", "../../models/order.summary");
            classDefinition.AddImport("IListResponse", "../../responses/list.response");

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

            classDefinition.Methods.Add(new MethodDefinition("void", "ngOnInit")
            {
                Lines = new List<ILine>()
                {
                    new TodoLine("Add logic for this operation")
                }
            });

            classDefinition.Methods.Add(new MethodDefinition("void", "search")
            {
                Lines = new List<ILine>()
                {
                    new TodoLine("Add logic for this operation")
                }
            });

            classDefinition.Methods.Add(new MethodDefinition("void", "details", new ParameterDefinition("OrderSummary", "order"))
            {
                Lines = new List<ILine>()
                {
                    new TodoLine("Add logic for this operation")
                }
            });

            var classBuilder = new TypeScriptClassBuilder
            {
                ObjectDefinition = classDefinition,
                OutputDirectory = "C:\\Temp\\CatFactory.TypeScript"
            };

            classBuilder.CreateFile();
        }

        [Fact]
        public void TestTypeScriptModule()
        {
            var moduleDefinition = new TypeScriptModuleDefinition();

            moduleDefinition.Constants.Add(new ConstantDefinition(AccessModifier.Public, "string", "foo", "\"foo value\""));

            var codeBuilder = new TypeScriptModuleBuilder(moduleDefinition)
            {
                ModuleFileName = "Shipping",
                OutputDirectory = "C:\\Temp\\CatFactory.TypeScript"
            };

            codeBuilder.CreateFile();
        }

        [Fact]
        public void TestAngularConfigurationGeneration()
        {
            var moduleDefinition = new TypeScriptModuleDefinition();

            moduleDefinition.AddImport("NgModule", "@angular/core");
            moduleDefinition.AddImport("RouterModule", "@angular/router");
            moduleDefinition.AddImport("AppComponent", "./components/app/app.component");

            var constantLines = new List<ILine>()
            {
                new CodeLine(1, "bootstrap: [],"),
                new CodeLine(1, "declarations: [],"),
                new CodeLine(1, "imports: [],"),
                new CodeLine(1, "providers: []")
            };

            moduleDefinition.Constants.Add(new ConstantDefinition("NgModule", "sharedConfig", new TypeScriptObjectValue { Value = constantLines }));

            var codeBuilder = new TypeScriptModuleBuilder(moduleDefinition)
            {
                ModuleFileName = "app.module.shared",
                OutputDirectory = "C:\\Temp\\CatFactory.TypeScript"
            };

            codeBuilder.CreateFile();
        }

        [Fact]
        public void TestRefactInterface()
        {
            var classDefinition = new TypeScriptClassDefinition
            {
                Name = "Gamer",
                Implements = new List<string>() { "IGamer" }
            };

            classDefinition.AddImport("IGamer", "./IGamer");

            classDefinition.Properties.Add(new PropertyDefinition("string", "firstName"));
            classDefinition.Properties.Add(new PropertyDefinition("string", "middleName"));
            classDefinition.Properties.Add(new PropertyDefinition("string", "lastName"));

            var classBuilder = new TypeScriptClassBuilder
            {
                ObjectDefinition = classDefinition,
                OutputDirectory = "C:\\Temp\\CatFactory.TypeScript"
            };

            classBuilder.CreateFile();

            var interfaceDefinition = classDefinition.RefactInterface();

            var interfaceBuilder = new TypeScriptInterfaceBuilder
            {
                ObjectDefinition = interfaceDefinition,
                OutputDirectory = "C:\\Temp\\CatFactory.TypeScript"
            };

            interfaceBuilder.CreateFile();
        }

        [Fact]
        public void TestRefactClass()
        {
            var customerClassDefinition = (new Customer()).RefactClass();

            var classBuilder = new TypeScriptClassBuilder
            {
                ObjectDefinition = customerClassDefinition,
                OutputDirectory = "C:\\Temp\\CatFactory.TypeScript"
            };

            classBuilder.CreateFile();
        }
    }
}
