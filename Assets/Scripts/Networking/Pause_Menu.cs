using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour
{
    [Tooltip("The canvas for the pause menu")]
    public GameObject PauseMenu;

    public Player_Data Player_Data;

    public GameObject Main_Camera;
    public GameObject Spawn_Camera;

    /// <summary>
    /// Checks if the menu is active
    /// </summary>
    public int Activate;
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
        //PauseMenu.SetActive(false);
        //Activate = 0;
        Pause_Menu_Off(true);

        //Main_Camera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Player_Data==null)
        {
            if (Player_Data.Local_Player != null)
            {
                Player_Data = GameObject.FindObjectOfType<Player_Data>();
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Activate == 0)
            {
                Pause_Menu_On();
            }
            else
            {
                Pause_Menu_Off(false);
            }
        }
    }

    public void Pause_Menu_On()
    {
        PauseMenu.SetActive(true);
        Activate = 1;
        Cursor.visible = true;

        Main_Camera.SetActive(false);
        Spawn_Camera.SetActive(true);
    }

    void Pause_Menu_Off(bool cursor)
    {
        PauseMenu.SetActive(false);
        Activate = 0;
        Cursor.visible = cursor;

        Main_Camera.SetActive(true);
        Spawn_Camera.SetActive(false);
    }

    /// <summary>
    /// The Exit button
    /// </summary>
    public void Exit_Menu()
    {
        SceneManager.LoadScene(0);
    }

    //Respawn button
    public void Allies_0()
    {
        Battle(0);
    }

    public void Allies_1()
    {
        Battle(1);
    }

    public void Allies_2()
    {
        Battle(2);
    }

    public void Axis_3()
    {
        Battle(3);
    }

    public void Axis_4()
    {
        Battle(4);
    }

    public void Axis_5()
    {
        Battle(5);
    }

    void Battle(int i)
    {
        Spawn_Camera.SetActive(false);
        Main_Camera.SetActive(true);

        Player_Data.CmdSpawnPlayer(i);

        Pause_Menu_Off(false);
    }
}
