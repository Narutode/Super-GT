using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum RaceState
{
    Qualification,
    ObjectPlacement,
    Race
};
public class CircuitManager : MonoBehaviour
{   
    public int numberFinished;
    public CarControllerP1 car1;
    public CarControllerP2 car2;
    public List<CarControllerAI> AIList;
    private RaceState rs;
    public int totalCheckpointsNumber;
    public int nbLaps;
    public LevelManager lm;
    public TimeShow ts1;
    public TimeShow ts2;
    public Text textLapP1;
    public Text textLapP2;
    public TournamentManager Tm;
    public List<Obstacle> obstacles;
    public int Obstacle1;
    public int Obstacle2;
    
    
    public List<Transform> startPositions;

    public Camera topDownCamera;

    public TournamentManager tournament;
    [SerializeField] GameObject ScoreBoard;

    private bool P2objectPlacing = false;
    // Start is called before the first frame update
    void Start()
    {
        rs = RaceState.Qualification;
        setupCars();

        foreach (var o in obstacles)
        {
            o.enabled = false;
            o.circuit = this;
        }
        textLapP1.text = "Tour de qualification";
        textLapP2.text = "Tour de qualification";
        Obstacle1 = Random.Range(0,obstacles.Count);
        Obstacle2 = Obstacle1;
        while(Obstacle2 == Obstacle1)
            Obstacle2 = Random.Range(0,obstacles.Count);
        setCarLayer("CarNoCollision");
    }

    private void Update()
    {
        if (rs == RaceState.ObjectPlacement)
        {
            if (car2 != null)
            {
                if (car1.objectPlace && !P2objectPlacing)
                {
                    obstacles[Obstacle2].enabled = true;
                    obstacles[Obstacle2].gameObject.SetActive(true);
                    obstacles[Obstacle2].car2 = car2;
                    P2objectPlacing = true;
                }

                if (car2.objectPlace)
                    raceStart();
            }
            else if(car1.objectPlace)
                raceStart();
        }
    }

    public void raceStart()
    {
        for (int i = 0; i < AIList.Count; i++)
        {
            AIList[i].currentNode = 0;
        }
        rs = RaceState.Race;
        numberFinished = 0; 
        setupCars();
    }
    
    public void checkpointPassed1(CarControllerP1 car, int nbCheck)
    {
        switch (rs)
        {
            case RaceState.Qualification:
                if (nbCheck == car.currentCheckpoint + 1)
                {
                    car.currentCheckpoint++;
                } 
                else if (car.currentCheckpoint != 0 && nbCheck == 0 && !car.finished)
                {
                    numberFinished++;
                    car.finished = true;
                    if (car2 != null && car2.placement == numberFinished)
                        car2.placement = car.placement;
                    else
                        foreach (var ai in AIList)
                        {
                            if (ai != null && ai.placement == numberFinished)
                            {
                                ai.placement = car.placement;
                                break;
                            }
                        }
                    car.placement = numberFinished;
                    ts1.EndGameAction();
                    
                    car.GetComponent<Rigidbody>().isKinematic = true;
                    car1.Time = ts1.Timeshow.text;
                    if (Tm.currentCircuit == 0)
                    {
                        car1.Circuit1Time = ts1.Timeshow.text;
                        car1.Circuit1Placement = numberFinished.ToString();
                    }
                    else if (Tm.currentCircuit == 1)
                    {
                        car1.Circuit2Time = ts1.Timeshow.text;
                        car1.Circuit2Placement = numberFinished.ToString();
                    }
                    else if (Tm.currentCircuit == 2)
                    {
                        car1.Circuit3Time = ts1.Timeshow.text;
                        car1.Circuit3Placement = numberFinished.ToString();
                    }
                    if (checkIfAllCarsHaveFinished())
                    {
                        stopAI();
                        objectPlacementPhaseStart();
                    }
                }
                break;
            case RaceState.Race:
                textLapP1.text = "Tour : " + car1.currentLap.ToString() + "/" + nbLaps.ToString();
                if (nbCheck == car.currentCheckpoint + 1)
                {
                    car.currentCheckpoint++;
                }
                else if(car.currentCheckpoint != 0 && nbCheck == 0)
                {
                    if (car.currentLap < nbLaps)
                    {
                        car.currentLap++;
                        textLapP1.text = "Tour : " + car1.currentLap.ToString() + "/" + nbLaps.ToString();
                        car.currentCheckpoint = 0;
                    }
                    else
                    {
                        if (!car.finished)
                        {
                            numberFinished++;
                            car.finished = true;
                            if (car2 != null && car2.placement == numberFinished)
                                car2.placement = car.placement;
                            else
                                foreach (var ai in AIList)
                                {
                                    if (ai != null && ai.placement == numberFinished)
                                    {
                                        ai.placement = car.placement;
                                        break;
                                    }
                                }
                            car.placement = numberFinished;
                            switch (numberFinished)
                            {
                                case 1: car.points += 10;
                                    break;
                                case 2: car.points += 8;
                                    break;
                                case 3: car.points += 6;
                                    break;
                                case 4: car.points += 4;
                                    break;
                                case 5: car.points += 2;
                                    break;
                                case 6: car.points += 0;
                                    break;
                            }
                            ts1.EndGameAction();
                            car1.Time = ts1.Timeshow.text;
                            if (Tm.currentCircuit == 0)
                            {
                                car1.Circuit1Time = ts1.Timeshow.text;
                                car1.Circuit1Placement = numberFinished.ToString();
                            }
                            else if (Tm.currentCircuit == 1)
                            {
                                car1.Circuit2Time = ts1.Timeshow.text;
                                car1.Circuit2Placement = numberFinished.ToString();
                            }
                            else if (Tm.currentCircuit == 2)
                            {
                                car1.Circuit3Time = ts1.Timeshow.text;
                                car1.Circuit3Placement = numberFinished.ToString();
                            }
                            car.GetComponent<Rigidbody>().isKinematic = true;
                            car.gameObject.layer = LayerMask.NameToLayer("CarNoCollision");
                            foreach (Transform child in car.gameObject.transform)
                            {
                                child.gameObject.layer = LayerMask.NameToLayer("CarNoCollision");
                            }
                            if (checkIfAllCarsHaveFinished())
                            {
                                stopAI();
                                setAIpoints();
                                setAITime();
                                ScoreBoard.SetActive(true);
                                Time.timeScale = 0f;
                            }
                        }
                    }
                }
                break;
        }
    }
    
