using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TournamentManager : MonoBehaviour
{
    //public CarControllerP2[] cars;
    public CarControllerP1 car1;
    public CarControllerP2 car2;
    public List<CarControllerAI> listAI;
    public Transform[] paths;
    public CircuitManager[] circuits;
    public  int currentCircuit = 0;
    [SerializeField] GameObject ScoreBoard;
    [SerializeField] GameObject ScoreBoardEnd;
    public LevelManager lm;
    
    
    


    // Start is called before the first frame update
    void Start()
    {
        circuits[0].tournament = this;
        circuits[0].car1 = car1;
        if(car2 != null)
            circuits[0].car2 = car2;
        circuits[0].AIList = listAI;
        circuits[0].gameObject.SetActive(true);
        StartCoroutine(lm.CompteRebours());
        
        foreach (var i in listAI )
        {
            i.path = paths[currentCircuit];
            i.GetPath();
        }

    }

    public void nextCircuit()
    {
        
        circuits[currentCircuit].gameObject.SetActive(false);
        currentCircuit++;
        if (currentCircuit < circuits.Length)
        {
            circuits[currentCircuit].car1 = car1;
            if(car2 != null)
                circuits[currentCircuit].car2 = car2;
            circuits[currentCircuit].tournament = this;
            circuits[currentCircuit].gameObject.SetActive(true);
            circuits[currentCircuit].AIList = listAI;
            
            foreach (var i in listAI )
            {
                i.path = paths[currentCircuit];
                i.GetPath();
            }
            
            StartCoroutine(lm.CompteRebours());
        }
        else
        {
            //écran victoire
            ScoreBoard.SetActive(false);
            ScoreBoardEnd.SetActive(true);
            Debug.Log("c gagné");
        }
    }
}
