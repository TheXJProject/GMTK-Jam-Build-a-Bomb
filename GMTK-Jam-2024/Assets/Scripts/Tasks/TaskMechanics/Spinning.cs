using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 0, 100); // Speed of rotation in degrees per second for each axis
    public bool isSpinning = false;

    void Update()
    {
        if (isSpinning)
        {
            transform.Rotate(rotationSpeed * Time.deltaTime);
        }
        // Rotate the object based on the rotation speed and time
    }
}
