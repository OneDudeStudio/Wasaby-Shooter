using UnityEngine;

public class ElevatorDoors : MonoBehaviour
{
    private Animator _componentAnimator;

    void Start()
    {
        _componentAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        print("in doors");
        _componentAnimator.SetTrigger("openingDoors");
    }

    private void OnTriggerExit(Collider other)
    {
        print("out doors");
        _componentAnimator.SetTrigger("closingDoors");
    }
}