using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public CarControllerP1 car1 = null;
    public CarControllerP2 car2 = null;
    public CircuitManager circuit;
    //public int numObstacle;

    void Update()
    {
        Ray ray = circuit.topDownCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit)) {
            if (hit.rigidbody != null)
            { 
                Vector3 pos = hit.point;
                pos.y = transform.position.y;
                transform.position = pos;
            }
        }
        
        if(Input.GetMouseButtonDown(0))
        {
            if (car1 != null)
            {
                if (car1.objectPlace) return;
                car1.objectPlace = true;
                this.enabled = false;
            }
            else
            {
                if (car2.objectPlace) return;
                car2.objectPlace = true;
                this.enabled = false;
            }
        }
        
        if (Input.GetKey(KeyCode.R))
        {
            transform.Rotate(new Vector3(0f,1f,0f), .1f);
        }
    }
}
