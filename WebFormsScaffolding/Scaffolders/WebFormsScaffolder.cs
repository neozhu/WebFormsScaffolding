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
using EnvDTE80;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.ComponentModel;


namespace Microsoft.AspNet.Scaffolding.WebForms.Scaffolders
{
    // This class performs all of the work of scaffolding. The methods are executed in the
    // following order:
    // 1) ShowUIAndValidate() - displays the Visual Studio dialog for setting scaffolding options
    // 2) Validate() - validates the model collected from the dialog
    // 3) GenerateCode() - if all goes well, generates the scaffolding output from the templates
    public class WebFormsScaffolder : CodeGenerator
    {
       
        private WebFormsCodeGeneratorViewModel _codeGeneratorViewModel;

        internal WebFormsScaffolder(CodeGenerationContext context, CodeGeneratorInformation information)
            : base(context, information)
        {

        }


        // Shows the Visual Studio dialog that collects scaffolding options
        // from the user.
        // Passing the dialog to this method so that all scaffolder UIs
        // are modal is still an open question and tracked by bug 578173.
        public override bool ShowUIAndValidate()
        {
            _codeGeneratorViewModel = new WebFormsCodeGeneratorViewModel(Context);

            WebFormsScaffolderDialog window = new WebFormsScaffolderDialog(_codeGeneratorViewModel);
            bool? isOk = window.ShowModal();

            if (isOk == true)
            {
                Validate();
            }

            return (isOk == true);
        }

        // Validates the model returned by the Visual Studio dialog.
        // We always force a Visual Studio build so we have a model
        // that we can use with the Entity Framework.
        private void Validate()
        {
            CodeType modelType = _codeGeneratorViewModel.ModelType.CodeType;
            ModelType dbContextType = _codeGeneratorViewModel.DbContextModelType;
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

        // Top-level method that generates all of the scaffolding output from the templates.
        // Shows a busy wait mouse cursor while working.
        public override void GenerateCode()
        {
            var project = Context.ActiveProject;
            var selectionRelativePath = GetSelectionRelativePath();

            if (_codeGeneratorViewModel == null)
            {
                throw new InvalidOperationException(Resources.WebFormsScaffolder_ShowUIAndValidateNotCalled);
            }

            Cursor currentCursor = Mouse.OverrideCursor;
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                GenerateCode(project, selectionRelativePath, this._codeGeneratorViewModel);
            }
            finally
            {
                Mouse.OverrideCursor = currentCursor;
            }
        }

        // Collects the common data needed by all of the scaffolded output and generates:
        // 1) Dynamic Data Field Templates
        // 2) Web Forms Pages
        private void GenerateCode(Project project, string selectionRelativePath, WebFormsCodeGeneratorViewModel codeGeneratorViewModel)
        {
            // Get Model Type
            var modelType = codeGeneratorViewModel.ModelType.CodeType;
 
            // Ensure the Data Context
            string dbContextTypeName = codeGeneratorViewModel.DbContextModelType.TypeName;
            IEntityFrameworkService efService = Context.ServiceProvider.GetService<IEntityFrameworkService>();
            ModelMetadata efMetadata = efService.AddRequiredEntity(Context, dbContextTypeName, modelType.FullName);


            // Get the dbContext
            ICodeTypeService codeTypeService = GetService<ICodeTypeService>();
            CodeType dbContext = codeTypeService.GetCodeType(project, dbContextTypeName);

            // Get the dbContext namespace
            string dbContextNamespace = dbContext.Namespace != null ? dbContext.Namespace.FullName : String.Empty;

            // Ensure the Dynamic Data Field templates
            EnsureDynamicDataFieldTemplates(project, dbContextNamespace, dbContextTypeName);

            // Add Web Forms Pages from Templates
            AddWebFormsPages(
                project, 
                selectionRelativePath,
                dbContextNamespace,
                dbContextTypeName,
                modelType, 
                efMetadata, 
                codeGeneratorViewModel.UseMasterPage, 
                codeGeneratorViewModel.DesktopMasterPage, 
                codeGeneratorViewModel.DesktopPlaceholderId, 
                codeGeneratorViewModel.OverwriteViews
           );
        }


