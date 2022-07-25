using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    private CircuitManager circuit;
    public int checkpointNumber;
    // Start is called before the first frame update
    void Start()
    {
        circuit = GetComponentInParent<CircuitManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var car1 = other.GetComponentInParent<CarControllerP1>();
        if (car1 != null)
        {
            circuit.checkpointPassed1(car1, checkpointNumber);
            return;
        }
        var car2 = other.GetComponentInParent<CarControllerP2>();
        if (car2 != null)
        {
            circuit.checkpointPassed2(car2, checkpointNumber);
            return;
        }
        var carAI = other.GetComponentInParent<CarControllerAI>();
        if (carAI != null)
        {
            
            circuit.checkpointPassedAI(carAI, checkpointNumber);
            return;
        }
    }
}
