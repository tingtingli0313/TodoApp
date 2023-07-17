using System.Reflection;
using TodoList.Core;
using Module = Autofac.Module;

namespace TodoList.Infrastructure;

public class DefaultInfrastructureModule : Module
{
    private readonly bool _isDevelopment = false;
    private readonly List<Assembly> _assemblies = new List<Assembly>();

    public DefaultInfrastructureModule(bool isDevelopment, Assembly? callingAssembly = null)
    {
        _isDevelopment = isDevelopment;
        var coreAssembly =
          Assembly.GetAssembly(typeof(TodoItem)); 
        var infrastructureAssembly = Assembly.GetAssembly(typeof(StartupSetup));
        if (coreAssembly != null)
        {
            _assemblies.Add(coreAssembly);
        }

        if (infrastructureAssembly != null)
        {
            _assemblies.Add(infrastructureAssembly);
        }

        if (callingAssembly != null)
        {
            _assemblies.Add(callingAssembly);
        }
    }
}
