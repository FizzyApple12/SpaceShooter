using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    public PlayerShip player;

    public void Start()
    {
        player = FindObjectOfType<PlayerShip>();
    }

    void Update()
    {
        transform.position = new Vector3(3.22f * Mathf.Cos(Time.time / 15), 2.35f, 3.22f * Mathf.Sin(Time.time / 15));

        transform.LookAt(new Vector3(0, 0.25f, 0));
    }
}
