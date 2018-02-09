// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Blazor.Razor;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis.Razor;

namespace Microsoft.VisualStudio.LanguageServices.Blazor
{
    [ExportCustomTemplateEngineFactory("Blazor-0.1", SupportsSerialization = true)]
    internal class BlazorCustomTemplateEngineFactory : ICustomTemplateEngineFactory
    {
        public RazorTemplateEngine Create(RazorConfiguration configuration, RazorProject project, Action<IRazorEngineBuilder> configure)
        {
            var engine = RazorEngine.CreateDesignTime(BlazorExtension.Register);
            return new RazorTemplateEngine(engine, project);
        }
    }
}
