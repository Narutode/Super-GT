using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeShow2 : MonoBehaviour
{
    private Text Timeshow;
    public bool Running;
    private DateTime StartTime;
    

    private void Awake()
    {
        Timeshow = transform.Find("TimeShow2").GetComponent<Text>();
        
    }

    private void StartGameAction()
    {
        Running = true;
        StartTime = DateTime.Now;
        InvokeRepeating(nameof(UpdateTimeShow),0,0.01f);
    }
    
    private void EndGameAction()
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
    }
    void Start()
    {
        StartGameAction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}