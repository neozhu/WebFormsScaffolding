
Follow these steps to build and run:

1. You must install the Visual Studio SDK to use a VSIX project:
http://www.microsoft.com/en-us/download/details.aspx?id=40758

2. Right-click WebFormsScaffolding.vsix and select Set as StartUp Project

3. Open Project Properties for WebFormsScaffolding.vsix and select the Debug tab.

4. In the Start Action section, enter the path to Visual Studio for Start external program. For example:
C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\devenv.exe

5. In the Start Options section, enter /rootsuffix Exp for Command line arguments.

Running the Solution will open an experimental instance of Visual Studio with the
Web Forms Scaffolding VSIX installed.


