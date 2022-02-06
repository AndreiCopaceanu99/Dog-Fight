using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    [Tooltip("The canvas for the tutorial menu")]
    public GameObject Tutorial_Menu;

    // Start is called before the first frame update
    void Start()
    {
        Tutorial_Menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// The Exit button
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Start button
    /// </summary>
    public void Battle()
    {
        SceneManager.LoadScene("Battle");
    }

    public void Tutorial()
    {
        Tutorial_Menu.SetActive(true);
    }

    public void Movement()
    {
        SceneManager.LoadScene("Tutorial_Movement");
    }

    public void Shooting()
    {
        SceneManager.LoadScene("Tutorial_Shooting");
    }

    public void Axis()
    {
        SceneManager.LoadScene("Tutorial_Axis");
    }

    public void Back()
    {
        Tutorial_Menu.SetActive(false);
    }

}
