using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour
{
    [SyncVar]
    float Hit_Points;

    public int HP;

    Player_Data Player;

    public Game_Manager Manager;

    // Start is called before the first frame update
    void Start()
    {
        if (isServer)
        {
            Hit_Points = HP;
        }

        Manager = GameObject.FindObjectOfType<Game_Manager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    [Command]
    public void CmdChangeHealth(float amount)
    {
        Hit_Points -= amount;
        if (Hit_Points <= 0)
        {
            Die();
        }
        if (gameObject.tag == "Allies")
        {
            Debug.Log(Hit_Points);
        }
        //Debug.Log(Hit_Points);
    }

    void Die()
    {
        if (isServer == false)
        {
            return;
        }

        try
        {
            Player_Movement Player = GetComponent<Player_Movement>();
            GameObject explode = GameObject.Instantiate(Player.Explosion, gameObject.transform.position, Quaternion.identity);
            NetworkServer.Spawn(explode);
        }
        catch
        {
            Debug.Log("Is not player");
        }

        if (Player_Data.Local_Player != null)
        {
            Player = Player_Data.Local_Player.GetComponent<Player_Data>();
            Player.CmdDeath(Player.Deaths + 1);
            if(Player.Team==0)
            {
                Player.Manager.CmdAllies(Player.Manager.Allies_Left-1);
            }
            else
            {
                Player.Manager.CmdAxis(Player.Manager.Axis_Left - 1);
            }
        }
        NetworkServer.Destroy(gameObject);

        if(this.gameObject.tag=="Bomber")
        {
            Manager.Axis_Win.SetActive(true);
            Time.timeScale = 0;
            Cursor.visible = true;
        }
    }

    public float Get_hit_Points()
    {
        return Hit_Points;
    }
}
