using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeCar : MonoBehaviour
{
    [SerializeField] private scriptableObjectGenerator generalProps;
    [SerializeField] private Button PreviousButton;
    [SerializeField] private Button LastButton;
    private int currentCar;
    [SerializeField] private GameObject voiture;
    private List<Color> colors = new List<Color> {Color.blue,Color.yellow,Color.magenta,Color.green,Color.red,Color.black};
    public GameObject img1;
    public GameObject img2;
    public GameObject img3;
    public GameObject imgP21;
    public GameObject imgP22;
    public GameObject imgP23;
    private void Awake()
    {
        SelectCar(0);
    }

    private void SelectCar(int index)
    {
        
        PreviousButton.interactable = (index != 0);
        LastButton.interactable = (index != colors.Count - 1);
        voiture.gameObject.GetComponent<MeshRenderer>().materials[2].color = colors[index];
        generalProps.indexColor = index;
    }
    
    public void changeCars(int change)
    {
        currentCar += change;
        SelectCar(currentCar);
    }
    
    private void SelectCarP2(int index)
    {
        
        PreviousButton.interactable = (index != 0);
        LastButton.interactable = (index != colors.Count - 1);
        voiture.gameObject.GetComponent<MeshRenderer>().materials[2].color = colors[index];
        generalProps.indexColorP2 = index;
    }
    
    public void changeCarsP2(int change)
    {
        currentCar += change;
        SelectCarP2(currentCar);
    }
    
    private void SelectModel(int index)
    {
        PreviousButton.interactable = (index != 0);
        LastButton.interactable = (index != transform.childCount - 1);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive( i == index);
        }

        generalProps.typeModel = index;
        if (index == 0)
        {
            img1.SetActive(true);
            img2.SetActive(false);
            img3.SetActive(false);
        }
        if (index == 1)
        {
            img1.SetActive(false);
            img2.SetActive(true);
            img3.SetActive(false);
        }
        if (index == 2)
        {
            img1.SetActive(false);
            img2.SetActive(false);
            img3.SetActive(true);
        }
    }
    
    public void changeModel(int change)
    {
        currentCar += change;
        SelectModel(currentCar);
    }
    private void SelectModel2(int index)
    {
        PreviousButton.interactable = (index != 0);
        LastButton.interactable = (index != transform.childCount - 1);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive( i == index);
        }

        generalProps.typeModelP2 = index;
        if (index == 0)
        {
            imgP21.SetActive(true);
            imgP22.SetActive(false);
            imgP23.SetActive(false);
        }
        if (index == 1)
        {
            imgP21.SetActive(false);
            imgP22.SetActive(true);
            imgP23.SetActive(false);
        }
        if (index == 2)
        {
            imgP21.SetActive(false);
            imgP22.SetActive(false);
            imgP23.SetActive(true);
        }
    }
    
    public void changeModel2(int change)
    {
        currentCar += change;
        SelectModel2(currentCar);
    }
}