        // A set of Dynamic Data field templates is created that support Bootstrap
        private void EnsureDynamicDataFieldTemplates(Project project, string dbContextNamespace, string dbContextTypeName)
        {
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

            // Add the folder
            AddFolder(project, fieldTemplatesPath);

            foreach (var fieldTemplate in fieldTemplates)
            {
                var templatePath = Path.Combine(fieldTemplatesPath, fieldTemplate);
                var outputPath = Path.Combine(fieldTemplatesPath, fieldTemplate);

                AddFileFromTemplate(
                    project: project,
                    outputPath: outputPath,
                    templateName: templatePath,
                    templateParameters: new Dictionary<string, object>() 
                    {
                        {"DefaultNamespace", project.GetDefaultNamespace()},
                        {"DbContextNamespace", dbContextNamespace},
                        {"DbContextTypeName", dbContextTypeName}
                    },
                    skipIfExists: true);
            }
        }


        // Generates all of the Web Forms Pages (Default Insert, Edit, Delete), 
        private void AddWebFormsPages(
            Project project, 
            string selectionRelativePath,
            string dbContextNamespace,
            string dbContextTypeName,
            CodeType modelType,
            ModelMetadata efMetadata,
            bool useMasterPage,
            string masterPage = null,
            string desktopPlaceholderId = null,
            bool overwriteViews = true
        )
        {

            if (modelType == null)
            {
                throw new ArgumentNullException("modelType");
            }

            // Get pluralized name used for web forms folder name
            string pluralizedModelName = efMetadata.EntitySetName;

            // Generate dictionary for related entities
            var relatedModels = GetRelatedModelDictionary(efMetadata);


            var webForms = new[] { "Default", "Insert", "Edit", "Delete", "Details" };

            // Extract these from the selected master page : Tracked by 721707
            var sectionNames = new[] { "HeadContent", "MainContent" };

            // Add folder for views. This is necessary to display an error when the folder already exists but 
            // the folder is excluded in Visual Studio: see https://github.com/Superexpert/WebFormsScaffolding/issues/18
            string outputFolderPath = Path.Combine(selectionRelativePath, pluralizedModelName);
            AddFolder(Context.ActiveProject, outputFolderPath);


            // Now add each view
            foreach (string webForm in webForms)
            {
                AddWebFormsViewTemplates(
                    outputFolderPath: outputFolderPath,
                    pluralizedModelName: pluralizedModelName,
                    modelType: modelType,
                    efMetadata: efMetadata,
                    relatedModels: relatedModels,
                    dbContextNamespace: dbContextNamespace,
                    dbContextTypeName: dbContextTypeName,
                    webFormsName: webForm,
                    useMasterPage: useMasterPage,
                    masterPage: masterPage,
                    sectionNames: sectionNames,
                    primarySectionName: desktopPlaceholderId,
                    overwrite: overwriteViews);
            }
        }



