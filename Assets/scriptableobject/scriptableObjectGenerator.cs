using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Start is called before the first frame update
[Serializable]
[CreateAssetMenu(fileName = "GeneralProperties", menuName = "General Properties")]
public class scriptableObjectGenerator : ScriptableObject
{
    [SerializeField] private bool _isAuto = false;
    
    [SerializeField] private string _langue = "English";
    
    [SerializeField] private float _Hcam;
    
    
    [SerializeField] private int _indexColor;
    
    [SerializeField] private int _indexColorP2;
    
    [SerializeField] private bool _isAutoP2 = false;
    
    [SerializeField] private int _LevelIA = 0;
    
    [SerializeField] private int _typeModel = 0;
    
    [SerializeField] private int _typeModelP2 = 0;
    
    [SerializeField] private string _Email = "";
    
    [SerializeField] private string _Password = "";
    public bool IsAuto
    {
        get { return _isAuto;}
        set { _isAuto = value;}
    }
    public string Langue
    {
        get { return _langue;}
        set { _langue = value;}
    }
    public float Hcam
    {
        get { return _Hcam;}
        set { _Hcam = value;}
    }
    
   
    public int indexColor
    {
        get { return _indexColor;}
        set { _indexColor = value;}
    }
    public int indexColorP2
    {
        get { return _indexColorP2;}
        set { _indexColorP2 = value;}
    }
    public bool IsAutoP2
    {
        get { return _isAutoP2;}
        set { _isAutoP2 = value;}
    }
    public int LevelIA
    {
        get { return _LevelIA;}
        set { _LevelIA = value;}
    }
    
    public int typeModel
    {
        get { return _typeModel;}
        set { _typeModel = value;}
    }
    public int typeModelP2
    {
        get { return _typeModelP2;}
        set { _typeModelP2 = value;}
    }
    public string Email
    {
        get { return _Email;}
        set { _Email = value;}
    }
    public string Password
    {
        get { return _Password;}
        set { _Password = value;}
    }
    
}