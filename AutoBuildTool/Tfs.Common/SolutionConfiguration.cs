using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tfs.Common
{
    public class SolutionConfiguration
    {
        static readonly Type s_ConfigInSolution;
        static readonly PropertyInfo configInSolution_configurationname;
        static readonly PropertyInfo configInSolution_fullName;
        static readonly PropertyInfo configInSolution_platformName;

        static SolutionConfiguration()
        {
            s_ConfigInSolution = Type.GetType("Microsoft.Build.Construction.ConfigurationInSolution, Microsoft.Build, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", false, false);
            if (s_ConfigInSolution != null)
            {
                configInSolution_configurationname = s_ConfigInSolution.GetProperty("ConfigurationName", BindingFlags.NonPublic | BindingFlags.Instance);
                configInSolution_fullName = s_ConfigInSolution.GetProperty("FullName", BindingFlags.NonPublic | BindingFlags.Instance);
                configInSolution_platformName = s_ConfigInSolution.GetProperty("PlatformName", BindingFlags.NonPublic | BindingFlags.Instance);
            }
        }

        public string configurationName { get; private set; }
        public string fullName { get; private set; }
        public string platformName { get; private set; }


        public SolutionConfiguration(object solutionConfiguration)
        {
            this.configurationName = configInSolution_configurationname.GetValue(solutionConfiguration, null) as string;
            this.fullName = configInSolution_fullName.GetValue(solutionConfiguration, null) as string;
            this.platformName = configInSolution_platformName.GetValue(solutionConfiguration, null) as string;
        }
    }
}
