using System;
using UnityEngine;
using System.Collections.Generic;
using  UnityEngine.UI;

using Random = UnityEngine.Random;


public class CarControllerAI : MonoBehaviour
{
    
    public Transform path;
    private List<Transform> nodes;
    public int currentNode = 0;
    public float currentSpeed;
    public float maxSpeed = 1.0f;
    public string Circuit1Time;
    public string Circuit2Time;
    public string Circuit3Time;
    public bool objectPlace = false;
    public string Name = "Bot";
    public string Time = "0.00";
    public int currentLap = 0;
    public int currentCheckpoint = 0;
    public bool finished = false;
    public int placement = 0;
    public int points = 0;
    public Camera mainCamera;
    [SerializeField] private scriptableObjectGenerator generalProps;
    public List<AxleInfo> axleInfos;
    public double EngineTorque;
    public double maxSteeringAngle;
    public double GearboxTorque;
    public double steering;
    public WheelCollider FrontLeft, FrontRight, RearLeft, RearRight;
    public float StiffnessDefaultFrontLeft,
        StiffnessDefaultFrontRight,
        StiffnessDefaultRearLeft,
        StiffnessDefaultRearRight;
    public int VarbrakeTorque;
    private double RPM;
    public static double vitesse;
    public Rigidbody rb;
    public Transform[] paths;
    public TournamentManager Tm;
    public CircuitManager Cm;
    
    // Son moteur

    private double topSpeed = 220d;
    private double pitch = 0;
    public AudioSource engine;

    public void GetPath()
    {
        Transform[] pathTransform = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransform.Length; i++)
        {
            if (pathTransform[i] != path.transform)
            {
                nodes.Add(pathTransform[i]);
            }
        }
    }

    void Start()
    {
        
        Transform[] pathTransform = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransform.Length; i++)
        {
            if (pathTransform[i] != path.transform)
            {
                nodes.Add(pathTransform[i]);
            }
        }
        

        if (generalProps.LevelIA == 0)
        {
            EngineTorque = Random.Range(60,100);
        }
        if (generalProps.LevelIA == 1)
        {
            EngineTorque = Random.Range(100,140);
        }
        if (generalProps.LevelIA == 2)
        {
            EngineTorque = Random.Range(140,180);
        }
        gearIndex = 1;
        
        StiffnessDefaultFrontLeft = FrontLeft.sidewaysFriction.stiffness;
        StiffnessDefaultFrontRight = FrontRight.sidewaysFriction.stiffness;
        StiffnessDefaultRearLeft = RearLeft.sidewaysFriction.stiffness;
        StiffnessDefaultRearRight = RearRight.sidewaysFriction.stiffness;
    }




    // Vitesses
    public double[] gears = new double[] {-3.500, 0, 3.500, 2.083, 1.368, 0.962, 0.821};
    public static int gearIndex;


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
        
        
        path = paths[Tm.currentCircuit];
        if (transform.rotation.eulerAngles.z > 150 && transform.rotation.eulerAngles.z< 180)
        {
            transform.rotation = (Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, 0)));
        }
        RearLeft.brakeTorque = 0;
        RearRight.brakeTorque = 0;
        FrontLeft.brakeTorque = 0;
        FrontRight.brakeTorque = 0;

        // Vitesse (km/h)

        // vitesse = Math.Abs(2* 3.6 * Math.PI * RearLeft.radius * RearLeft.rpm / 60);

        vitesse = rb.velocity.magnitude * 3.6f;



        // Vitesses



        if (vitesse < 40)
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







        // Contrôles
        /*
        if (Input.GetKey(KeyCode.Space))
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

            Brake();


        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            steering = maxSteeringAngle * 1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            steering = maxSteeringAngle * -1;
        }
        */
    }
    

    public void FixedUpdate()
    {
        ApplySteer();
        Drive();
        CheckWaypointDistance();




        // Son moteur

        if (vitesse != 0)
        {


            pitch = vitesse / topSpeed;
            engine.pitch = (float) pitch;

        }



        steering = 0;
        //GearboxTorque = 0;
    }

    private void ApplySteer()
    {
       Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        
        double newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteeringAngle;
        foreach (var axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = (float) newSteer;
                axleInfo.rightWheel.steerAngle = (float) newSteer;
                ApplyLocalPositionToVisuals(axleInfo.leftWheel);
                ApplyLocalPositionToVisuals(axleInfo.rightWheel);
            }
        }
    }

    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * FrontLeft.radius * FrontLeft.rpm * 60 / 1000;
        


            if (gearIndex != 1)
            {

                if (gearIndex == 0 && vitesse < 30 || gearIndex == 2 && vitesse < 40 ||
                    gearIndex == 3 && vitesse < 70 ||
                    gearIndex == 4 && vitesse < 110 || gearIndex == 5 && vitesse < 160 ||
                    gearIndex == 6 && vitesse < 220)
                {
                    GearboxTorque = (float) (EngineTorque * gears[gearIndex]);
                }
            }

            foreach (var axleInfo in axleInfos)
            {
                if (axleInfo.motor)
                {

                    axleInfo.leftWheel.motorTorque = (float) GearboxTorque/2;
                    axleInfo.rightWheel.motorTorque = (float) GearboxTorque/2;
                    RPM = (RearLeft.rpm + RearRight.rpm) / 2 * gears[gearIndex];
                }

            }
        
        
    }




    private void Brake()
    {
        FrontLeft.brakeTorque = VarbrakeTorque;
        FrontRight.brakeTorque = VarbrakeTorque;
    }

    private void CheckWaypointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 4.0f)
        {
            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
    }
}