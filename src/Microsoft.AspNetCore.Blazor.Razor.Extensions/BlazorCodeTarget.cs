// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Razor.Language.CodeGeneration;

namespace Microsoft.AspNetCore.Blazor.Razor
{
    /// <summary>
    /// Directs a <see cref="DocumentWriter"/> to use <see cref="BlazorIntermediateNodeWriter"/>.
    /// </summary>
    internal class BlazorCodeTarget : CodeTarget
    {
        public BlazorCodeTarget(ICodeTargetExtension[] extensions)
        {
            Extensions = extensions;
        }

        public ICodeTargetExtension[] Extensions { get; }

        public override IntermediateNodeWriter CreateNodeWriter()
            => new BlazorIntermediateNodeWriter();

        public override TExtension GetExtension<TExtension>()
        {
            for (var i = 0; i < Extensions.Length; i++)
            {
                var match = Extensions[i] as TExtension;
                if (match != null)
                {
                    return match;
                }
            }

            return null;
        }

        public override bool HasExtension<TExtension>()
        {
            for (var i = 0; i < Extensions.Length; i++)
            {
                var match = Extensions[i] as TExtension;
                if (match != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
