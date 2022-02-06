using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Switch : MonoBehaviour
{
    [Tooltip("The cameras")]
    public Camera[] Cameras;

    GameObject plane;
    Camera Bombing_Camera;

    // Start is called before the first frame update
    void Start()
    {

        Cameras[0] = Camera.main;

        //Enables the main camera and disables the bomb_camera when the game begins
        Cameras[0].enabled = true;
        Cameras[1].enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        //if (!plane)
        //{
        //    plane = GameObject.FindGameObjectWithTag("Allies");
        //    if (plane)
        //    {
        //        Bombing_Camera = plane.transform.Find("Bombing_Camera").GetComponent<Camera>();
        //        Cameras[1] = Bombing_Camera;
        //        Cameras[1].enabled = false;
        //    }
            
        //}


        //Switches between the cameras
        if (Input.GetKeyDown(KeyCode.F))
        {
            Cameras[0].enabled = !Cameras[0].enabled;
            Cameras[1].enabled = !Cameras[1].enabled;
        }
    }
}
