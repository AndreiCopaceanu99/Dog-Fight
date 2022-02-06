using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Torus : MonoBehaviour
{
    public Tutorial_Manager Points;
    public GameObject Torus;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Allies")
        {
            Points.Points++;
            var Torus_Renderer = Torus.GetComponent<Renderer>();
            Torus_Renderer.material.SetColor("_Color", Color.green);
            Debug.Log("Green");
        }
    }
}
