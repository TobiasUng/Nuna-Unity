using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RumbleFeedbackContinuous : IContinuousFeedback
{

    InputDevice rightController;
    float rightRumble;
    InputDevice leftController;
    float leftRumble;

    private void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);
        rightController = devices[0];

        InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, devices);
        leftController = devices[0];
    }

    public override void giveFeedback(float distance, float angle, GameObject placement)
    {

        if (initDistance == null)
        {
            initDistance = distance;
        }

        if(angle < 0)
        {
            rightRumble = mapToZeroOne(Mathf.Min(1, distance), 0, (float)initDistance) * angle;
            leftRumble = 0;
        }

        else
        {
            rightRumble = 0;
            leftRumble = Mathf.Abs(mapToZeroOne(Mathf.Min(1, distance), 0, (float)initDistance) * -angle);
        }

        if (rightController != null && leftController != null)
        {
            //hiFreq = Mathf.Abs(mapToZeroOne(Mathf.Min(1, distance), 0, (float)initDistance)*angle);
            continuous(distance, placement);
            
        }

    }

    public override void continuous(float distance, GameObject placement)
    {
        //hiFreq = Mathf.Min(1, distance);
        
        leftRumble = Mathf.Pow(leftRumble, 3);
        rightRumble = Mathf.Pow(rightRumble, 3);
        leftController.StopHaptics();
        rightController.StopHaptics();
        leftController.SendHapticImpulse(0u, leftRumble);
        rightController.SendHapticImpulse(0u, rightRumble);
        
    }


    public override void stopFeedback()
    {
        initDistance = null;
        leftController.StopHaptics();
        rightController.StopHaptics();
    }
}
