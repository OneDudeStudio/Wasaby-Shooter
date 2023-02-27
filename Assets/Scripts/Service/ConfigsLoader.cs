using UnityEngine;

public class ConfigsLoader : MonoBehaviour
{
    private RootConfig _rootconfig;

    private void Awake()
    {
        _rootconfig = Resources.Load<RootConfig>("RootConfig");
    }

    public RootConfig RootConfig => _rootconfig; 
}
