using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial_Pause_Menu : MonoBehaviour
{
    [Tooltip("The canvas for the pause menu")]
    public GameObject PauseMenu;

    public string Level;

    /// <summary>
    /// Checks if the menu is active
    /// </summary>
    public int Activate;

    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Activate == 0)
            {
                Pause_Menu_On();
            }
            else
            {
                Pause_Menu_Off();
            }
        }
    }

    public void Pause_Menu_On()
    {
        PauseMenu.SetActive(true);
        Activate = 1;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    void Pause_Menu_Off()
    {
        PauseMenu.SetActive(false);
        Activate = 0;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    public void Exit_Menu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void Respawn()
    {
        SceneManager.LoadScene(Level);
    }
}
