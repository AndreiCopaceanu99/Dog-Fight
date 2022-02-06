using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Linq;

public class Game_Manager : NetworkBehaviour
{
    [SyncVar]
    public int Allies_Left = 100;
    [SyncVar]
    public int Axis_Left = 100;

    public int Team;

    public Text Allies;
    public Text Axis;
    public Text Health_Text;
    public Text Waiting_Time;
    public GameObject Wait;

    public GameObject UI;
    public GameObject Select_Team;

    public Pause_Menu Pause_Menu;

    public Slider Slider;

    Health Health;
    public Player_Movement Player;
    public Player_Data Player_Data;

    public GameObject Allies_Win;
    public GameObject Axis_Win;

    public GameObject Allies_Spawn;
    public GameObject Axis_Spawn;

    public GameObject[] Spawn;

    public GameObject Bomber;

    public string Name;
    public InputField IField;

    [SyncVar]
    float Start_Time=90;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

        Instantiate(Bomber, new Vector3(66726, 14019, 0), Quaternion.identity);
        UI.SetActive(false);

        Axis_Spawn.SetActive(false);
        Allies_Spawn.SetActive(false);

       // InvokeRepeating("Wait_To_Start", 0f, 0.5f);

        //StartCoroutine(Wait_Start(Start_Time));
        //Call_Wait();

        Time.timeScale = 1;

        Allies_Win.SetActive(false);
        Axis_Win.SetActive(false);
    }

    private IEnumerator Wait_Start(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Player_Data == null)
       // {
         //   Player_Data = GameObject.FindObjectOfType<Player_Data>();
       // }
        Airplanes_Left();

        Player_Health();

        Gun_Jammed();

        Player_Name(IField.text);
    }

    void Airplanes_Left()
    {
        Allies.text = Allies_Left.ToString();
        Axis.text = Axis_Left.ToString();
    }

    void Player_Health()
    {
        if (Health == null)
        {
            if (Player_Movement.Local_Player != null)
            {
                Health = Player_Movement.Local_Player.GetComponent<Health>();
            }

            if (Health == null)
            {
                Health_Text.text = "0";
                return;
            }
        }
        Health_Text.text = Health.Get_hit_Points().ToString();
    }

    //void Call_Wait()
    //{
      //  InvokeRepeating("Wait_To_Start", 0f, 1f);
    //}

    /*void Wait_To_Start()
    {
        Start_Time--;

        Waiting_Time.text = Start_Time.ToString();

        if (Start_Time < 0)
        {
            Time.timeScale = 1;
            Wait.SetActive(false);
        }
    }*/

    void Gun_Jammed()
    {
        if (Player == null)
        {
            if (Player_Movement.Local_Player != null)
            {
                Player = Player_Movement.Local_Player.GetComponent<Player_Movement>();
            }

            if (Player == null)
            {
                return;
            }
        }

        Slider.value = Player.Gun_Jammed;
    }

    public void GB()
    {
        if (Player_Data.Local_Player!=null)
        {
            Player_Data = Player_Data.Local_Player.GetComponent<Player_Data>();
            //Player_Data.Team = 0;
            Player_Data.CmdTeam(0);
            Select_Team.SetActive(false);
            Allies_Spawn.SetActive(true);
            UI.SetActive(true);
            Pause_Menu.Pause_Menu_On();
        }
    }

    public void Nazi()
    {
        if (Player_Data.Local_Player!=null)
        {
            Player_Data = Player_Data.Local_Player.GetComponent<Player_Data>();
            //Player_Data.Team = 1;
            Player_Data.CmdTeam(1);
            Select_Team.SetActive(false);
            Axis_Spawn.SetActive(true);
            UI.SetActive(true);
            Pause_Menu.Pause_Menu_On();
        }
    }

    public void Player_Name(string _name)
    {

        if (Player_Data.Local_Player != null)
        {
            Player_Data = Player_Data.Local_Player.GetComponent<Player_Data>();
            //Player_Data.PlayerName = _name;
            Player_Data.CmdName(_name);
        }
        //Debug.Log(Name);
    }

    [Command]
    public void CmdAllies(int _allies)
    {
        Allies_Left = _allies;
    }

    [Command]
    public void CmdAxis(int _axis)
    {
        Axis_Left = _axis;
    }
}
