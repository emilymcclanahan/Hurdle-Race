using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform Player;

    // Update is called once per frame
    void Update()
    {
        // Camera follows player's x position
        transform.position = new Vector3(Player.position.x + .75f, 1.3f, -10);
    }
}