    public void checkpointPassed2(CarControllerP2 car, int nbCheck)
    {
        switch (rs)
        {
            case RaceState.Qualification:
                if (nbCheck == car.currentCheckpoint + 1)
                {
                    car.currentCheckpoint++;
                } 
                else if (car.currentCheckpoint != 0 && nbCheck == 0 && !car.finished)
                {
                    numberFinished++;
                    car.finished = true;
                    if (car2 != null && car2.placement == numberFinished)
                        car2.placement = car.placement;
                    else
                        foreach (var ai in AIList)
                        {
                            if (ai != null && ai.placement == numberFinished)
                            {
                                ai.placement = car.placement;
                                break;
                            }
                        }
                    car.placement = numberFinished;
                    ts2.EndGameAction();
                    car2.Time = ts2.Timeshow.text;
                    if (Tm.currentCircuit == 0)
                    {
                        car2.Circuit1Time = ts2.Timeshow.text;
                    }
                    else if (Tm.currentCircuit == 1)
                    {
                        car2.Circuit2Time = ts2.Timeshow.text;
                    }
                    else if (Tm.currentCircuit == 2)
                    {
                        car2.Circuit3Time = ts2.Timeshow.text;
                    }
                    car.GetComponent<Rigidbody>().isKinematic = true;
                    if (checkIfAllCarsHaveFinished())
                    {
                        stopAI();
                        objectPlacementPhaseStart();
                    }
                }
                break;
            case RaceState.Race:
                textLapP2.text = "Tour : " + car2.currentLap.ToString() + "/" + nbLaps.ToString();
                if (nbCheck == car.currentCheckpoint + 1)
                {
                    car.currentCheckpoint++;
                }
                else if(car.currentCheckpoint != 0 && nbCheck == 0)
                {
                    if (car.currentLap < nbLaps)
                    {
                        car.currentLap++;
                        textLapP2.text = "Tour : " + car2.currentLap.ToString() + "/" + nbLaps.ToString();
                        car.currentCheckpoint = 0;
                    }
                    else
                    {
                        if (!car.finished)
                        {
                            numberFinished++;
                            car.finished = true;
                            if (car2 != null && car2.placement == numberFinished)
                                car2.placement = car.placement;
                            else
                                foreach (var ai in AIList)
                                {
                                    if (ai != null && ai.placement == numberFinished)
                                    {
                                        ai.placement = car.placement;
                                        break;
                                    }
                                }
                            car.placement = numberFinished;
                            switch (numberFinished)
                            {
                                case 1: car.points += 10;
                                    break;
                                case 2: car.points += 8;
                                    break;
                                case 3: car.points += 6;
                                    break;
                                case 4: car.points += 4;
                                    break;
                                case 5: car.points += 2;
                                    break;
                                case 6: car.points += 0;
                                    break;
                            }
                            ts2.EndGameAction();
                            car2.Time = ts2.Timeshow.text;
                            if (Tm.currentCircuit == 0)
                            {
                                car2.Circuit1Time = ts2.Timeshow.text;
                            }
                            else if (Tm.currentCircuit == 1)
                            {
                                car2.Circuit2Time = ts2.Timeshow.text;
                            }
                            else if (Tm.currentCircuit == 2)
                            {
                                car2.Circuit3Time = ts2.Timeshow.text;
                            }

                            car.GetComponent<Rigidbody>().isKinematic = true;
                            car.gameObject.layer = LayerMask.NameToLayer("CarNoCollision");
                            foreach (Transform child in car.gameObject.transform)
                            {
                                child.gameObject.layer = LayerMask.NameToLayer("CarNoCollision");
                            }
                            if (checkIfAllCarsHaveFinished())
                            {
                                stopAI();
                                setAIpoints();
                                setAITime();
                                ScoreBoard.SetActive(true);
                                Time.timeScale = 0f;
                            }
                        }
                    }
                }
                break;
        }
    }
    
