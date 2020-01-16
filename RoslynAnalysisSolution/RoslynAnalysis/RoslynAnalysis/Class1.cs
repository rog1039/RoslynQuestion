using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using Xunit;
using Xunit.Abstractions;

namespace RoslynAnalysis
{
    public class Class1
    {
        private readonly ITestOutputHelper _testOutputHelper;

        private readonly string _solutionToAnalyzePath =
            @"..\..\..\..\..\SampleCodeSolution\RoslynTestSolution\RoslynTestSolution.sln";

        [Fact()]
        public async Task InspectPropertyTypeTestWithProjectCompilation()
        {
            using (var workspace = MSBuildWorkspace.Create())
            {
                var solution = await workspace.OpenSolutionAsync(_solutionToAnalyzePath);
                foreach (var project in solution.Projects)
                {
                    var projectCompilation = await project.GetCompilationAsync();
                    foreach (var document in project.Documents)
                    {
                        var syntaxTree         = await document.GetSyntaxTreeAsync();
                        var syntaxRoot         = await syntaxTree.GetRootAsync();
                        var semanticModel      = projectCompilation.GetSemanticModel(syntaxTree);
                        var memberDeclarations = syntaxRoot.DescendantNodes().OfType<MemberDeclarationSyntax>();

                        foreach (var memberDeclaration in memberDeclarations)
                        {
                            switch (memberDeclaration)
                            {
                                case PropertyDeclarationSyntax propertyDeclarationSyntax:
                                {
                                    var propertySymbol = semanticModel.GetDeclaredSymbol(propertyDeclarationSyntax);

                                    PrintPropertyInfoToConsole(propertySymbol);
                                    
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        [Fact()]
        public async Task InspectPropertyTypeTestWithoutProjectCompilation()
        {
            using (var workspace = MSBuildWorkspace.Create())
            {
                var solution = await workspace.OpenSolutionAsync(_solutionToAnalyzePath);
                foreach (var project in solution.Projects)
                {
                    foreach (var document in project.Documents)
                    {
                        var syntaxTree         = await document.GetSyntaxTreeAsync();
                        var syntaxRoot         = await syntaxTree.GetRootAsync();
                        var semanticModel      = await document.GetSemanticModelAsync();
                        var memberDeclarations = syntaxRoot.DescendantNodes().OfType<MemberDeclarationSyntax>();

                        foreach (var memberDeclaration in memberDeclarations)
                        {
                            switch (memberDeclaration)
                            {
                                case PropertyDeclarationSyntax propertyDeclarationSyntax:
                                {
                                    var propertySymbol = semanticModel.GetDeclaredSymbol(propertyDeclarationSyntax);

                                    PrintPropertyInfoToConsole(propertySymbol);

                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void PrintPropertyInfoToConsole(IPropertySymbol propertySymbol)
        {
            var typeInfo =
                $"Property Type: {propertySymbol.Type.ToDisplayString(),-18}:{propertySymbol.Type.ContainingAssembly.ToDisplayString(),-90} |";
            var propInfo = $"Property: {propertySymbol.ToDisplayString()}";
            _testOutputHelper.WriteLine($"{typeInfo} - {propInfo}");
        }

        public Class1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
    }
}