using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
using UnityEngine.XR;

public class RumbleFeedbackPulse : IPulseFeedback
{
    public float minMotorSpeed = 0.1f;
    public bool shouldRumble = true;
    public float? pulseDuration;
    public float? pulseStep = 0.1f;

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

    private void Update()
    {
        
    }



    /*public void startRumble(float distance)
    {
        if (Gamepad.current != null)
        {
            loFreq = hiFreq = Mathf.Max(mapToZeroOne(distance, 0, 1), minMotorSpeed);
            Gamepad.current.SetMotorSpeeds(0.0f, hiFreq);
        }
    }*/

    public override void giveFeedback(float distance, float angle, GameObject placement)
    {


        if (angle < deadZone && angle > -deadZone)
        {
            leftRumble = 0.5f;
            rightRumble = 0.5f;
        }

        else if (angle > 0)
        {
            leftRumble = 0;
            rightRumble = 0.5f;
        }

        else
        {
            leftRumble = 0.5f;
            rightRumble = 0;
        }

        if (rightController != null && leftController != null)
        {            
            calcPulseStep(distance);
            pulsate();
        }
    }

    public void calcPulseStep(float distance)
    {
        pulseStep = Mathf.Abs(mapToZeroOne(distance, 0, 1) - 1);
    }

    public override void pulsate()
    {
        if (pulseDuration == null)
        {
            pulseDuration = Time.time + pulseStep;
        }

        if(Time.time > pulseDuration)
        {
            shouldRumble = !shouldRumble;
            pulseDuration = Time.time + pulseStep;

        }

        if (shouldRumble)
        {
            leftController.StopHaptics();
            rightController.StopHaptics();
            leftController.SendHapticImpulse(0u, leftRumble, Time.deltaTime);
            rightController.SendHapticImpulse(0u, rightRumble, Time.deltaTime);
        }

        else
        {
            leftController.StopHaptics();
            rightController.StopHaptics();
        }

    }

    public override void stopFeedback()
    {
        if (rightController != null && leftController != null)
        {
            leftController.StopHaptics();
            rightController.StopHaptics();
        }
    }

   

}
