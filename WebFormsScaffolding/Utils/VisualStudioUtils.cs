using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Scaffolding.WebForms.Utils
{
    internal class VisualStudioUtils
    {

        private DTE2 _dte;

        internal VisualStudioUtils()
        {
            // initialize DTE object -- the top level object for working with Visual Studio
            this._dte = Package.GetGlobalService(typeof(DTE)) as DTE2;
        }

        internal void BuildCurrentProject()
        {
            var solutionConfiguration = _dte.Solution.SolutionBuild.ActiveConfiguration.Name;
            var activeProject = GetActiveProject();
            if (activeProject == null)
            {
                throw new NullReferenceException("active project");
            }

            _dte.Solution.SolutionBuild.BuildProject(solutionConfiguration, activeProject.FullName, true);
        }

        internal Project GetActiveProject()
        {
            Project activeProject = null;
            Array activeSolutionProjects = _dte.ActiveSolutionProjects as Array;
            if (activeSolutionProjects != null && activeSolutionProjects.Length > 0)
            {
                activeProject = activeSolutionProjects.GetValue(0) as Project;
            }

            return activeProject;
        }


    }
}
