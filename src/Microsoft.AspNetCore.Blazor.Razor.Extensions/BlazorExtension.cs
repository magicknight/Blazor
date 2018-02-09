// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.AspNetCore.Razor.Language.Extensions;

namespace Microsoft.AspNetCore.Blazor.Razor
{
    public static class BlazorExtension
    {
        public static void Register(IRazorEngineBuilder builder)
        {
            FunctionsDirective.Register(builder);

            builder.Features.Add(new ComponentDocumentClassifier());

            builder.Phases.Remove(builder.Phases.OfType<IRazorCSharpLoweringPhase>().Single());
            builder.Phases.Add(new BlazorLoweringPhase());
        }
    }
}
