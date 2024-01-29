using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandUI : MonoBehaviour
{
    public TextMeshProUGUI text;

    public PlayerShip player;

    void Start()
    {
        player = FindObjectOfType<PlayerShip>();
    }

    void Update()
    {
        text.text = "Score:\n" + GameManager.score + "\nHealth:\n" + Mathf.FloorToInt(player.health);
    }
}
