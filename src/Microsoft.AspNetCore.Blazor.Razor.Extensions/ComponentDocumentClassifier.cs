// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using Microsoft.AspNetCore.Razor.Language.Intermediate;

namespace Microsoft.AspNetCore.Blazor.Razor
{
    internal class ComponentDocumentClassifier : DocumentClassifierPassBase
    {
        private static readonly ICodeTargetExtension[] EmptyExtensionArray = new ICodeTargetExtension[0];
        private ICodeTargetExtension[] _targetExtensions;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var feature = Engine.Features.OfType<IRazorTargetExtensionFeature>();
            _targetExtensions = feature.FirstOrDefault()?.TargetExtensions.ToArray() ?? EmptyExtensionArray;
        }

        protected override string DocumentKind => "Blazor.Component-0.1";

        protected override bool IsMatch(RazorCodeDocument codeDocument, DocumentIntermediateNode documentNode)
        {
            // Everything is a component for now.
            return true;
        }

        protected override void OnDocumentStructureCreated(
            RazorCodeDocument codeDocument,
            NamespaceDeclarationIntermediateNode @namespace,
            ClassDeclarationIntermediateNode @class,
            MethodDeclarationIntermediateNode method)
        {
            // These are Haxxxx - we need to flow the real information through the project system.
            @namespace.Content = ((string)codeDocument.Items[BlazorCodeDocItems.Namespace]) ?? "Blazor";
            @class.ClassName = ((string)codeDocument.Items[BlazorCodeDocItems.ClassName]) ?? "MyComponent";
            @class.BaseType = BlazorComponent.FullTypeName;

            // The signature we want is like:
            //
            // public override void BuildRenderTree(RenderTreeBuilder builder)
            //
            // This isn't possible yet because we need parameter support (not part of the IR/writer today).
            method.Modifiers.Clear();
            method.Modifiers.Add("public");
            method.Modifiers.Add("override");
            method.ReturnType = "void";
            method.MethodName = BlazorComponent.BuildRenderTree;

            codeDocument.GetDocumentIntermediateNode().Target = new BlazorCodeTarget(_targetExtensions);
        }
    }
}
