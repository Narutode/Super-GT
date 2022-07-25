using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;





    


// test publish
public class CountachCarController : MonoBehaviour
{
    [SerializeField] private scriptableObjectGenerator generalProps;
    public bool objectPlace = false;
    
    public int currentLap = 0;
    public int currentCheckpoint = 0;
    public bool finished = false;
    public int placement;
    public int points = 0;

    public Camera mainCamera;
    public List<Camera> cameraList;
    public int cameraIndex=0;

    public List<AxleInfo> axleInfos;
    public double EngineTorque;
    public double maxSteeringAngle;
    public double GearboxTorque;
    public double steering;
    public WheelCollider FrontLeft, FrontRight, RearLeft, RearRight;
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
    
    private void Start()
    {
        voiture.gameObject.GetComponent<MeshRenderer>().materials[2].color = colors[generalProps.indexColor];
        gearIndex = 1;
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

        if (Input.GetKey(KeyCode.DownArrow))
        {
            brake();
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
        if (Input.GetKey(KeyCode.RightControl))
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
        // Son moteur

        if (vitesse != 0)
        {


            pitch = vitesse / topSpeed;
            engine.pitch = (float) pitch;

        }



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



        steering = 0;
        GearboxTorque = 0;
    }
}
