using UnityEngine;

public class Elevator : MonoBehaviour
{
    private bool _insideElevator;

    private void OnTriggerEnter(Collider other)
    {
        _insideElevator = true;
        print("inside");
        SceneTransition.SwitchToScene("LubaScene2");
    }

    private void OnTriggerExit(Collider other)
    {
        _insideElevator = false;
        print("leaving");
    }
}