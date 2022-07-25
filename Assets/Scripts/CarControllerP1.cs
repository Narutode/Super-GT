using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;





    


// test publish
public class CarControllerP1 : MonoBehaviour
{
   
    public GameObject MainCam, ReverseCam;
    [SerializeField] private scriptableObjectGenerator generalProps;
    public bool objectPlace = false;
    private float originalRotXP2, originalRotYP2, originalRotZP2;
    public int currentLap = 0;
    public int currentCheckpoint = 0;
    public bool finished = false;
    public int placement;
    public int points = 0;
    public string Time = "0.00";
    public string Circuit1Time= "0.00";
    public string Circuit2Time = "0.00";
    public string Circuit3Time = "0.00";
    public string Circuit1Placement ="1";
    public string Circuit2Placement ="2";
    public string Circuit3Placement="3";
    public string Email;
    public string Password;
    public string Name = "player 1";
    public Camera mainCamera;
    public List<Camera> cameraList;
    public int cameraIndex=0;

    public List<AxleInfo> axleInfos;
    public List<AxleInfo> axleInfosM2;
    public double EngineTorque;
    public double maxSteeringAngle;
    public double GearboxTorque;
    public double steering;
    public WheelCollider FrontLeft, FrontRight, RearLeft, RearRight;
    public float[] SideStiffness = new float[4];
    public float[] ForwardStiffness = new float[4];
    
    public int VarbrakeTorque;
    private double RPM;
    public static double vitesse;
    public Rigidbody rb;
    private List<Color> colors = new List<Color> {Color.blue,Color.yellow,Color.magenta,Color.green,Color.red,Color.black};
    [SerializeField] private GameObject voiture;
    // Son moteur

    private double topSpeed = 220d;
    private double pitch = 0;
    public AudioSource engine;
    // Vitesses
    public double[] gears = new double[] {-3.500, 0, 3.500, 2.083, 1.368, 0.962, 0.821};
    [SerializeField]
    public static int gearIndex;

    public GameObject skin1;
    public GameObject skin2;
    public GameObject skin3;
    
    private void Start()
    {
       
        
        voiture.gameObject.GetComponent<MeshRenderer>().materials[2].color = colors[generalProps.indexColor];
      
        Email = generalProps.Email;
        Password = generalProps.Password;
        gearIndex = 1;
        originalRotXP2 = transform.rotation.x;
        originalRotYP2 = transform.rotation.y;
        originalRotZP2 = 0;
    }

    private void Awake()
    {
        ChooseModelCar(generalProps.typeModel); 
    }

    void brake()
    {
        FrontLeft.brakeTorque = VarbrakeTorque;
        FrontRight.brakeTorque = VarbrakeTorque;
    }

    private static void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        var visualWheel = collider.transform.GetChild(0);

        collider.GetWorldPose(out var position, out var rotation);
        rotation = rotation * Quaternion.Euler(new Vector3(0, 0, 90));

