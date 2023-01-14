using System;
using DG.Tweening;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private String _sceneToSwitch;
    [SerializeField] private Transform _elevatorDoors;
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _playerCam;
    

    private void OnTriggerEnter(Collider other)
    {
        print("in elevator");
        
        // disable camera script
        _playerCam.GetComponent<PlayerCam>().enabled = false;
        
        if (_sceneToSwitch != String.Empty)
        {
            
            // do some animations 
            PlayLookAtAnimations();
            PlayElevatorDoorsAnimation();
            
            SceneTransition.SwitchToScene(_sceneToSwitch);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        print("out elevator");
        
        // enable camera script
        _playerCam.GetComponent<PlayerCam>().enabled = true;
    }

    private void PlayLookAtAnimations()
    {
        float cycleLenght = 1;
        _player.DORotate(new Vector3(0, 180, 0), cycleLenght);
    }

    private void PlayElevatorDoorsAnimation()
    {
        _elevatorDoors.GetComponent<Animator>().SetTrigger("closingDoors");
    }
}