        private void AddWebFormsViewTemplates(
                                string outputFolderPath,
                                string pluralizedModelName,
                                CodeType modelType,
                                ModelMetadata efMetadata,
                                IDictionary<string, RelatedModelMetadata> relatedModels,
                                string dbContextNamespace,
                                string dbContextTypeName,
                                string webFormsName,
                                bool useMasterPage,
                                string masterPage = "",
                                string[] sectionNames = null,
                                string primarySectionName = "",
                                bool overwrite = false
        )
        {
            if (modelType == null)
            {
                throw new ArgumentNullException("modelType");
            }
            if (String.IsNullOrEmpty(webFormsName))
            {
                throw new ArgumentException(Resources.WebFormsViewScaffolder_EmptyActionName, "webFormsName");
            }

            // Generate unique code beside name (needed because VB does not namespace)
            var codeBesideName = GetUniqueCodeBesideName(Context.ActiveProject, webFormsName);

            PropertyMetadata primaryKey = efMetadata.PrimaryKeys.FirstOrDefault();

            string modelNameSpace = modelType.Namespace != null ? modelType.Namespace.FullName : String.Empty;
            string relativePath = outputFolderPath.Replace(@"\", @"/");

            var modelDisplayNames = GetDisplayNames(modelType);

            List<string> webFormsTemplates = new List<string>();
            webFormsTemplates.AddRange(new string[] { webFormsName, webFormsName + ".aspx", webFormsName + ".aspx.designer" });

            // Scaffold aspx page and code behind
            foreach (string webForm in webFormsTemplates)
            {
                Project project = Context.ActiveProject;
                var templatePath = Path.Combine("WebForms", webForm);
                string outputPath = Path.Combine(outputFolderPath, webForm);

                var defaultNamespace = Context.ActiveProject.GetDefaultNamespace();
                var folderNamespace = GetDefaultNamespace() + "." + pluralizedModelName;



                AddFileFromTemplate(project,
                    outputPath,
                    templateName: templatePath,
                    templateParameters: new Dictionary<string, object>() 
                    {
                        {"IsContentPage", useMasterPage}, // does this page have a master page?
                        {"MasterPageFile", masterPage ?? String.Empty}, // master page associated with this page
                        {"PrimarySectionName", primarySectionName}, // the main content section of a master page
                        
                        {"ModelName", modelType.Name}, // singular model name (e.g., Movie)
                        {"FullModelName", modelType.FullName}, // singular model name with namespace (e.g., Samples.Movie)
                        {"PluralizedModelName", pluralizedModelName}, // the plural model name (e.g. Movies)
                        {"ModelMetadata", efMetadata}, // the EF meta date for the model
                        {"RelatedModels", relatedModels}, // models related by association to the model

                        {"DefaultNamespace", defaultNamespace}, // the default namespace of the project (used by VB)
                        {"FolderNamespace", folderNamespace}, // the namespace of the current folder (used by C#)
                        {"ModelNamespace", modelNameSpace}, // the namespace of the model (e.g., Samples.Models)                        
                        {"CodeBesideName", codeBesideName}, // the Web Forms code beside class name (e.g., _Default)
                        {"PrimaryKeyName", primaryKey.PropertyName}, // primary key of model (e.g., Id)
                        {"PrimaryKeyType", primaryKey.ShortTypeName}, // short primary key type name (e.g., string)

                        {"RelativePath", relativePath}, // relative path of current page (e.g., /samples/movie)

                        {"DbContextNamespace", dbContextNamespace},
                        {"DbContextTypeName", dbContextTypeName},
                        {"ModelDisplayNames", modelDisplayNames}
                    },
                    skipIfExists: !overwrite);
            }

        }

        // VB, unlike C#, does not namespace by folder. For this reason, we must generate a unique
        // code-beside name for each VB Web Form class. For example, there cannot be more than one
        // Insert class in the same VB project. Instead, we need Insert, Insert1, Insert2, ...
        private string GetUniqueCodeBesideName(Project project, string originalName)
        {
            // In VB, rename Default to _Default (because Default is a keyword)
            if (originalName == "Default" && ProjectLanguage.VisualBasic.Equals(Context.ActiveProject.GetCodeLanguage()))
            {
                originalName = "_Default";
            }

            var counter = 0;
            var currentName = originalName;

            ICodeTypeService codeTypeService = GetService<ICodeTypeService>();
            while (codeTypeService.GetAllCodeTypes(project).Any(c => c.Name == currentName))
            {
                counter++;
                currentName = originalName + counter.ToString();
            }
            return currentName;
        }



        // Called to ensure that the project was compiled successfully
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


        // Create a dictionary that maps foreign keys to related models. We only care about associations
        // with a single key (so we can display in a DropDownList)
        protected IDictionary<string, RelatedModelMetadata> GetRelatedModelDictionary(ModelMetadata efMetadata)
        {
            var dict = new Dictionary<string, RelatedModelMetadata>();

            foreach (var relatedEntity in efMetadata.RelatedEntities)
            {
                if (relatedEntity.ForeignKeyPropertyNames.Count() == 1)
                {
                    dict[relatedEntity.ForeignKeyPropertyNames[0]] = relatedEntity;
                }
            }
            return dict;
        }


        // Create a mapping between property names and display names in case
        // the property is decorated with a DisplayAttribute
        protected IDictionary<string, string> GetDisplayNames(CodeType modelType)
        {
            var type = GetReflectionType(modelType.FullName);
            var lookup = new Dictionary<string, string>();
            foreach (PropertyInfo prop in type.GetProperties()) {
                var attr = (DisplayAttribute)prop.GetCustomAttribute(typeof( DisplayAttribute), true);
                var value = attr != null && !String.IsNullOrWhiteSpace(attr.Name) ? attr.Name : prop.Name;
                lookup.Add(prop.Name, value);
            }
            return lookup;
        }


    }
}
