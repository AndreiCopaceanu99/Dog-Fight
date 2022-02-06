using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public ScoreboadUI[] Allies;
    public ScoreboadUI[] Axis;

    Player_Data[] Players;

    int Allies_Place=0;
    int Axis_Place=0;

    private void Start()
    {
        foreach(Player_Data player in Players)
        {
            if(player.Team==0)
            {
                player.Place = Allies_Place;
                Allies_Place++;
            }
            else
            {
                player.Place = Axis_Place;
                Axis_Place++;
            }
        }
    }

    void OnEnable()
    {
        Players = GameObject.FindObjectsOfType<Player_Data>();

        foreach(Player_Data player in Players)
        {
            Change_Places(player);
            Change_UI(player);
        }

    }

    void Change_Places(Player_Data player)
    {
        foreach (Player_Data _player in Players)
        {
            if (player.Team == _player.Team)
            {
                if (player.Kills > _player.Kills && player.Place==_player.Place-1)
                {
                    player.Place++;
                    _player.Place--;
                }
            }
        }
    }

    void Change_UI(Player_Data player)
    {
        if(player.Team==0)
        {
            Allies[player.Place].Username.text = player.PlayerName;
            Allies[player.Place].Kills.text = player.Kills.ToString();
            Allies[player.Place].Deaths.text = player.Deaths.ToString();
        }
        else
        {
            Axis[player.Place].Username.text = player.PlayerName;
            Axis[player.Place].Kills.text = player.Kills.ToString();
            Axis[player.Place].Deaths.text = player.Deaths.ToString();
        }
    }

    void OnDisable()
    {
        // Clean up our list of items
    }
}
