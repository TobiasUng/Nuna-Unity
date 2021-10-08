using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Rumbler : MonoBehaviour
{

    InputDevice rightController;
    InputDevice leftController;
    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);
        rightController = devices[0];

        InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, devices);
        leftController = devices[0];
    }

    public void shortRumble()
    {
        Debug.Log("rumble");
        leftController.SendHapticImpulse(0u, 0.5f, 0.1f);
        rightController.SendHapticImpulse(0u, 0.5f, 0.1f);
    }
}
