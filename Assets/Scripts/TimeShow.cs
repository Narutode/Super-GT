using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeShow : MonoBehaviour
{
    public Text Timeshow;
    public Text Timeshow2;
    public String TimeshowAi;
    public bool Running;
    private DateTime StartTime;
    public CarControllerP2 p2;

    private void Awake()
    {
        //Timeshow = transform.Find("TimeShow").GetComponent<Text>();
        //Timeshow2 = transform.Find("ShowShow").GetComponent<Text>();
    }

    public void StartGameAction()
    {
        Running = true;
        StartTime = DateTime.Now;
        InvokeRepeating(nameof(UpdateTimeShow),0,0.01f);
        InvokeRepeating(nameof(UpdateTimeShowAi),0,0.01f);
    }
    
    public void EndGameAction()
    {
        Running = false;
        CancelInvoke(nameof(UpdateTimeShow));
    }

    private void UpdateTimeShow()
    {
        var  diffOfDates = (DateTime.Now - StartTime);
        var a = diffOfDates.Minutes.ToString();
        var b =diffOfDates.Seconds.ToString();
        var c =diffOfDates.Milliseconds.ToString();
        Timeshow.text = a+":"+b+":"+c;
        if (p2 !=null)
        {
            Timeshow2.text = a+":"+b+":"+c;
        }
       
    }
    private void UpdateTimeShowAi()
    {
        var  diffOfDates = (DateTime.Now - StartTime);
        var a = diffOfDates.Minutes.ToString();
        var b =diffOfDates.Seconds.ToString();
        var c =diffOfDates.Milliseconds.ToString();
        TimeshowAi = a+":"+b+":"+c;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
