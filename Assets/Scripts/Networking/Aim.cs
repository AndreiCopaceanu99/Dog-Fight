using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    /// <summary>
    /// Reference for the player;
    /// </summary>
    public GameObject Player;

    /// <summary>
    /// A reference for the Rigidbody component
    /// </summary>
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //Get acces to the Rigidbody
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        /// <summary>
        /// Keep the aim point in front of the airplane
        /// </summary>
        transform.position = Player.transform.position + Player.transform.forward * 300.0f - Player.transform.up * 0.0f;
    }
}
