using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Bullet : MonoBehaviour
{
    [Tooltip("The speed of the bullet")]
    public float Speed;

    public float Damage = 100;

    float Bullet_Disappear;

    public Tutorial_Player_Movement Source_Airplane;

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

        ////Destroys the bullet if it gets to far away
        if (Bullet_Disappear > 10)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// The collide detector
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag=="Targets")
        {
            Targets targets = collision.GetComponent<Targets>();
            Instantiate(targets.Explosion, targets.transform.position, targets.transform.rotation);
            Destroy(collision.gameObject);
            Tutorial_Manager points = GameObject.FindObjectOfType<Tutorial_Manager>();
            points.Points++;
        }
        if (Source_Airplane != null && Source_Airplane.GetComponent<Rigidbody>() == collision.attachedRigidbody)
        {
            return;
        }

        if (collision.gameObject.tag == "Allies" || collision.gameObject.tag == "Axis" || collision.gameObject.tag=="Bomber")
        {
            Tutorial_Health h = collision.GetComponent<Tutorial_Health>();
            if (h != null)
            {
                h.ChangeHealth(1);
            }
        }

        Destroy(gameObject);
    }
}
