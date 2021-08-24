using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallConfig : MonoBehaviour
{
    public IFeedback feedback;
    public GameObject playerCamera;
    public GameObject publisher;
    public GameObject screwSpawner;
    public Material[] screwColors;

    public Material randomColor()
    {
        return screwColors[Random.Range(0, screwColors.Length)];
    }
}
