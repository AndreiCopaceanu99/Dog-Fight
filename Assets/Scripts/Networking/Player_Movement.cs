using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// The movement of the player
/// </summary>
public class Player_Movement : NetworkBehaviour
{
    [Tooltip("The speed of the player")]
    public float Speed;

    [Tooltip("The speed of the rotation")]
    public float Rotation;

    [Tooltip("The bullet")]
    public GameObject BulletPrefab;

    public GameObject Bullet_Explosion;

    float Shooting_Time;

    [Tooltip("The position where the bullets are spawned")]
    public List<GameObject> Spawn = new List<GameObject>();

    [Tooltip("Main Camera")]
    public Camera Camera;

    //[Tooltip("The canvas for the pause menu")]
    //public GameObject PauseMenu;

    [Tooltip("The explosion asset")]
    public GameObject Explosion;

    [Tooltip("The bombs on the wings")]
    public GameObject[] Bombs;

    public float Gun_Jammed=0;
    float Gun_Jammed_Time;

    public Player_Data Player_Data;

    //Health h;

    static public Player_Movement Local_Player { get; protected set; }

    [SyncVar]
    Vector3 serverPosition;

    Vector3 serverPositionSmoothVelocity;

    /// <summary>
    /// Number of bombs;
    /// </summary>
    int Bombs_Count=2;

    [Tooltip("The bomb that is spawned")]
    public GameObject Bomb;

    [Tooltip("The position where the bomb is spawned")]
    public GameObject[] Bombs_Spawners;

    /// <summary>
    /// A reference for the Rigidbody component
    /// </summary>
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //Get acces to the Rigidbody
        rb = GetComponent<Rigidbody>();

        Camera = Camera.main;

        //PauseMenu = GameObject.Find("Pause_Menu");

        Player_Data = GameObject.FindObjectOfType<Player_Data>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority)
        {
            Local_Player = this;

            if (Player_Data.Pause_Menu.Activate == 0)
            {
                AuthorityUpdate();
            }

            if (Gun_Jammed > 0 && Gun_Jammed < 100)
            {
                Gun_Jammed -= 0.3f;
            }
        }

        //if(PauseMenu==null)
        //{
        //    PauseMenu = GameObject.Find("Pause_Menu");
        //}

        if(hasAuthority==false)
        {
            transform.position = Vector3.SmoothDamp(
                transform.position,
                serverPosition,
                ref serverPositionSmoothVelocity,
                0.25f);
        }
    }

    private void FixedUpdate()
    {
        if (hasAuthority==false)
            return;

        Move();

        Camera_Movement();
    }

    void AuthorityUpdate()
    {
        if (Input.GetButton("Fire1") && Gun_Jammed<100)
        {
            Gun_Jammed += 0.5f;
            Fire();
        }

        if(Gun_Jammed>=100 && Gun_Jammed_Time<5f)
        {
            Gun_Jammed_Time += Time.deltaTime;
            Gun_Jammed = 100;

            if(Gun_Jammed_Time>5f)
            {
                Gun_Jammed = 0;
                Gun_Jammed_Time = 0;
            }
        }


        if ((Input.GetButtonDown("Fire2") && (Bombs_Count != 0)))
        {
            Drop_Bomb();
        }
    }

    /// <summary>
    /// The movement
    /// </summary>
    void Move()
    {
        //The forward movement
        rb.velocity = transform.forward  * Speed;

        //"Gravity" effect on the speed
        Speed -= transform.forward.y * Time.deltaTime * 50.0f;

        //Stoping the airplane speed to go unde 35.0f
        if (Speed < 300.0f)
        {
            Speed = 300.0f;
        }

        /// <summary>
        /// The rotation of the airplane
        /// </summary>
        if (Player_Data.Pause_Menu.Activate == 0)
        {
            transform.Rotate(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), -Input.GetAxis("Horizontal") * Rotation * Time.deltaTime);
        }

        CmdUpdatePosition(transform.position);
    }

    [Command]
    void CmdUpdatePosition(Vector3 newPosition)
    {
        serverPosition = newPosition;
    }

    [ClientRpc]
    void RpcFixPosition(Vector3 newPosition)
    {
        // We've received a message from the server to immediately
        // correct this tank's position.
        // This is probably only going to happen if the client tried to 
        // move in some kind of illegal manner.

        transform.position = newPosition;

    }
    /// <summary>
    /// The main camera movement
    /// </summary>
    void Camera_Movement()
    {
        /// <summary>
        /// Camera position
        /// </summary>
        Vector3 moveCamTo = transform.position - transform.forward * 20.0f + Vector3.up * 40.0f;

        //The procent for the spring function
        float bias = 0.80f;

        //The movement of the camera
        Camera.transform.position = Camera.transform.position * bias + moveCamTo * (1.0f - bias);

        //Camera.transform.rotation = transform.rotation;

        //Debug.Log(transform.rotation);
        //Debug.Log(Camera.transform.rotation);


        //Camera is looking in front of the airplane
        Camera.transform.LookAt(transform.position + transform.forward * 30.0f, transform.up);
    }

    /// <summary>
    /// The bullets are fired
    /// </summary>
    void Fire()
    {
        Shooting_Time += Time.deltaTime;
        if (Shooting_Time >= 1f)
        {
            foreach (GameObject spawn in Spawn)
            {
                CmdFireBullet(spawn);
            }
            Shooting_Time = 0;
        }
    }

    //[Command]
    void CmdFireBullet(GameObject bulletPosition)
    {
        GameObject go = Instantiate(BulletPrefab,
            bulletPosition.transform.position,
            bulletPosition.transform.rotation
        );
        go.GetComponent<Bullet>().Source_Airplane = this;
        NetworkServer.Spawn(go);
        go = Instantiate(Bullet_Explosion,
            bulletPosition.transform.position,
            bulletPosition.transform.rotation
        );
        NetworkServer.Spawn(go);
    }

    /// <summary>
    /// The bombs are launched
    /// </summary>
    void Drop_Bomb()
    {
        //Disables the bombs on the wings
        //foreach (GameObject bomb in Bombs)
        //{
        //    bomb.SetActive(false);
        //}
        //Spawnes the actual bombs
        if(Bombs_Count==2)
        {
            Bombs[0].SetActive(false);
            CmdDropBomb(Bombs_Spawners[0]);
            //GameObject bomb = GameObject.Instantiate(Bomb, Bombs_Spawners[0].transform.position, Bombs_Spawners[0].transform.rotation) as GameObject;
            //NetworkServer.Spawn(bomb);
        }
        else
        {
            Bombs[1].SetActive(false);
            CmdDropBomb(Bombs_Spawners[1]);
            //GameObject bomb = GameObject.Instantiate(Bomb, Bombs_Spawners[1].transform.position, Bombs_Spawners[1].transform.rotation) as GameObject;
            //NetworkServer.Spawn(bomb);
        }

        Bombs_Count--;
    }

    //[Command]
    void CmdDropBomb(GameObject BombPosition)
    {
        GameObject go = Instantiate(Bomb,
            BombPosition.transform.position,
            BombPosition.transform.rotation
        );
        go.GetComponent<Bombs>().Source_Airplane = this;

        NetworkServer.Spawn(go);
    }

    /// <summary>
    /// The collide detector
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Bullets")
        {
            
        }
        else
        {
            //GameObject explode = GameObject.Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);
            //NetworkServer.Spawn(explode);
            Health h = GetComponent<Health>();
            h.CmdChangeHealth(100);

            Player_Data.Pause_Menu.Pause_Menu_On();
            //NetworkServer.Destroy(gameObject);
        }

    }
}

