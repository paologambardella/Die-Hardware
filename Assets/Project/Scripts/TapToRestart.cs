using UnityEngine;
using System.Collections;

public class TapToRestart : InputReceiverMonoBehaviour 
{
    public override bool TouchBegan(Vector3 touchPosition)
    {
        if (GameController.instance.player.IsAlive())
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public override bool TouchEnded(Vector3 touchPosition)
    {
        GameController.instance.RestartLevel();

        return true;
    }
}
