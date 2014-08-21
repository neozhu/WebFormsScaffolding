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
using Microsoft.AspNet.Scaffolding.WebForms.UI;

namespace Microsoft.AspNet.Scaffolding.WebForms.Scaffolders
{    

    // This is where everything with the scaffolder is kicked off. The factory
    // returns a WebFormsScaffolder when a project meets the requirements.

    [Export(typeof(CodeGeneratorFactory))]
    public class WebFormsScaffolderFactory : CodeGeneratorFactory
    {
        public WebFormsScaffolderFactory()
            : base(CreateCodeGeneratorInformation())
        {

        }

        public override ICodeGenerator CreateInstance(CodeGenerationContext context)
        {
            return new WebFormsScaffolder(context, Information);
        }
      
        // We support CSharp or VB WAPs targetting at least .Net Framework 4.5 or above.
        public override bool IsSupported(CodeGenerationContext codeGenerationContext)
        {
            if (ProjectLanguage.CSharp.Equals(codeGenerationContext.ActiveProject.GetCodeLanguage()) || ProjectLanguage.VisualBasic.Equals(codeGenerationContext.ActiveProject.GetCodeLanguage()))
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
                displayName: Resources.WebFormsScaffolder_Name,
                description: Resources.WebFormsScaffolder_Description,
                author: "Outercurve Foundation",
                version: new Version(1, 0, 0, 0),
                id: typeof(WebFormsScaffolder).Name,
                icon: null,
                gestures: null,
                categories: new[] { "Common/Web Forms" }
            );              
        }
    }
}
