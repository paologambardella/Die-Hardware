using UnityEngine;
using System.Collections;

public interface InputReceiver 
{
    int GetPriority();
    bool TouchBegan(Vector3 touchPosition);
    bool TouchUpdated(Vector3 touchPosition);
    bool TouchEnded(Vector3 touchPosition);
}
