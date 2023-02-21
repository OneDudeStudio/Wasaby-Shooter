using System;
using System.Threading;
using Railways;
using Railways.GeneratorsAndDestroyers;
using Railways.Trains;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class Test : MonoBehaviour
{
    public DynamicGeneratorByController DynamicGeneratorByController;
    public Destroyer Destroyer;

    private ControlledTrain _controlledTrain;
    
    private void Start()
    {
        _controlledTrain = DynamicGeneratorByController.Generate().GetComponent<ControlledTrain>();
        Destroyer.OnDestroy += GenerateInstance;
        _controlledTrain.Arrive += Process;
    }

    public void GenerateInstance()
    {
        _controlledTrain = DynamicGeneratorByController.Generate().GetComponent<ControlledTrain>();
    }

    public void Process()
    {
        
        _controlledTrain.StopMove();
        _controlledTrain.OpenDoors();
        Debug.Log("process2");
        /*_controlledTrain.CloseDoors();
        Debug.Log("process3");*/
        //_controlledTrain.StartMove();
        
    }
    
}
