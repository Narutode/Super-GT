using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebManager : MonoBehaviour
{
    public CarControllerP1 car1;
    public Text player1name;
    
    
    public void ViewUrl()
    {
        string player1nametext = player1name.text;
        String Player1Circuit1Time = car1.Circuit1Time;
        String Player1Circuit2Time = car1.Circuit2Time;
        String Player1Circuit3Time = car1.Circuit3Time;
        String Player1Circuit1Placement = car1.Circuit1Placement;
        String Player1Circuit2Placement = car1.Circuit2Placement;
        String Player1Circuit3Placement = car1.Circuit3Placement;
        String Email = car1.Email;
        String Password = car1.Password;
        
        String url = "http://localhost:8080/SuperGt/index.php?"+"Name="+player1nametext+"&Cuircuit1Time="+Player1Circuit1Time+
                     "&Cuircuit2Time="+Player1Circuit2Time+"&Cuircuit3Time="+Player1Circuit3Time+"&Cuircuit1Placement="+
                     Player1Circuit1Placement+"&Cuircuit2Placement="+Player1Circuit2Placement+"&Cuircuit3Placement="+
                    Player1Circuit3Placement+"&Email="+Email+"&Password="+Password;
        Application.OpenURL(url);
    }
}
