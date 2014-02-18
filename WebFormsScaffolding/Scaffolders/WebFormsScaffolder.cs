using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using EnvDTE;
using Microsoft.AspNet.Scaffolding.EntityFramework;
using Microsoft.AspNet.Scaffolding.NuGet;
using Microsoft.AspNet.Scaffolding.WebForms.UI;
using Microsoft.AspNet.Scaffolding.Core.Metadata;
using Microsoft.AspNet.Scaffolding.WebForms.Utils;
using System.IO;


namespace Microsoft.AspNet.Scaffolding.WebForms.Scaffolders
{
    public class WebFormsScaffolder : CodeGenerator
    {

        private WebFormsCodeGeneratorViewModel _viewModel;

        internal WebFormsScaffolder(CodeGenerationContext context, CodeGeneratorInformation information)
            : base(context, information)
        {

        }

        public override void GenerateCode()
        {
            if (_viewModel == null)
            {
                throw new InvalidOperationException(Resources.WebFormsScaffolder_ShowUIAndValidateNotCalled);
            }

            Cursor currentCursor = Mouse.OverrideCursor;
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                GenerateCode(_viewModel.ModelType.CodeType,
                    _viewModel.DbContextModelType.TypeName,
                    _viewModel.DesktopMasterPage ?? String.Empty,
                    _viewModel.DesktopPlaceholderId,
                    _viewModel.OverwriteViews);
            }
            finally
            {
                Mouse.OverrideCursor = currentCursor;
            }
        }

        private void GenerateCode(CodeType modelType,
                                 string dbContextTypeName,
                                 string masterPage = null,
                                 string desktopPlaceholderId = null,
                                 bool overwriteViews = true)
        {
            Project project = Context.ActiveProject;
            Debug.Assert(project != null);

            if (modelType == null)
            {
                throw new ArgumentNullException("modelType");
            }

            IEntityFrameworkService efService = Context.ServiceProvider.GetService<IEntityFrameworkService>();
            ModelMetadata efMetadata = efService.AddRequiredEntity(Context, dbContextTypeName, modelType.FullName);

            ICodeTypeService codeTypeService = GetService<ICodeTypeService>();
            //After the above step the dbContext must have been created.
            CodeType dbContext = codeTypeService.GetCodeType(project, dbContextTypeName);

            Debug.Assert(dbContext != null, "Something wrong - the dbContext scaffolding failed...");

            var views = new[] { "Default", "Insert", "Edit", "Delete" };

            // Extract these from the selected master page : Tracked by 721707
            var sectionNames = new[] { "HeadContent", "MainContent" };

            foreach (string view in views)
            {
                AddWebFormsViewTemplates(modelType, dbContext,
                    efMetadata: efMetadata,
                    actionName: view,
                    masterPage: masterPage,
                    sectionNames: sectionNames,
                    primarySectionName: desktopPlaceholderId,
                    overwrite: overwriteViews);
            }


            AddDynamicDataTemplates();
        }

        // Passing the dialog to this method so that all scaffolder UIs
        // are modal is still an open question and tracked by bug 578173.
        public override bool ShowUIAndValidate()
        {
            _viewModel = new WebFormsCodeGeneratorViewModel(Context);

            WebFormsScaffolderDialog window = new WebFormsScaffolderDialog(_viewModel);
            bool? isOk = window.ShowModal();

            if (isOk == true)
            {
                Validate();
            }

            return (isOk == true);
        }



        private void Validate()
        {
            CodeType modelType = _viewModel.ModelType.CodeType;
            ModelType dbContextType = _viewModel.DbContextModelType;
            string dbContextTypeName = (dbContextType != null)
                ? dbContextType.TypeName
                : null;

            if (modelType == null)
            {
                throw new InvalidOperationException(Resources.WebFormsScaffolder_SelectModelType);
            }

            if (dbContextType == null || String.IsNullOrEmpty(dbContextTypeName))
            {
                throw new InvalidOperationException(Resources.WebFormsScaffolder_SelectDbContextType);
            }

            Type reflectedModelType = GetReflectionType(modelType.FullName);

            if (reflectedModelType == null)
            {
                // let's try building when reflected type is null
                var visualStudioUtils = new VisualStudioUtils();
                visualStudioUtils.BuildCurrentProject();

                // if the model STILL does not exist after building then give up and throw
                reflectedModelType = GetReflectionType(modelType.FullName);
                if (reflectedModelType == null)
                {
                    throw new InvalidOperationException(Resources.WebFormsScaffolder_ProjectNotBuilt);
                }
            }
        }




        private Type GetReflectionType(string typeName)
        {
            return GetService<IReflectedTypesService>().GetType(Context.ActiveProject, typeName);
        }

        // We are just pulling in some dependent nuget packages
        // to meet "Web Application Project" experience in this change.
        // There are some open questions regarding the experience for
        // webforms scaffolder in the case of an empty project.
        // Those details need to be worked out and
        // depending on that, we would modify the list of packages below
        // or conditions which determine when they are installed etc.
        public override IEnumerable<NuGetPackage> Dependencies
        {
            get
            {
                return GetService<IEntityFrameworkService>().Dependencies;
            }
        }

        private TService GetService<TService>() where TService : class
        {
            return (TService)ServiceProvider.GetService(typeof(TService));
        }



        private void AddDynamicDataTemplates()
        {
            AddDynamicDataEntityTemplates();
            AddDynamicDataFieldTemplates();

        }



        private void AddDynamicDataEntityTemplates()
        {
            var entityTemplates = new[] { "Default_Insert", "Default_Insert.ascx" };
            var entityTemplatesPath = "DynamicData\\EntityTemplates";
            Project project = Context.ActiveProject;

            foreach (var entityTemplate in entityTemplates)
            {
                var outputPath = Path.Combine(entityTemplatesPath, entityTemplate);

                AddFileFromTemplate(
                    project: project,
                    outputPath: outputPath,
                    templateName: entityTemplate,
                    templateParameters: new Dictionary<string, object>() 
                    {
                        {"DefaultNamespace", project.GetDefaultNamespace()}
                    },
                    skipIfExists: true);
            }
        }



        private void AddDynamicDataFieldTemplates() {
            var fieldTemplates = new[] { "Text_Edit", "Text_Edit.ascx"};
            var fieldTemplatesPath = "DynamicData\\FieldTemplates";                
            Project project = Context.ActiveProject;

            foreach (var fieldTemplate in fieldTemplates) {
                var outputPath = Path.Combine(fieldTemplatesPath, fieldTemplate);

                AddFileFromTemplate(
                    project:project,
                    outputPath:outputPath,
                    templateName: fieldTemplate,
                    templateParameters: new Dictionary<string, object>() 
                    {
                        {"DefaultNamespace", project.GetDefaultNamespace()}
                    },
                    skipIfExists: true);
            }
        }




        private void AddWebFormsViewTemplates(CodeType modelType,
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
                        {"PluralizedName", pluralizedName}
                    },
                    skipIfExists: !overwrite);
            }

        }




    }
}
