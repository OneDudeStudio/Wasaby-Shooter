using Railways.GeneratorsAndDestroyers;
using Railways.Trains;
using UnityEngine;

public class Test : MonoBehaviour
{
    public DynamicGeneratorByController DynamicGeneratorByController;
    public Destroyer Destroyer;

    private ControlledTrain _controlledTrain;

    private void Start()
    {
        GenerateInstance();
        Destroyer.OnDestroy += GenerateInstance;
        _controlledTrain.OnArrive += StartProcess;
        // нужно подписаться на событие что все враги вышли
        // enemySpawner.AllEnemiesLeaveTrain = += FinishProcess;
    }

    public void GenerateInstance()
    {
        _controlledTrain = DynamicGeneratorByController.Generate().GetComponent<ControlledTrain>();
    }

    public void StartProcess()
    {
        _controlledTrain.StopMove();
        _controlledTrain.OpenDoors();
    }

    public void FinishProcess()
    {
        _controlledTrain.CloseDoors();
        _controlledTrain.StartMove();
    }
}