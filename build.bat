cls
set initialPath=%cd%
set srcPath=%cd%\src\CatFactory.TypeScript
set testPath=%cd%\test\CatFactory.TypeScript.Tests
cd %srcPath%
dotnet build
cd %testPath%
dotnet test
cd %srcPath%
dotnet pack
cd %initialPath%