    public void checkpointPassedAI(CarControllerAI car, int nbCheck)
    {
        switch (rs)
        {
            case RaceState.Qualification:
                if (nbCheck == car.currentCheckpoint + 1)
                {
                    car.currentCheckpoint++;
                } 
                else if (car.currentCheckpoint != 0 && nbCheck == 0 && !car.finished)
                {
                    numberFinished++;
                    car.finished = true;
                    if (car1 != null && car1.placement == numberFinished)
                        car1.placement = car.placement;
                    else if (car2 != null && car2.placement == numberFinished)
                        car1.placement = car.placement;
                    else
                        foreach (var ai in AIList)
                        {
                            if (ai != null && ai.placement == numberFinished)
                            {
                                ai.placement = car.placement;
                                break;
                            }
                        }
                    car.placement = numberFinished;
                    //car.GetComponent<TimeShow>().EndGameAction();
                    car.Time = ts1.TimeshowAi;
                    if (Tm.currentCircuit == 0)
                    {
                        car.Circuit1Time = ts1.TimeshowAi;
                    }
                    else if (Tm.currentCircuit == 1)
                    {
                        car.Circuit2Time = ts1.TimeshowAi;
                    }
                    else if (Tm.currentCircuit == 2)
                    {
                        car.Circuit3Time = ts1.TimeshowAi;
                    }
                    car.GetComponentInChildren<Rigidbody>().isKinematic = true;
                    if (checkIfAllCarsHaveFinished())
                    {
                        objectPlacementPhaseStart();
                    }
                }
                break;
            case RaceState.Race:
                if (nbCheck == car.currentCheckpoint + 1)
                {
                    car.currentCheckpoint++;
                }
                else if(car.currentCheckpoint != 0 && nbCheck == 0)
                {
                    if (car.currentLap < nbLaps)
                    {
                        car.currentLap++;
                        textLapP1.text = "Tour : " + car1.currentLap.ToString() + "/" + nbLaps.ToString();
                        car.currentCheckpoint = 0;
                    }
                    else
                    {
                        if (!car.finished)
                        {
                            numberFinished++;
                            car.finished = true;
                            car.placement = numberFinished;

                            switch (numberFinished)
                            {
                                case 1: car.points += 10;
                                    break;
                                case 2: car.points += 8;
                                    break;
                                case 3: car.points += 6;
                                    break;
                                case 4: car.points += 4;
                                    break;
                                case 5: car.points += 2;
                                    break;
                                case 6: car.points += 0;
                                    break;
                            }
                            //car.GetComponentInChildren<TimeShow>().EndGameAction();
                            car.Time = ts1.TimeshowAi;
                            if (Tm.currentCircuit == 0)
                            {
                                car.Circuit1Time = ts1.TimeshowAi;
                            }
                            else if (Tm.currentCircuit == 1)
                            {
                                car.Circuit2Time = ts1.TimeshowAi;
                            }
                            else if (Tm.currentCircuit == 2)
                            {
                                car.Circuit3Time = ts1.TimeshowAi;
                            }
                            car.GetComponent<Rigidbody>().isKinematic = true;
                            car.gameObject.layer = LayerMask.NameToLayer("CarNoCollision");
                            foreach (Transform child in car.gameObject.transform)
                            {
                                child.gameObject.layer = LayerMask.NameToLayer("CarNoCollision");
                            }
                            if (checkIfAllCarsHaveFinished())
                            {
                                ScoreBoard.SetActive(true);
                                Time.timeScale = 0f;
                            }
                        }
                    }
                }
                break;
        }
    }

