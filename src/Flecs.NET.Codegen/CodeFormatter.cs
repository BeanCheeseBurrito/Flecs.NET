using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Flecs.NET.Codegen
{
    internal sealed class CodeFormatter : CSharpSyntaxRewriter
    {
        public static string Format(string source)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);
            SyntaxNode normalized = syntaxTree.GetRoot().NormalizeWhitespace();

            normalized = new CodeFormatter().Visit(normalized);

            return normalized.ToFullString();
        }

        private static T FormatMembers<T>(T node, IEnumerable<SyntaxNode> members) where T : SyntaxNode
        {
            SyntaxNode[] membersArray = members as SyntaxNode[] ?? members.ToArray();

            int memberCount = membersArray.Length;
            int current = 0;

            return node.ReplaceNodes(membersArray, RewriteTrivia);

            SyntaxNode RewriteTrivia<TNode>(TNode oldMember, TNode _) where TNode : SyntaxNode
            {
                string trailingTrivia = oldMember.GetTrailingTrivia().ToFullString().TrimEnd() + "\n\n";
                return current++ != memberCount - 1
                    ? oldMember.WithTrailingTrivia(SyntaxFactory.Whitespace(trailingTrivia))
                    : oldMember;
            }
        }

        public override SyntaxNode VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            return base.VisitNamespaceDeclaration(FormatMembers(node, node.Members));
        }

        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            return base.VisitClassDeclaration(FormatMembers(node, node.Members));
        }

        public override SyntaxNode VisitStructDeclaration(StructDeclarationSyntax node)
        {
            return base.VisitStructDeclaration(FormatMembers(node, node.Members));
        }
    }
}
