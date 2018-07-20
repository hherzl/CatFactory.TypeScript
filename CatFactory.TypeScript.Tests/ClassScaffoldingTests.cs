using CatFactory.CodeFactory;
using CatFactory.OOP;
using Xunit;

namespace CatFactory.TypeScript.Tests
{
    public class ClassScaffoldingTests
    {
        [Fact]
        public void TestTypeScriptBaseClassGeneration()
        {
            var definition = new TypeScriptClassDefinition
            {
                Name = "Entity"
            };

            TypeScriptClassBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, definition);
        }

        [Fact]
        public void TestTypeScriptClassWithFieldsGeneration()
        {
            var definition = new TypeScriptClassDefinition
            {
                Namespace = "ContactManager",
                Name = "Contact",
                BaseClass = "Entity",
                Implements =
                {
                    "IContact"
                }
            };

            definition.AddImport("IContact", "./IContact");
            definition.AddImport("Entity", "./Entity");

            definition.Fields.Add(new FieldDefinition("string", "firstName"));
            definition.Fields.Add(new FieldDefinition("string", "middleName"));
            definition.Fields.Add(new FieldDefinition("string", "lastName"));
            definition.Fields.Add(new FieldDefinition("string", "gender"));
            definition.Fields.Add(new FieldDefinition("Date", "birthDate"));

            TypeScriptClassBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, definition);
        }

        [Fact]
        public void TestTypeScriptClassWithPropertiesGeneration()
        {
            var definition = new TypeScriptClassDefinition
            {
                Namespace = "HumanResources",
                Name = "Employee"
            };

            definition.Fields.Add(new FieldDefinition("string", "m_firstName"));
            definition.Fields.Add(new FieldDefinition("string", "m_middleName"));
            definition.Fields.Add(new FieldDefinition("string", "m_lastName"));
            definition.Fields.Add(new FieldDefinition("string", "m_gender"));
            definition.Fields.Add(new FieldDefinition("Date", "m_birthDate"));

            definition.Properties.Add(new PropertyDefinition("string", "firstName"));
            definition.Properties.Add(new PropertyDefinition("string", "middleName"));
            definition.Properties.Add(new PropertyDefinition("string", "lastName"));
            definition.Properties.Add(new PropertyDefinition("string", "gender"));
            definition.Properties.Add(new PropertyDefinition("Date", "birthDate"));

            TypeScriptClassBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, definition);
        }

        [Fact]
        public void TestTypeScriptClassWithPropertiesAndMethodsGeneration()
        {
            var definition = new TypeScriptClassDefinition
            {
                Namespace = "School",
                Name = "Student"
            };

            definition.Fields.Add(new FieldDefinition("string", "m_firstName"));
            definition.Fields.Add(new FieldDefinition("string", "m_middleName"));
            definition.Fields.Add(new FieldDefinition("string", "m_lastName"));
            definition.Fields.Add(new FieldDefinition("string", "m_gender"));
            definition.Fields.Add(new FieldDefinition("Date", "m_birthDate"));
            definition.Fields.Add(new FieldDefinition("string", "m_fullName"));
            definition.Fields.Add(new FieldDefinition("number", "m_age"));

            definition.Properties.Add(new PropertyDefinition("string", "firstName"));
            definition.Properties.Add(new PropertyDefinition("string", "middleName"));
            definition.Properties.Add(new PropertyDefinition("string", "lastName"));
            definition.Properties.Add(new PropertyDefinition("string", "gender"));
            definition.Properties.Add(new PropertyDefinition("Date", "birthDate"));
            definition.Properties.Add(new PropertyDefinition("string", "fullName"));
            definition.Properties.Add(new PropertyDefinition("number", "age"));

            definition.Methods.Add(new MethodDefinition("boolean", "equals", new ParameterDefinition("any", "obj"))
            {
                Lines =
                {
                    new CodeLine("return false;")
                }
            });

            definition.Methods.Add(new MethodDefinition("number", "getHashCode")
            {
                Lines =
                {
                    new CodeLine("return 0;")
                }
            });

            definition.Methods.Add(new MethodDefinition("string", "tostring")
            {
                Lines =
                {
                    new CodeLine("return '';")
                }
            });

            TypeScriptClassBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, definition);
        }

        [Fact]
        public void TestTypeScriptClassWithReadOnlyFieldsGeneration()
        {
            var definition = new TypeScriptClassDefinition
            {
                Name = "Tokens"
            };

            definition.AddConstant("number", "foo", "123");
            definition.AddConstant("string", "bar", "'hello'");
            definition.AddConstant("string", "zaz", "\"ABCDEF\"");

            TypeScriptClassBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, definition);
        }

        [Fact]
        public void TestTypeScriptClassServiceGeneration()
        {
            var definition = new TypeScriptClassDefinition
            {
                Name = "NorthwindService"
            };

            definition.Attributes.Add(new MetadataAttribute("Injectable"));

            definition.AddImport("Injectable", "@angular/core");
            definition.AddImport(new string[] { "Http", "Response" }, "@angular/http");
            definition.AddImport("Observable", "rxjs/Observable");

            definition.Constructors.Add(new TypeScriptClassConstructorDefinition(new TypeScriptParameterDefinition("Http", "http"))
            {
                Lines =
                {
                    new CodeLine("this.api = '{0}';", "api/Northwind")
                }
            });

            definition.Fields.Add(new FieldDefinition("string", "api"));

            definition.Methods.Add(new MethodDefinition("Observable<Response>", "getOrders", new ParameterDefinition("number", "pageNumber"), new ParameterDefinition("number", "pageSize"))
            {
                Lines =
                {
                    new CodeLine("return this.http.get([this.api, 'Sales', 'Order'].join('/'));")
                }
            });

            definition.Methods.Add(new MethodDefinition("Observable<Response>", "getOrder", new ParameterDefinition("number", "id"))
            {
                Lines =
                {
                    new CodeLine("return this.http.get([this.api, 'Sales', 'Order', id].join('/'));")
                }
            });

            TypeScriptClassBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, definition);
        }

        [Fact]
        public void TestTypeScriptClassComponentGeneration()
        {
            var definition = new TypeScriptClassDefinition
            {
                Name = "OrderListComponent",
                Implements =
                {
                    "OnInit"
                }
            };

            definition.AddImport(new string[] { "Component", "Injectable", "OnInit" }, "@angular/core");
            definition.AddImport("Router", "@angular/router");
            definition.AddImport("SalesService", "../../services/sales.service");
            definition.AddImport("OrderSummary", "../../models/order.summary");
            definition.AddImport("IListResponse", "../../responses/list.response");

            definition.Attributes.Add(new MetadataAttribute("Component")
            {
                Sets =
                {
                    new MetadataAttributeSet("selector", "'order-list'"),
                    new MetadataAttributeSet("template", "require('./order-list.component.html')")
                }
            });

            definition.Constructors.Add(new TypeScriptClassConstructorDefinition(
                new TypeScriptParameterDefinition(AccessModifier.Private, "Router", "router"),
                new TypeScriptParameterDefinition(AccessModifier.Private, "SalesService", "service"))
            );

            definition.Fields.Add(new FieldDefinition("number", "pageSize"));
            definition.Fields.Add(new FieldDefinition("number", "pageNumber"));
            definition.Fields.Add(new FieldDefinition("string", "salesOrderNumber"));
            definition.Fields.Add(new FieldDefinition("string", "customerName"));
            definition.Fields.Add(new FieldDefinition("IListResponse<OrderSummary>", "result"));

            definition.Methods.Add(new MethodDefinition("void", "ngOnInit")
            {
                Lines =
                {
                    new TodoLine("Add logic for this operation")
                }
            });

            definition.Methods.Add(new MethodDefinition("void", "search")
            {
                Lines =
                {
                    new TodoLine("Add logic for this operation")
                }
            });

            definition.Methods.Add(new MethodDefinition("void", "details", new ParameterDefinition("OrderSummary", "order"))
            {
                Lines =
                {
                    new TodoLine("Add logic for this operation")
                }
            });

            TypeScriptClassBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, definition);
        }

        [Fact]
        public void TestRefactClass()
        {
            var definition = (new Customer()).RefactClass();

            TypeScriptClassBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, definition);
        }

        [Fact]
        public void TestRefactAnonymous()
        {
            var definition = (new { ID = 0, Name = "", Description = "" }).RefactClass(name: "Anonymous");

            TypeScriptClassBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, definition);
        }
    }
}
