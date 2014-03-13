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

                GenerateCode(
                    _viewModel.ModelType.CodeType,
                    _viewModel.DbContextModelType.TypeName,
                    _viewModel.UseMasterPage,
                    _viewModel.DesktopMasterPage ?? String.Empty,
                    _viewModel.DesktopPlaceholderId,
                    _viewModel.OverwriteViews
                );
            }
            finally
            {
                Mouse.OverrideCursor = currentCursor;
            }
        }

        private void GenerateCode(CodeType modelType,
                                 string dbContextTypeName,
                                 bool useMasterPage,
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

            // Add folder for views. This is necessary to display an error when the folder already exists but 
            // the folder is excluded in Visual Studio: see https://github.com/Superexpert/WebFormsScaffolding/issues/18
            string outputPath = Path.Combine(GetSelectionRelativePath(), modelType.Name);
            AddFolder(Context.ActiveProject, outputPath);

            // Now add each view
            foreach (string view in views)
            {
                AddWebFormsViewTemplates(modelType, dbContext,
                    efMetadata: efMetadata,
                    actionName: view,
                    useMasterPage: useMasterPage,
                    masterPage: masterPage,
                    sectionNames: sectionNames,
                    primarySectionName: desktopPlaceholderId,
                    overwrite: overwriteViews);
            }

            // Add the Dynamic Data Entity and Field templates
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

            // always force the project to build so we have a compiled
            // model that we can use with the Entity Framework
            var visualStudioUtils = new VisualStudioUtils();
            visualStudioUtils.BuildProject(Context.ActiveProject);


            Type reflectedModelType = GetReflectionType(modelType.FullName);
            if (reflectedModelType == null)
            {
                throw new InvalidOperationException(Resources.WebFormsScaffolder_ProjectNotBuilt);
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
            var entityTemplates = new[] { 
                "Default", "Default.ascx", "Default.ascx.designer", 
                "Default_Edit", "Default_Edit.ascx", "Default_Edit.ascx.designer",
                "Default_Insert", "Default_Insert.ascx", "Default_Insert.ascx.designer"
            };
            var entityTemplatesPath = "DynamicData\\EntityTemplates";
            Project project = Context.ActiveProject;

            // Add the folder
            AddFolder(Context.ActiveProject, entityTemplatesPath);

            foreach (var entityTemplate in entityTemplates)
            {
                var templatePath = Path.Combine(entityTemplatesPath, entityTemplate);
                var outputPath = Path.Combine(entityTemplatesPath, entityTemplate);

                AddFileFromTemplate(
                    project: project,
                    outputPath: outputPath,
                    templateName: templatePath,
                    templateParameters: new Dictionary<string, object>() 
                    {
                        {"DefaultNamespace", project.GetDefaultNamespace()}
                    },
                    skipIfExists: true);
            }
        }



        private void AddDynamicDataFieldTemplates() {
            var fieldTemplates = new[] { 
                "Boolean", "Boolean.ascx.designer", "Boolean.ascx",
                "Boolean_Edit", "Boolean_Edit.ascx.designer", "Boolean_Edit.ascx",
                "Children", "Children.ascx.designer", "Children.ascx",
                "Children_Insert", "Children_Insert.ascx.designer", "Children_Insert.ascx",
                "DateTime", "DateTime.ascx.designer", "DateTime.ascx",
                "DateTime_Edit", "DateTime_Edit.ascx.designer", "DateTime_Edit.ascx",
                "Decimal_Edit", "Decimal_Edit.ascx.designer", "Decimal_Edit.ascx",
                "EmailAddress", "EmailAddress.ascx.designer", "EmailAddress.ascx",
                "Enumeration", "Enumeration.ascx.designer", "Enumeration.ascx",
                "Enumeration_Edit", "Enumeration_Edit.ascx.designer", "Enumeration_Edit.ascx",
                "ForeignKey", "ForeignKey.ascx.designer", "ForeignKey.ascx",
                "ForeignKey_Edit", "ForeignKey_Edit.ascx.designer", "ForeignKey_Edit.ascx",
                "Integer_Edit", "Integer_Edit.ascx.designer", "Integer_Edit.ascx",
                "MultilineText_Edit", "MultilineText_Edit.ascx.designer", "MultilineText_Edit.ascx",
                "Text", "Text.ascx.designer", "Text.ascx",
                "Text_Edit", "Text_Edit.ascx.designer", "Text_Edit.ascx",
                "Url", "Url.ascx.designer", "Url.ascx",
                "Url_Edit", "Url_Edit.ascx.designer", "Url_Edit.ascx"
            };
            var fieldTemplatesPath = "DynamicData\\FieldTemplates";                
            Project project = Context.ActiveProject;


            // Add the folder
            AddFolder(Context.ActiveProject, fieldTemplatesPath);

            foreach (var fieldTemplate in fieldTemplates) {
                var templatePath = Path.Combine(fieldTemplatesPath, fieldTemplate);
                var outputPath = Path.Combine(fieldTemplatesPath, fieldTemplate);

                AddFileFromTemplate(
                    project:project,
                    outputPath:outputPath,
                    templateName: templatePath,
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
                                bool useMasterPage,
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

            string outputPath = Path.Combine(GetSelectionRelativePath(), modelType.Name, actionName);
            string modelNameSpace = modelType.Namespace != null ? modelType.Namespace.FullName : String.Empty;
            string dbContextNameSpace = dbContext.Namespace != null ? dbContext.Namespace.FullName : String.Empty;

            List<string> actionTemplates = new List<string>();
            actionTemplates.AddRange(new string[] { actionName, actionName + ".aspx" });

            // Scaffold aspx page and code behind
            foreach (string action in actionTemplates)
            {
                Project project = Context.ActiveProject;
                var templatePath = Path.Combine("WebForms", action);
                var defaultNamespace = GetDefaultNamespace() + "." + modelType.Name;
                AddFileFromTemplate(project,
                    outputPath,
                    templateName: templatePath,
                    templateParameters: new Dictionary<string, object>() 
                    {
                        {"DefaultNamespace", defaultNamespace},
                        {"Namespace", modelNameSpace},
                        {"IsContentPage", useMasterPage},
                        {"MasterPageFile", masterPage},
                        {"SectionNames", sectionNames},
                        {"PrimarySectionName", primarySectionName},
                        {"PrimaryKeyMetadata", primaryKey},
                        {"PrimaryKeyName", primaryKey.PropertyName},
                        {"PrimaryKeyType", primaryKey.ShortTypeName},
                        {"ViewDataType", modelType},
                        {"ViewDataTypeName", modelType.Name},
                        {"DBContextType", dbContext.Name},
                        {"DBContextNamespace", dbContextNameSpace},
                        {"PluralizedName", pluralizedName}
                    },
                    skipIfExists: !overwrite);
            }

        }


        // Returns the relative path of the folder selected in Visual Studio or an empty 
        // string if no folder is selected.
        protected string GetSelectionRelativePath()
        {
            return Context.ActiveProjectItem == null ? String.Empty : ProjectItemUtils.GetProjectRelativePath(Context.ActiveProjectItem);
        }


        // If a Visual Studio folder is selected then returns the folder's namespace, otherwise
        // returns the project namespace.
        protected string GetDefaultNamespace()
        {
            return Context.ActiveProjectItem == null 
                ? Context.ActiveProject.GetDefaultNamespace() 
                : Context.ActiveProjectItem.GetDefaultNamespace();
        }

    }
}
