using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bombs : NetworkBehaviour
{
    [Tooltip("The explosion asset")]
    public GameObject Explosion;

    public float Radius = 20f;
    public float Damage= 100;

    public Player_Movement Source_Airplane;

    Game_Manager Game_Manager;

    /// <summary>
    /// A reference for the Rigidbody component
    /// </summary>
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Game_Manager = GameObject.FindObjectOfType<Game_Manager>();
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
            if(transform.rotation.eulerAngles.z > 90)
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
        if(isServer==false)
        {
            return;
        }

        if(Source_Airplane != null && Source_Airplane.GetComponent<Rigidbody>()==collision.attachedRigidbody)
        {
            return;
        }
        Collider[] cols = Physics.OverlapSphere(this.transform.position, Radius);

        foreach(Collider col in cols)
        {
            Health h = col.GetComponent<Health>();
            if(h!=null)
            {
                h.CmdChangeHealth(Damage);
            }

            if(col.name== "Raffinerie")
            {
                Game_Manager.Allies_Win.SetActive(true);
                Time.timeScale = 0;
                Cursor.visible = true;
            }
        }

        GameObject explode = GameObject.Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);
        NetworkServer.Spawn(explode);
        NetworkServer.Destroy(gameObject);
    }

}
