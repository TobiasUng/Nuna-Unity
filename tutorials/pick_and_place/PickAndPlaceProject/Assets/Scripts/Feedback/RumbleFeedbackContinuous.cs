using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RumbleFeedbackContinuous : IContinuousFeedback
{

    InputDevice rightController;
    public float rightRumble;
    public InputDevice leftController;
    public float leftRumble;

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

        if (initDistance == null || distance > initDistance)
        {
            initDistance = distance;
        }

        if (angle < 0.07 && angle > -0.07)
        {
            leftRumble = rightRumble = Mathf.Max(mapToZeroOne(distance, 0, (float)initDistance), 0.1f);
            
        }

        else if(angle > 0)
        {
                        
            rightRumble = Mathf.Max(mapToZeroOne(distance, 0, (float) initDistance), 0.1f);
            //rightRumble = 0.5f;
            leftRumble = 0;
        }

        else
        {
            rightRumble = 0;

            //leftRumble = 0.5f;
            leftRumble = Mathf.Max(mapToZeroOne(distance, 0, (float)initDistance), 0.1f);
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
        
        
        leftController.StopHaptics();
        rightController.StopHaptics();
        leftController.SendHapticImpulse(0u, leftRumble, Time.deltaTime);
        rightController.SendHapticImpulse(0u, rightRumble, Time.deltaTime);
        
    }


    public override void stopFeedback()
    {
        initDistance = null;
        leftController.StopHaptics();
        rightController.StopHaptics();
    }
}
