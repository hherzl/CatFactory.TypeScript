using CatFactory.CodeFactory;
using CatFactory.OOP;
using CatFactory.TypeScript.CodeFactory;
using CatFactory.TypeScript.ObjectOrientedProgramming;
using CatFactory.TypeScript.Tests.Models;
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
                },
                Fields =
                {
                    new FieldDefinition("string", "firstName"),
                    new FieldDefinition("string", "middleName"),
                    new FieldDefinition("string", "lastName"),
                    new FieldDefinition("string", "gender"),
                    new FieldDefinition("Date", "birthDate")
                }
            };

            definition.AddImport("IContact", "./IContact");
            definition.AddImport("Entity", "./Entity");

            TypeScriptClassBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, definition);
        }

        [Fact]
        public void TestTypeScriptClassWithPropertiesGeneration()
        {
            var definition = new TypeScriptClassDefinition
            {
                Namespace = "HumanResources",
                Name = "Employee",
                Fields =
                {
                    new FieldDefinition("string", "m_firstName"),
                    new FieldDefinition("string", "m_middleName"),
                    new FieldDefinition("string", "m_lastName"),
                    new FieldDefinition("string", "m_gender"),
                    new FieldDefinition("Date", "m_birthDate")
                },
                Properties =
                {
                    new PropertyDefinition("string", "firstName"),
                    new PropertyDefinition("string", "middleName"),
                    new PropertyDefinition("string", "lastName"),
                    new PropertyDefinition("string", "gender"),
                    new PropertyDefinition("Date", "birthDate")
                }
            };

            TypeScriptClassBuilder.CreateFiles("C:\\Temp\\CatFactory.TypeScript", string.Empty, true, definition);
        }

        [Fact]
        public void TestTypeScriptClassWithPropertiesAndMethodsGeneration()
        {
            var definition = new TypeScriptClassDefinition
            {
                Namespace = "School",
                Name = "Student",
                Fields =
                {
                    new FieldDefinition("string", "m_firstName"),
                    new FieldDefinition("string", "m_middleName"),
                    new FieldDefinition("string", "m_lastName"),
                    new FieldDefinition("string", "m_gender"),
                    new FieldDefinition("Date", "m_birthDate"),
                    new FieldDefinition("string", "m_fullName"),
                    new FieldDefinition("number", "m_age")
                },
                Properties =
                {
                    new PropertyDefinition("string", "firstName"),
                    new PropertyDefinition("string", "middleName"),
                    new PropertyDefinition("string", "lastName"),
                    new PropertyDefinition("string", "gender"),
                    new PropertyDefinition("Date", "birthDate"),
                    new PropertyDefinition("string", "fullName"),
                    new PropertyDefinition("number", "age")
                }
            };

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
                Attributes =
                {
                    new MetadataAttribute("Injectable")
                },
                Name = "NorthwindService"
            };

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
