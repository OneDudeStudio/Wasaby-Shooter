using System;
using System.Collections.Generic;

public class ModuleManager
{
    private Dictionary<Type, GunModule> _modules;
    private GunModule _currentModule;

    public ModuleManager(Gun gun)
    {
        _modules = new Dictionary<Type, GunModule>()
        {
            { typeof(NullModule), new NullModule(gun)},
            { typeof(BarrelModule), new BarrelModule(gun)},
            { typeof(ExtendedMag), new ExtendedMag(gun)},
            { typeof(ShutterModule), new ShutterModule(gun)}
        };

        _currentModule = null;
    }

    public void SetModule(Type moduleType)
    {
        if (_modules.ContainsKey(moduleType))
        {
            ResetModule();

            _currentModule = _modules[moduleType];
            _currentModule.ApplyModifiers();
        }
    }

    public void ResetModule()
    {
        if (_currentModule != null)
        {
            _currentModule.ResetModifiers();
        }
    }

}