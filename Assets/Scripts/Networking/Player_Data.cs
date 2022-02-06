using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player_Data : NetworkBehaviour
{
    public GameObject Axis;
    public GameObject Allies;

    GameObject Player;
    GameObject My_Airplane;

    public Pause_Menu Pause_Menu;

    public GameObject Scoreboard;

    public Game_Manager Manager;

    [SyncVar]
    public int Place;

    [SyncVar]
    public int Team;

    [SyncVar]
    public int Kills=0;
    [SyncVar]
    public int Deaths=0;

    static public Player_Data Local_Player { get; protected set; }

    //static Player_Data myInstance;

    [SyncVar]
    public string PlayerName;

    // Start is called before the first frame update
    void Start()
    {
        if (isServer == false)
        {
            return;
        }


        Manager = GameObject.FindObjectOfType<Game_Manager>();
     
        Pause_Menu = GameObject.FindObjectOfType<Pause_Menu>();

        Scoreboard = GameObject.Find("Scoreboard");
    }

    // Update is called once per frame
    void Update()
    {
        if(Pause_Menu==null)
        {
            Pause_Menu = GameObject.FindObjectOfType<Pause_Menu>();
        }

        if(Scoreboard==null)
        {
            Scoreboard = GameObject.Find("Scoreboard");
        }

        if(Manager==null)
        {
            Manager = GameObject.FindObjectOfType<Game_Manager>();
        }

        Show_Scoreboard();

        if (hasAuthority)
        {
            Local_Player = this;
        }
            //Debug.Log(Pause_Menu.Activate);

        if(Team==0)
        {
            Player = Allies;
        }

        else
        {
            Player = Axis;
        }

        //this.name = Manager.Name;
        //PlayerName = this.name;
        this.name = PlayerName;
    }

    void Show_Scoreboard()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            Scoreboard.SetActive(true);
        }
        else if(Input.GetKeyUp(KeyCode.Tab))
        {
            Scoreboard.SetActive(false);
        }
    }

    /// <summary>
    /// Spawns the Player object on the server
    /// </summary>
    [Command]
    public void CmdSpawnPlayer(int i)
    {
        if (isServer == false)
        {
            Debug.Log("Is not the server");
            return;
        }

        if (My_Airplane!=null)
        {
            NetworkServer.Destroy(My_Airplane);
        }

        My_Airplane = Instantiate(Player, Manager.Spawn[i].transform.position, Manager.Spawn[i].transform.rotation);

        NetworkServer.SpawnWithClientAuthority(My_Airplane, connectionToClient);
    }

    [Command]
    public void CmdName(string _name)
    {
        PlayerName = _name;
    }

    [Command]
    public void CmdTeam(int _team)
    {
        Team = _team;
    }

    [Command]
    public void CmdDeath(int _death)
    {
        Deaths = _death;
    }

    [Command]
    public void CmdKill(int _kill)
    {
        Kills = _kill;
    }
}
