using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Is rotating the propeller
/// </summary>
public class Propeller_Rotation : MonoBehaviour
{
    [Tooltip("The speed of the rotation")]
    public float speed = 20;

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        //Rotates constantly the propeller
        transform.Rotate(0, 0, speed);
    }
}