using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomMaterial
{
    public static Material[] colors;

   

    public static void getRandomMaterial()
    {
        int rnd = Random.Range(1, 3);
    }
}
