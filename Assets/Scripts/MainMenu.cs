using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private scriptableObjectGenerator generalProps;
    public void PlayGame()
    {
        if (generalProps.Hcam == 1)
        {
            SceneManager.LoadScene("Solo");
        }
        else
        {
            SceneManager.LoadScene("Multijoueur");
        }
    }
    public void QuitGame()
        {
            Debug.Log("QUIT");
            Application.Quit();
        }

    public void IsAuto()
    {
        generalProps.IsAuto = true;
    }

    public void IsManual()
    {
        generalProps.IsAuto = false;
    }
    public void IsAutoP2()
    {
        generalProps.IsAutoP2 = true;
    }

    public void IsManualP2()
    {
        generalProps.IsAutoP2 = false;
    }
    public void SoloPlayer()
    {
        generalProps.Hcam = 1;
        MainCamera.rect = new Rect(0, 0f, 1.0f , generalProps.Hcam);
    }

    public void MultiPlayer()
    {
        generalProps.Hcam = 0.5f;
        MainCamera.rect = new Rect(0, generalProps.Hcam, 1.0f , generalProps.Hcam);
    }

    public void SetDifficulty(int indexLevel)
    {
        generalProps.LevelIA = indexLevel;
    }
}