    void stopAI()
    {
        foreach (var ai in AIList)
        {
            ai.GetComponentInChildren<Rigidbody>().isKinematic = true;
        }
    }

    void setAIpoints()
    {
        foreach (var ai in AIList)
        {
            if (!ai.finished)
            {
                switch (ai.placement)
                {
                    case 1:
                        ai.points += 10;
                        break;
                    case 2:
                        ai.points += 8;
                        break;
                    case 3:
                        ai.points += 6;
                        break;
                    case 4:
                        ai.points += 4;
                        break;
                    case 5:
                        ai.points += 2;
                        break;
                    case 6:
                        ai.points += 0;
                        break;
                }
            }
        }
    }

    void objectPlacementPhaseStart()
    {
        rs = RaceState.ObjectPlacement;
        P2objectPlacing = false;

        car1.mainCamera.enabled = false;
        car1.mainCamera.targetDisplay = 1;
        car1.objectPlace = false;
        car1.currentLap = -1;
        obstacles[Obstacle1].enabled = true;
        obstacles[Obstacle1].gameObject.SetActive(true);
        obstacles[Obstacle1].car1 = car1;

        if (car2 != null)
        {
            car2.mainCamera.enabled = false;
            car2.mainCamera.targetDisplay = 1;
            car2.objectPlace = false;
            car2.currentLap = -1;
        }
        
        topDownCamera.enabled = true;
        topDownCamera.targetDisplay = 0;
    }
    
    

    bool checkIfAllCarsHaveFinished()
    {
        /*
        foreach (var c in AIList)
        {
            if (!c.finished)
                return false;
        }
*/
        if (car2 != null)
            return car1.finished && car2.finished;
        return car1.finished;
    }

    void setAITime()
    {
        foreach (var ai in AIList)
        {
            if (ai.finished == false)
            {
                ai.Time = "- DNF -";
            }
        }
    }

    //au début et lorsque le placement d'objet est terminé
    void setupCars()
    {
        Display.displays[0].Activate();
        numberFinished = 0;
        topDownCamera.enabled = false;

        car1.finished = false;
        car1.currentLap = 0;
        car1.currentCheckpoint = 0;
        car1.transform.position = startPositions[car1.placement-1].position;
        car1.transform.rotation = startPositions[0].rotation;
        car1.GetComponent<Rigidbody>().velocity = Vector3.zero;
        car1.mainCamera.enabled = true;
        car1.mainCamera.targetDisplay = 0;

        if (car2 != null)
        {
            car2.finished = false;
            car2.currentLap = 0;
            car2.currentCheckpoint = 0;
            car2.transform.position = startPositions[car2.placement - 1].position;
            car2.transform.rotation = startPositions[car2.placement - 1].rotation;
            car2.GetComponent<Rigidbody>().velocity = Vector3.zero;
            car2.mainCamera.enabled = true;
            car2.mainCamera.targetDisplay = 0;
        }

        foreach (var carAI in AIList)
        {
            carAI.finished = false;
            carAI.currentLap = 0;
            carAI.currentCheckpoint = 0;
            
            carAI.transform.position = startPositions[carAI.placement-1].position;
            carAI.transform.rotation = startPositions[carAI.placement-1].rotation;
            carAI.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        setCarLayer("Car");
        StartCoroutine(lm.CompteRebours());
    }

    void setCarLayer(String layer)
    {
        car1.gameObject.layer = LayerMask.NameToLayer(layer);
        foreach (Transform child in car1.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer(layer);
        }

        if (car2 != null)
        {
            car2.gameObject.layer = LayerMask.NameToLayer(layer);
            foreach (Transform child in car2.transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer(layer);
            }
        }

        foreach (var carAI in AIList)
        {
            carAI.gameObject.layer = LayerMask.NameToLayer(layer);
            foreach (Transform child in carAI.gameObject.transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer(layer);
            }
        }
    }
}


