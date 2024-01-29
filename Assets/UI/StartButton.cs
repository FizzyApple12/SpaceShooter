using Assets.Vehicles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : Vehicle
{
    public override float health { get; set; } = 100;

    public override float pointValue => 0;

    Transform parent;

    Material buttonMaterial;

    public void Start()
    {
        parent = transform.parent;

        buttonMaterial = GetComponent<Renderer>().material;

        buttonMaterial.EnableKeyword("_EMISSION");
    }

    public void Update()
    {
        Vector3 targetPosition = GameManager.gameStarted ? new Vector3(0, -1, 2.15f) : new Vector3(0, 0.4f, 2.15f);

        this.parent.position = Vector3.Lerp(parent.position, targetPosition, Time.deltaTime);

        Color buttonColor = Color.red;

        buttonColor.g = 1.0f - (health / 100.0f);

        if (GameManager.gameStarted)
        {
            buttonColor = Color.green;
        }

        buttonMaterial.SetColor("_Color", buttonColor);
        buttonMaterial.SetColor("_EmissionColor", buttonColor);
    }

    public override bool Kill()
    {
        if (!GameManager.gameStarted) GameManager.StartGame();

        health = 100.0f;

        return false;
    }
}
