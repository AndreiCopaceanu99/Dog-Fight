using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// The bullet script
/// </summary>
public class Bullet : NetworkBehaviour
{
    [Tooltip("The speed of the bullet")]
    public float Speed;

    public float Damage = 100;

    float Bullet_Disappear;

    public Player_Movement Source_Airplane;

    [SyncVar]
    Vector3 serverPosition;

    //[Tooltip("The player")]
    //public GameObject Player;

    /// <summary>
    /// A reference for the Rigidbody component
    /// </summary>
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //Get acces to the Rigidbody
        rb = GetComponent<Rigidbody>();

        Bullet_Disappear = 0;

        //Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //The movement forward
        rb.velocity = -transform.up * Speed * Time.deltaTime;

        Bullet_Disappear += Time.deltaTime;
        /////<summary>
        /////The Distance between the bullet and the airplane
        ///// </summary>
        //float Distance = Vector3.Distance(Player.transform.position, transform.position);

        //CmdUpdatePosition(transform.position);

        ////Destroys the bullet if it gets to far away
        if (Bullet_Disappear > 10)
        {
            NetworkServer.Destroy(gameObject);
        }
    }

    //[Command]
    //void CmdUpdatePosition(Vector3 newPosition)
    //{
    //    serverPosition = newPosition;
    //}

    /// <summary>
    /// The collide detector
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter(Collider collision)
    {
        if (isServer == false)
        {
            return;
        }

        if (Source_Airplane != null && Source_Airplane.GetComponent<Rigidbody>() == collision.attachedRigidbody)
        {
            return;
        }

        if (collision.gameObject.tag == "Allies" || collision.gameObject.tag == "Axis" || collision.gameObject.tag=="Bomber")
        {
            Debug.Log("Hit");
            Health h = collision.GetComponent<Health>();
            if (h != null)
            {
                
                h.CmdChangeHealth(1);
                if(h.HP==0)
                {
                    Source_Airplane.Player_Data.CmdKill(Source_Airplane.Player_Data.Kills+1);
                }
            }
        }

        NetworkServer.Destroy(gameObject);
    }
}
