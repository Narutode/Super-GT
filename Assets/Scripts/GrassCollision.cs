using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
     {
         var car1 = other.GetComponentInParent<CarControllerP1>();
         if (car1 != null)
         {
             WheelFrictionCurve wfc = car1.FrontLeft.sidewaysFriction;
             wfc.stiffness = car1.SideStiffness[0]/3;
             car1.FrontLeft.sidewaysFriction = wfc;
             wfc = car1.FrontLeft.forwardFriction;
             wfc.stiffness = car1.ForwardStiffness[0]/3;
             car1.FrontLeft.forwardFriction = wfc;
            
             wfc = car1.FrontRight.sidewaysFriction;
             wfc.stiffness = car1.SideStiffness[1]/3;
             car1.FrontRight.sidewaysFriction = wfc;
             wfc = car1.FrontRight.forwardFriction;
             wfc.stiffness = car1.ForwardStiffness[1]/3;
             car1.FrontRight.forwardFriction = wfc;

             wfc = car1.RearLeft.sidewaysFriction;
             wfc.stiffness = car1.SideStiffness[2]/3;
             car1.RearLeft.sidewaysFriction = wfc;
             wfc = car1.RearLeft.forwardFriction;
             wfc.stiffness = car1.ForwardStiffness[2]/3;
             car1.RearLeft.forwardFriction = wfc;

             wfc = car1.RearRight.sidewaysFriction;
             wfc.stiffness = car1.SideStiffness[3]/3;
             car1.RearRight.sidewaysFriction = wfc;
             wfc = car1.RearRight.forwardFriction;
             wfc.stiffness = car1.ForwardStiffness[3]/3;
             car1.RearRight.forwardFriction = wfc;
             return;
         }
         var car2 = other.GetComponentInParent<CarControllerP2>();
         if (car2 != null)
         {
             WheelFrictionCurve wfc = car2.FrontLeft.sidewaysFriction;
             wfc.stiffness = car2.StiffnessDefaultFrontLeft / 3;
             car2.FrontLeft.sidewaysFriction = wfc;
            
             wfc = car2.FrontRight.sidewaysFriction;
             wfc.stiffness = car2.StiffnessDefaultFrontRight / 3;
             car2.FrontRight.sidewaysFriction = wfc;

             wfc = car2.RearLeft.sidewaysFriction;
             wfc.stiffness = car2.StiffnessDefaultRearLeft / 3;
             car2.RearLeft.sidewaysFriction = wfc;

             wfc = car2.RearRight.sidewaysFriction;
             wfc.stiffness = car2.StiffnessDefaultRearRight / 3;
             car2.RearRight.sidewaysFriction = wfc;             
             return;
         }
         var carAI = other.GetComponentInParent<CarControllerAI>();
         if (carAI != null)
         {
             WheelFrictionCurve wfc = carAI.FrontLeft.sidewaysFriction;
             wfc.stiffness = carAI.StiffnessDefaultFrontLeft / 2;
             carAI.FrontLeft.sidewaysFriction = wfc;
            
             wfc = carAI.FrontRight.sidewaysFriction;
             wfc.stiffness = carAI.StiffnessDefaultFrontRight / 2;
             carAI.FrontRight.sidewaysFriction = wfc;

             wfc = carAI.RearLeft.sidewaysFriction;
             wfc.stiffness = carAI.StiffnessDefaultRearLeft / 2;
             carAI.RearLeft.sidewaysFriction = wfc;

             wfc = carAI.RearRight.sidewaysFriction;
             wfc.stiffness = carAI.StiffnessDefaultRearRight / 2;
             carAI.RearRight.sidewaysFriction = wfc;             }
     }

    private void OnTriggerExit(Collider other)
    {
        var car1 = other.GetComponentInParent<CarControllerP1>();
        if (car1 != null)
        {
            WheelFrictionCurve wfc = car1.FrontLeft.sidewaysFriction;
            wfc.stiffness = car1.SideStiffness[0];
            car1.FrontLeft.sidewaysFriction = wfc;
            wfc = car1.FrontLeft.forwardFriction;
            wfc.stiffness = car1.ForwardStiffness[0];
            car1.FrontLeft.forwardFriction = wfc;
            
            wfc = car1.FrontRight.sidewaysFriction;
            wfc.stiffness = car1.SideStiffness[1];
            car1.FrontRight.sidewaysFriction = wfc;
            wfc = car1.FrontRight.forwardFriction;
            wfc.stiffness = car1.ForwardStiffness[1];
            car1.FrontRight.forwardFriction = wfc;

            wfc = car1.RearLeft.sidewaysFriction;
            wfc.stiffness = car1.SideStiffness[2];
            car1.RearLeft.sidewaysFriction = wfc;
            wfc = car1.RearLeft.forwardFriction;
            wfc.stiffness = car1.ForwardStiffness[2];
            car1.RearLeft.forwardFriction = wfc;

            wfc = car1.RearRight.sidewaysFriction;
            wfc.stiffness = car1.SideStiffness[3];
            car1.RearRight.sidewaysFriction = wfc;
            wfc = car1.RearRight.forwardFriction;
            wfc.stiffness = car1.ForwardStiffness[3];
            car1.RearRight.forwardFriction = wfc;
            return;
        }
        var car2 = other.GetComponentInParent<CarControllerP2>();
        if (car2 != null)
        {
            WheelFrictionCurve wfc = car2.FrontLeft.sidewaysFriction;
            wfc.stiffness = car2.StiffnessDefaultFrontLeft;
            car2.FrontLeft.sidewaysFriction = wfc;
            
            wfc = car2.FrontRight.sidewaysFriction;
            wfc.stiffness = car2.StiffnessDefaultFrontRight;
            car2.FrontRight.sidewaysFriction = wfc;

            wfc = car2.RearLeft.sidewaysFriction;
            wfc.stiffness = car2.StiffnessDefaultRearLeft;
            car2.RearLeft.sidewaysFriction = wfc;

            wfc = car2.RearRight.sidewaysFriction;
            wfc.stiffness = car2.StiffnessDefaultRearRight;
            car2.RearRight.sidewaysFriction = wfc;            
            return;
        }
        var carAI = other.GetComponentInParent<CarControllerAI>();
        if (carAI != null)
        {
            WheelFrictionCurve wfc = carAI.FrontLeft.sidewaysFriction;
            wfc.stiffness = carAI.StiffnessDefaultFrontLeft;
            carAI.FrontLeft.sidewaysFriction = wfc;
            
            wfc = carAI.FrontRight.sidewaysFriction;
            wfc.stiffness = carAI.StiffnessDefaultFrontRight;
            carAI.FrontRight.sidewaysFriction = wfc;

            wfc = carAI.RearLeft.sidewaysFriction;
            wfc.stiffness = carAI.StiffnessDefaultRearLeft;
            carAI.RearLeft.sidewaysFriction = wfc;

            wfc = carAI.RearRight.sidewaysFriction;
            wfc.stiffness = carAI.StiffnessDefaultRearRight;
            carAI.RearRight.sidewaysFriction = wfc;         }
    }
}
