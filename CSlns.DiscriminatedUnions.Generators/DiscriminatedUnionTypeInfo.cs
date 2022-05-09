using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace CSlns.DiscriminatedUnions.Generators;


internal record DiscriminatedUnionTypeInfo(
    TypeDeclarationSyntax TypeDeclaration,
    ITypeSymbol TypeSymbol,
    INamedTypeSymbol? CasesType
);