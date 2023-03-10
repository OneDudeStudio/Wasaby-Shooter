using UnityEngine;

public class ConfigsLoader : MonoBehaviour
{
    private RootConfig _rootconfig;

    private void Awake()
    {
        _rootconfig = Resources.Load<RootConfig>("RootConfig");
        GlobalEventManager.OnConfigsLoaded.Invoke(this);
    }

    public RootConfig RootConfig => _rootconfig; 
}
