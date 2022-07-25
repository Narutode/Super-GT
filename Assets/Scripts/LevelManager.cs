using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject CompteReboursText;
    public bool StartGame;
    public bool EndGame;
    public Text CompteReboursTime;
    public float TimeGame;
    public TimeShow ts1;
    public TimeShow ts2;
    public List<Rigidbody> Listrb;

    public CarControllerP2 p2;
    // Start is called before the first frame update

    void Start()
    {
        
    }
    

    public IEnumerator CompteRebours()
    {
        TimeGame = 0;
        CloseControllerCar();
        CompteReboursText.SetActive(true);
        yield return new WaitForEndOfFrame();
        CompteReboursTime.text = "4";
        yield return new WaitForSeconds(1f);
        CompteReboursTime.text = "3";
        yield return new WaitForSeconds(1f);
        CompteReboursTime.text = "2";
        yield return new WaitForSeconds(1f);
        CompteReboursTime.text = "1";
        yield return new WaitForSeconds(1f);
        CompteReboursTime.text = "Start";
        yield return new WaitForSeconds(1f);
        CompteReboursText.SetActive(false);
        StartGame = true;
        CanStartControllerCar();
        ts1.StartGameAction();
        if(ts2 != null)
            ts2.StartGameAction();

    }
    // Update is called once per frame
    void Update()
    {
        if (StartGame)
        {
            TimeGame += Time.deltaTime;
        }
    }

    public void CanStartControllerCar()
    {
        foreach (var car in Listrb)
        {
            car.isKinematic = false;
        }
    }
    public void CloseControllerCar()
    {
        foreach (var car in Listrb)
        {
            car.isKinematic = true;
        }
    }
    
}

