using System;
using System.Configuration;

namespace Deldysoft.Foundation
{
    public static class AppEnvironment
    {
        static AppEnvironment()
        {
            var environment = ConfigurationManager.AppSettings["environment"];
            
            if (environment.Equals("live", StringComparison.CurrentCultureIgnoreCase)) {
                Current = EnvironmentType.Live;
            }
            else {
                Current = EnvironmentType.Development;
            } 
        }

        public static EnvironmentType Current { get; private set; }
    }
}