using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bomber : NetworkBehaviour
{
    public float Speed;
    public GameObject[] Target;
    int i=0;
    public GameObject Bomb_Prefab;
    public GameObject[] Bomb_Spawner;
    public float Bombing_Time;
    float time=0;
    Rigidbody rb;
    [SyncVar]
    Vector3 serverPosition;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        rb = GetComponent<Rigidbody>();

        Target[0] = GameObject.Find("Bunker Target");
        Target[1] = GameObject.Find("Railway Target");
        Target[2] = GameObject.Find("Raffinerie Target");


    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (i == 3)
        {
            rb.velocity = transform.forward * Speed;
        }
        else
        {
            Go_To_Target(Target[i]);
        }

        RpcUpdatePosition(transform.position);
    }

    void Go_To_Target(GameObject target)
    {
        transform.LookAt(target.transform);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime);
        if(transform.position==target.transform.position)
        {
            i++;
        }

        float dist = Vector3.Distance(target.transform.position, transform.position);
        if (dist<1000f)
        {
            RpcBomb();
        }
    }

    void RpcUpdatePosition(Vector3 newPosition)
    {
        serverPosition = newPosition;
        RpcFixPosition(serverPosition);
    }

    void RpcFixPosition(Vector3 newPosition)
    {

        transform.position = newPosition;

    }

    void RpcBomb()
    {
        time += Time.deltaTime;
        if (time >= Bombing_Time)
        {
            foreach (GameObject t in Bomb_Spawner)
            {
                GameObject go = Instantiate(Bomb_Prefab,
                     t.transform.position,
                     t.transform.rotation
                     );
                NetworkServer.Spawn(go);
            }
            time = 0;
        }
    }
}
