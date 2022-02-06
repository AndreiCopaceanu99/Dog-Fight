using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial_Manager : MonoBehaviour
{
    public Tutorial_Player_Movement Player;

    public int Points;
    public int Points_Goal;

    public Text Points_Text;

    public Text Time_Text;

    public Slider Slider;

    float time;
    public float Time_Goal;

    public GameObject Lose_Menu;

    public GameObject Tutorial;
    public List<GameObject> text = new List<GameObject>();
    int Text_Number=0;
    int Texts=0;

    public string Next_Level;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        Cursor.visible = true;

        Instantiate(Player, new Vector3(60846, 30494, -763), Quaternion.Euler(0,-90,0));

        Points = 0;

        time = 0;

        Lose_Menu.SetActive(false);
        Tutorial.SetActive(true);

        Player = GameObject.FindObjectOfType<Tutorial_Player_Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        Time_Text.text = "Time: " + time.ToString("0.##");
        Points_Text.text = "Ponts: " + Points.ToString();

        Gun_Jammed();

        if (Points==Points_Goal)
        {
            Win();
        }

        if(time>Time_Goal)
        {
            Lose();
        }

        foreach (GameObject t in text)
        {
            if (text[Text_Number] == t)
            {
                t.SetActive(true);
            }
            else
            {
                t.SetActive(false);
            }
        }
    }

    public void Text()
    {
        if(Text_Number==text.Count-1)
        {
            Tutorial.SetActive(false);
            Time.timeScale = 1;
            Cursor.visible = false;
        }
        else
        {
            Text_Number++;
        }
    }

    void Gun_Jammed()
    {
        Slider.value = Player.Gun_Jammed;
    }

    void Win()
    {
        SceneManager.LoadScene(Next_Level);
    }

    void Lose()
    {
        Lose_Menu.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
    }
}
