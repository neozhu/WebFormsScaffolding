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


namespace Microsoft.AspNet.Scaffolding.WebForms.Scaffolders
{
    public class WebFormsScaffolder : CodeGenerator
    {
        //Should this change according to VS version? Tracked by 716268.
        private static readonly string WebToolsNugetPackagesRegistryKeyNamePrefix = "WebFormsVS";

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
                WebFormsViewScaffolderFactory factory = new WebFormsViewScaffolderFactory();
                WebFormsViewScaffolder viewCodeGenerator = (WebFormsViewScaffolder)factory.CreateInstance(Context);

                viewCodeGenerator.GenerateCode(modelType, dbContext,
                    efMetadata: efMetadata,
                    actionName: view,
                    masterPage: masterPage,
                    sectionNames: sectionNames,
                    primarySectionName: desktopPlaceholderId,
                    overwrite: overwriteViews);
            }

            AddNuGetDependenciesForGeneratedCode();
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

        private void AddNuGetDependenciesForGeneratedCode()
        {
            if (_viewModel == null)
            {
                throw new InvalidOperationException(Resources.WebFormsScaffolder_ShowUIAndValidateNotCalled);
            }
            
            Version vsVersion = Context.ServiceProvider.GetService<IVisualStudioInformation>().Version;
            string nugetRepositoryRegistryKey = WebToolsNugetPackagesRegistryKeyNamePrefix + vsVersion.Major;

            NuGetRegistryRepository repository = new NuGetRegistryRepository(nugetRepositoryRegistryKey, isPreUnzipped: true);

            NuGetPackage dynamicDataTemplateInstallData = new NuGetPackage("Microsoft.AspNet.DynamicDataTemplates.CS", "1.0.0-beta1", repository);
            Context.Packages.Add(dynamicDataTemplateInstallData);

        }
    }
}
