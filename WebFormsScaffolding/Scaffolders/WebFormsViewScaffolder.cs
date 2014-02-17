using EnvDTE;
using Microsoft.AspNet.Scaffolding;
using Microsoft.AspNet.Scaffolding.Core.Metadata;
using Microsoft.AspNet.Scaffolding.EntityFramework;
using Microsoft.AspNet.Scaffolding.WebForms.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;

namespace Microsoft.AspNet.Scaffolding.WebForms.Scaffolders
{
    public class WebFormsViewScaffolder : CodeGenerator
    {
        internal WebFormsViewScaffolder(CodeGenerationContext context, CodeGeneratorInformation information)
            : base(context, information)
        {

        }

        public override bool ShowUIAndValidate()
        {
            throw new NotSupportedException();
        }

        public override void GenerateCode()
        {
            throw new NotSupportedException();
        }

        public void GenerateCode(CodeType modelType,
                                CodeType dbContext,
                                ModelMetadata efMetadata,
                                string actionName,
                                string masterPage = "",
                                string[] sectionNames = null,
                                string primarySectionName = "",
                                bool overwrite = false)
        {
            if (modelType == null)
            {
                throw new ArgumentNullException("modelType");
            }
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (String.IsNullOrEmpty(actionName))
            {
                throw new ArgumentException(Resources.WebFormsViewScaffolder_EmptyActionName, "actionName");
            }

            PropertyMetadata primaryKey = efMetadata.PrimaryKeys.FirstOrDefault();
            string pluralizedName = efMetadata.EntitySetName;

            string outputPath = Path.Combine(modelType.Name, actionName);
            string modelNameSpace = modelType.Namespace != null ? modelType.Namespace.FullName : String.Empty;
            string dbContextNameSpace = dbContext.Namespace != null ? dbContext.Namespace.FullName : String.Empty;

            List<string> actionTemplates = new List<string>();
            actionTemplates.AddRange(new string[] { actionName, actionName + ".aspx" });

            // Scaffold aspx page and code behind
            foreach (string action in actionTemplates)
            {
                Project project = Context.ActiveProject;

                AddFileFromTemplate(project,
                    outputPath,
                    templateName: action,
                    templateParameters: new Dictionary<string, object>() 
                    {
                        {"DefaultNamespace", project.GetDefaultNamespace()},
                        {"Namespace", modelNameSpace},
                        {"IsContentPage", !String.IsNullOrEmpty(masterPage)},
                        {"MasterPageFile", masterPage},
                        {"SectionNames", sectionNames},
                        {"PrimarySectionName", primarySectionName},
                        {"PrimaryKeyMetadata", primaryKey},
                        {"PrimaryKeyName", primaryKey.PropertyName},
                        {"ViewDataType", modelType},
                        {"ViewDataTypeName", modelType.Name},
                        {"DBContextType", dbContext.Name},
                        {"DBContextNamespace", dbContextNameSpace},
                        {"PluralizedName", pluralizedName},
                    },
                    skipIfExists: !overwrite);
            }
        }
    }
}