        var transform1 = visualWheel.transform;
        transform1.position = position;
        transform1.rotation = rotation;

    }
    
    void Reverse()
    {
        if (FrontLeft.rpm <= 0)
        {
            
            gearIndex = 0;

            //MainCam.SetActive(false);
            //ReverseCam.SetActive(true);

            if (vitesse < 30)
            {

                GearboxTorque = (float) (EngineTorque * gears[gearIndex]);
            }
                
        }
        else if (FrontLeft.rpm > 0)
        {
            
            
            GearboxTorque = 0;
            FrontLeft.brakeTorque = VarbrakeTorque;
            FrontRight.brakeTorque = VarbrakeTorque;
             
        }
    }
    
    public void ChooseModelCar(int mode)
    {
        if (mode == 0)
        {
            //mettre les changements pour la conduite ici en fonction du modele 1
            EngineTorque = 550;

            WheelFrictionCurve wfcForward = new WheelFrictionCurve();
            wfcForward.extremumSlip = 0.6f;
            wfcForward.extremumValue = 1;
            wfcForward.asymptoteSlip = 0.8f;
            wfcForward.asymptoteValue = 0.5f;
            wfcForward.stiffness = 2f;
            WheelFrictionCurve side = new WheelFrictionCurve();
            side.extremumSlip = 0.5f;
            side.extremumValue = 1;
            side.asymptoteSlip = 0.8f;
            side.asymptoteValue = 0.75f;
            side.stiffness = 1.38f;
            FrontLeft.sidewaysFriction = side;
            FrontLeft.forwardFriction = wfcForward;
            
            RearLeft.sidewaysFriction = side;
            RearLeft.forwardFriction = wfcForward;
            
            RearRight.sidewaysFriction = side;
            RearRight.forwardFriction = wfcForward;
            
            FrontRight.sidewaysFriction = side;
            FrontRight.forwardFriction = wfcForward;
            
            skin1.SetActive(true);
        }
        if (mode == 1)
        {
            //modele par defaut pas de changement au niveau de la conduite à faire
            skin2.SetActive(true);
        }
        if (mode == 2)
        {
            //mettre les changements pour la conduite ici en fonction du modele 3
            EngineTorque = 300;
            WheelFrictionCurve wfcForward = new WheelFrictionCurve();
            wfcForward.extremumSlip = 0.6f;
            wfcForward.extremumValue = 1;
            wfcForward.asymptoteSlip = 0.8f;
            wfcForward.asymptoteValue = 0.5f;
            wfcForward.stiffness = 2f;
            WheelFrictionCurve side = new WheelFrictionCurve();
            side.extremumSlip = 0.5f;
            side.extremumValue = 1;
            side.asymptoteSlip = 0.8f;
            side.asymptoteValue = 4f;
            side.stiffness = 1;
            FrontLeft.sidewaysFriction = side;
            FrontLeft.forwardFriction = wfcForward;
            
            RearLeft.sidewaysFriction = side;
            RearLeft.forwardFriction = wfcForward;
            
            RearRight.sidewaysFriction = side;
            RearRight.forwardFriction = wfcForward;
            
            FrontRight.sidewaysFriction = side;
            FrontRight.forwardFriction = wfcForward;
            skin3.SetActive(true);
        }

        SideStiffness[0] = FrontLeft.sidewaysFriction.stiffness;
        SideStiffness[1] = FrontRight.sidewaysFriction.stiffness;
        SideStiffness[2] = RearLeft.sidewaysFriction.stiffness;
        SideStiffness[3] = RearRight.sidewaysFriction.stiffness;
        
        ForwardStiffness[0] = FrontLeft.forwardFriction.stiffness;
        ForwardStiffness[1] = FrontRight.forwardFriction.stiffness;
        ForwardStiffness[2] = RearLeft.forwardFriction.stiffness;
        ForwardStiffness[3] = RearRight.forwardFriction.stiffness;
    }
    
    public void Update()
    {
        
        
        RearLeft.brakeTorque = 0;
        RearRight.brakeTorque = 0;
        FrontLeft.brakeTorque = 0;
        FrontRight.brakeTorque = 0;

        // Vitesse (km/h)

        // vitesse = Math.Abs(2* 3.6 * Math.PI * RearLeft.radius * RearLeft.rpm / 60);

        vitesse = rb.velocity.magnitude * 3.6f;


        // Vitesses
        if (gearIndex > 0)
        {
            if (Input.GetKeyDown(KeyCode.B) && generalProps.IsAuto == false)
            {

                gearIndex--;
            }

        }

        if (gearIndex < 6)
        {
            if (Input.GetKeyDown(KeyCode.N) && generalProps.IsAuto == false)
            {
                
                
                gearIndex++;
                
            }
        }
        
        if (generalProps.IsAuto == true)
        {
            if (vitesse < 40 )
            {
                gearIndex = 2;
            }
            else if (vitesse < 70)
            {
                gearIndex = 3;
            }
            else if (vitesse < 110)
            {
                gearIndex = 4;
            }
            else if (vitesse < 160)
            {
                gearIndex = 5;
            }
            else if (vitesse < 220)
            {
                gearIndex = 6;
            }
        }





        // Contrôles
        
        if (Input.GetKey(KeyCode.I))
        {
            transform.rotation = (Quaternion.Euler(new Vector3(originalRotXP2, originalRotYP2, originalRotZP2)));
        }
        
        if (Input.GetKey(KeyCode.RightControl))
        {
            RearLeft.brakeTorque = 10000;
            RearRight.brakeTorque = 10000;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (gearIndex != 1)
            {

                if (gearIndex == 0 && vitesse < 30 || gearIndex == 2 && vitesse < 40 ||
                    gearIndex == 3 && vitesse < 70 || gearIndex == 4 && vitesse < 110 ||
                    gearIndex == 5 && vitesse < 160 || gearIndex == 6 && vitesse < 220)
                {
                    GearboxTorque = (float) (EngineTorque * gears[gearIndex]);
                }
            }
        }

        if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
            brake();
        }
        
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow) && generalProps.IsAuto == true)
        {
            Reverse();
        }
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            steering = maxSteeringAngle * 1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            steering = maxSteeringAngle * -1;
        }
        
        //Camera
        if (Input.GetKeyUp(KeyCode.K))
        {
            cameraList[cameraIndex].enabled = false;
            cameraList[cameraIndex].targetDisplay = 1;
            cameraIndex++;
            if (cameraIndex == cameraList.Count)
                cameraIndex = 0;
            cameraList[cameraIndex].enabled = true;
            cameraList[cameraIndex].targetDisplay = 0;
        }
    }

    public void FixedUpdate()
    {
        
        if (FrontLeft.rpm >= 0 && GearboxTorque > 0)
        {
            MainCam.SetActive(true);
            ReverseCam.SetActive(false);
        }
        // Son moteur

        if (vitesse != 0)
        {


            pitch = vitesse / topSpeed;
            engine.pitch = (float) pitch;

        }


        if (generalProps.typeModel == 0)
        {
            foreach (var axleInfo in axleInfosM2)
            {
                if (axleInfo.steering)
                {
                    axleInfo.leftWheel.steerAngle = (float) steering;
                    axleInfo.rightWheel.steerAngle = (float) steering;
                }

                if (axleInfo.motor)
                {
                    axleInfo.leftWheel.motorTorque = (float) GearboxTorque / 2;
                    axleInfo.rightWheel.motorTorque = (float) GearboxTorque / 2;
                }

                RPM = (RearLeft.rpm + RearRight.rpm) / 2 * gears[gearIndex];

                ApplyLocalPositionToVisuals(axleInfo.leftWheel);
                ApplyLocalPositionToVisuals(axleInfo.rightWheel);
            }
        }
        else
        {
            foreach (var axleInfo in axleInfos)
            {
                if (axleInfo.steering)
                {
                    axleInfo.leftWheel.steerAngle = (float) steering;
                    axleInfo.rightWheel.steerAngle = (float) steering;
                }

                if (axleInfo.motor)
                {
                    axleInfo.leftWheel.motorTorque = (float) GearboxTorque / 2;
                    axleInfo.rightWheel.motorTorque = (float) GearboxTorque / 2;
                }

                RPM = (RearLeft.rpm + RearRight.rpm) / 2 * gears[gearIndex];

                ApplyLocalPositionToVisuals(axleInfo.leftWheel);
                ApplyLocalPositionToVisuals(axleInfo.rightWheel);
            }
        }
        



        steering = 0;
        GearboxTorque = 0;
    }
}
