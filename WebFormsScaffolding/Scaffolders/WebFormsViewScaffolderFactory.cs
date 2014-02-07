using EnvDTE;
using Microsoft.AspNet.Scaffolding;
using Microsoft.AspNet.Scaffolding.NuGet;
using Microsoft.AspNet.Scaffolding.EntityFramework.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;

namespace Microsoft.AspNet.Scaffolding.WebForms.Scaffolders
{
    public class WebFormsViewScaffolderFactory : CodeGeneratorFactory
    {
        public WebFormsViewScaffolderFactory()
            : base(CreateCodeGeneratorInformation())
        {

        }

        public override ICodeGenerator CreateInstance(CodeGenerationContext context)
        {
            return new WebFormsViewScaffolder(context, Information);
        }
        
        // We support CSharp WAPs targetting atleast .NetFramework 4.5 or above.
        public override bool IsSupported(CodeGenerationContext codeGenerationContext)
        {
            if (ProjectLanguage.CSharp.Equals(codeGenerationContext.ActiveProject.GetCodeLanguage()))
            {
                FrameworkName targetFramework = codeGenerationContext.ActiveProject.GetTargetFramework();
                return (targetFramework != null) &&
                        String.Equals(".NetFramework", targetFramework.Identifier, StringComparison.OrdinalIgnoreCase) &&
                        targetFramework.Version >= new Version(4, 5);
            }

            return false;
        }

        private static CodeGeneratorInformation CreateCodeGeneratorInformation()
        {
            return new CodeGeneratorInformation(
                displayName: "not exported, not used",
                description: "not exported, not used",
                author: "Microsoft",
                version: new Version(1, 0, 0, 0),
                id: typeof(WebFormsViewScaffolder).Name
                );
        }
    }
}
