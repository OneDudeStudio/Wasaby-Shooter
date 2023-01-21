using System;
using System.Collections.Generic;

public class ModuleManager
{
    private Dictionary<Type, GunModule> _modules;
    private GunModule _currentModule;

    public ModuleManager(Gun gun)
    {
        _currentModule = new NullModule(gun);
        _modules = new Dictionary<Type, GunModule>()
        {
            { typeof(NullModule), _currentModule},
            { typeof(BarrelModule), new BarrelModule(gun)},
            { typeof(ExtendedMag), new ExtendedMag(gun)}
        };
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
        _currentModule.ResetModifiers();
    }

}