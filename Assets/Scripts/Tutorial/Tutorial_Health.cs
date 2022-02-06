using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Health : MonoBehaviour
{
    float Hit_Points;

    public int HP;

    Tutorial_Pause_Menu Menu;

    public GameObject Explosion;

    // Start is called before the first frame update
    void Start()
    { 
        Hit_Points = HP;
    }

    // Update is called once per frame
    void Update()
    {
        if(Menu==null)
        {
            Menu= GameObject.FindObjectOfType<Tutorial_Pause_Menu>();
        }
    }

    public void ChangeHealth(float amount)
    {
        Hit_Points -= amount;
        if (Hit_Points <= 0)
        {
            Die();
        }
        if(gameObject.name == "Handley Page Halifax")
        {
            Debug.Log(Hit_Points);
        }
    }

    void Die()
    {

        //try
        //{
        //    Tutorial_Player_Movement Player = GetComponent<Tutorial_Player_Movement>();
        //    Instantiate(Player.Explosion, gameObject.transform.position, Quaternion.identity);
        //}
        //catch
        //{
        //    Debug.Log("Is not player");
        //}
        
        Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
        if(gameObject.tag == "Allies" || gameObject.tag == "Axis")
        {
            Menu.Pause_Menu_On();
        }
        if(gameObject.tag=="Bomber")
        {
            Tutorial_Manager Points=GameObject.FindObjectOfType<Tutorial_Manager>();
            Points.Points += 40;
        }
    }

    public float Get_hit_Points()
    {
        return Hit_Points;
    }
}
