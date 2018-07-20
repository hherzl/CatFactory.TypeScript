cls
set initialPath=%cd%
set srcPath=%cd%\CatFactory.TypeScript
set testPath=%cd%\CatFactory.TypeScript.Tests
cd %srcPath%
dotnet build
cd %testPath%
dotnet test
cd %srcPath%
dotnet pack
cd %initialPath%
pause
