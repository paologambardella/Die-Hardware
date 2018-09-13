using UnityEngine;
using System.Collections;

public class MovesWithPlayer : MonoBehaviour 
{
    void Update()
    {
        Vector3 playerPos = GameController.instance.player.transform.position;
        playerPos.x = 0f;
        playerPos.y = 0f;
        this.transform.position = playerPos;
    }
}
