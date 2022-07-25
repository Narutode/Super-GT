using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [SerializeField] private scriptableObjectGenerator generalProps;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public Text message;
    
    // Start is called before the first frame update


    // Update is called once per frame
    public void LoginButton()
    {
        generalProps.Email = emailInput.text;
        generalProps.Password = passwordInput.text;
        message.text = "Successful login/account create!";
    }
}