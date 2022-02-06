using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorian_Bomb : MonoBehaviour
{
    [Tooltip("The explosion asset")]
    public GameObject Explosion;

    public float Radius = 20f;
    public float Damage = 100;

    public Tutorial_Player_Movement Source_Airplane;

    /// <summary>
    /// A reference for the Rigidbody component
    /// </summary>
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Rotates the bomb to give a more natural feeling
        if (transform.rotation.eulerAngles.z < 90)
        {
            transform.Rotate(0, 0, 1);
        }
        else
            if (transform.rotation.eulerAngles.z > 90)
        {
            transform.Rotate(0, 0, -1);
        }
    }

    /// <summary>
    /// The collide detector
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.name=="AAs")
        {
            Destroy(collision.gameObject);
            Tutorial_Manager Points = GameObject.FindObjectOfType<Tutorial_Manager>();
            Points.Points += 4;
        }

        if (Source_Airplane != null && Source_Airplane.GetComponent<Rigidbody>() == collision.attachedRigidbody)
        {
            return;
        }
        Collider[] cols = Physics.OverlapSphere(this.transform.position, Radius);

        foreach (Collider col in cols)
        {
            Tutorial_Health h = col.GetComponent<Tutorial_Health>();
            if (h != null)
            {
                h.ChangeHealth(Damage);
            }
        }

        Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
