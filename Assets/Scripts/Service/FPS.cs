using UnityEngine;

public class FPS : MonoBehaviour
{
    private static float fps;

    private void OnGUI()
    {
        fps = 1.0f / Time.deltaTime;
        float normalizedFps = (0 < fps && fps < 1000) ? fps : 0;
        GUILayout.Label("FPS: " + (int)normalizedFps);
    }
}