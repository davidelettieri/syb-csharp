using System;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeRefactorings;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Syb.CodeGenerator
{
    [ExportCodeRefactoringProvider(LanguageNames.CSharp, Name = nameof(SybCodeGeneratorCodeRefactoringProvider)), Shared]
    internal class SybCodeGeneratorCodeRefactoringProvider : CodeRefactoringProvider
    {
        public sealed override async Task ComputeRefactoringsAsync(CodeRefactoringContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
            var node = root.FindNode(context.Span);

            if (node is TypeDeclarationSyntax typeDec)
            {
                var action = CodeAction.Create("Add GMapT auto implementation", c => AddGMapT(context.Document, typeDec, c));
                context.RegisterRefactoring(action);
                return;
            }

            if (!(node is ConstructorDeclarationSyntax constructor))
            {
                return;
            }

            if (!(constructor.Parent is TypeDeclarationSyntax parent))
            {
                return;
            }

            var document = context.Document;

            var model = await document.GetSemanticModelAsync(context.CancellationToken).ConfigureAwait(false);

            var classModel = model.GetDeclaredSymbol(parent, context.CancellationToken);

            if (classModel.Interfaces.Any(p => p.Name == "ITerm"))
            {
                var action = CodeAction.Create("Add GMapT auto implementation", c => AddGMapT(context.Document, parent, constructor, c));
                context.RegisterRefactoring(action);
            }

        }

        private async Task<Document> AddGMapT(Document document, TypeDeclarationSyntax typeDec, CancellationToken c)
        {
            var newTypeDecl = typeDec.AddMembers(GenerateGMapT(typeDec));

            var sr = await document.GetSyntaxRootAsync(c);

            var nsr = sr.ReplaceNode(typeDec, newTypeDecl);

            return document.WithSyntaxRoot(nsr);
        }

        private async Task<Document> AddGMapT(Document document, TypeDeclarationSyntax typeDecl, ConstructorDeclarationSyntax constructor, CancellationToken cancellationToken)
        {
            var newTypeDecl = typeDecl.AddMembers(GenerateGMapT(typeDecl, constructor));

            var sr = await document.GetSyntaxRootAsync(cancellationToken);

            var nsr = sr.ReplaceNode(typeDecl, newTypeDecl);

            return document.WithSyntaxRoot(nsr);
        }

        public static string GetIdentifierValue(ExpressionSyntax expression)
        {
            if (expression is IdentifierNameSyntax id)
            {
                return id.Identifier.ValueText;
            }

            throw new Exception("Something is wrong");
        }

        private MemberDeclarationSyntax GenerateGMapT(TypeDeclarationSyntax typeDecl, ConstructorDeclarationSyntax constructor)
        {
            var constructorParameters = constructor.ParameterList.Parameters.Select(p => p.Identifier.ValueText);

            var properties = constructor.Body.Statements
                                           .OfType<ExpressionStatementSyntax>()
                                           .Select(p => p.Expression)
                                           .OfType<AssignmentExpressionSyntax>()
                                           .ToDictionary(p => GetIdentifierValue(p.Right), p => GetIdentifierValue(p.Left));

            var newParameters = new System.Collections.Generic.List<ArgumentSyntax>();


            foreach (var parameterName in constructorParameters)
            {
                if (properties.TryGetValue(parameterName, out var property))
                {
                    var argument = Argument(
                                    InvocationExpression(
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            IdentifierName("lf"),
                                            IdentifierName("Apply"))).WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(IdentifierName(property))))));

                    newParameters.Add(argument);
                }
            }

            var separatedList = SeparatedList(newParameters);
            var argumentList = ArgumentList(separatedList);

            return MethodDeclaration(
                        IdentifierName(typeDecl.Identifier.ValueText),
                        Identifier("GMapT"))
                    .WithModifiers(
                        TokenList(
                            Token(SyntaxKind.PublicKeyword)))
                    .WithTypeParameterList(
                        TypeParameterList(
                            SingletonSeparatedList(
                                TypeParameter(
                                    Identifier("A")))))
                    .WithParameterList(
                        ParameterList(
                            SingletonSeparatedList(
                                Parameter(
                                    Identifier("lf"))
                                .WithType(
                                    GenericName(
                                        Identifier("MkT"))
                                    .WithTypeArgumentList(
                                        TypeArgumentList(
                                            SingletonSeparatedList<TypeSyntax>(
                                                IdentifierName("A"))))))))
                    .WithBody(
                        Block(
                            SingletonList<StatementSyntax>(
                                ReturnStatement(
                                    ObjectCreationExpression(
                                        IdentifierName(typeDecl.Identifier.ValueText))
                                    .WithArgumentList(argumentList)))));
        }

        private MemberDeclarationSyntax GenerateGMapT(TypeDeclarationSyntax typeDecl)
        {
            return MethodDeclaration(
                        IdentifierName(typeDecl.Identifier.ValueText),
                        Identifier("GMapT"))
                    .WithModifiers(
                        TokenList(
                            Token(SyntaxKind.PublicKeyword)))
                    .WithTypeParameterList(
                        TypeParameterList(
                            SingletonSeparatedList(
                                TypeParameter(
                                    Identifier("A")))))
                    .WithParameterList(
                        ParameterList(
                            SingletonSeparatedList(
                                Parameter(
                                    Identifier("lf"))
                                .WithType(
                                    GenericName(
                                        Identifier("MkT"))
                                    .WithTypeArgumentList(
                                        TypeArgumentList(
                                            SingletonSeparatedList<TypeSyntax>(
                                                IdentifierName("A"))))))))
                    .WithBody(
                        Block(
                            SingletonList<StatementSyntax>(
                                ReturnStatement(
                                    ThisExpression()))));
        }
    }
}
