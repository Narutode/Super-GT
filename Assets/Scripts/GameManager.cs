using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static CarControllerP1 ControllerP1;
    public GameObject needle;
    
    private double startPosition = 215f, endPosition = -23f, desiredPosition;

    public double vehicleSpeed;
    public int numVitesse;
    public Text gearIndicator;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        vehicleSpeed =  CarControllerP1.vitesse;
        updateNeedle();

        numVitesse = CarControllerP1.gearIndex;
        updateGears();

    }

    public void updateNeedle()
    {
        desiredPosition = startPosition - endPosition;
        double temp = vehicleSpeed / 220;
        needle.transform.eulerAngles = new Vector3(0, 0, (float) (startPosition - temp * desiredPosition));
    }

    public void updateGears()
    {
        switch (numVitesse)
        {
            case 0:
                gearIndicator.text = "R";
                break;
            case 1:
                gearIndicator.text = "N";
                break;
            case 2:
                gearIndicator.text = "1";
                break;
            case 3:
                gearIndicator.text = "2";
                break;
            case 4:
                gearIndicator.text = "3";
                break;
            case 5:
                gearIndicator.text = "4";  
                break;
            case 6:
                gearIndicator.text = "5";
                break;
        }
    }
}
