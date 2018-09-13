using UnityEngine;
using System.Collections;

public class InputReceiverMonoBehaviour : MonoBehaviour, InputReceiver
{
    [SerializeField] int inputPriority = 0;

    virtual public int GetPriority()
    {
        return inputPriority;
    }

    virtual public bool TouchBegan(Vector3 touchPosition)
    {
        return false;
    }

    virtual public bool TouchUpdated(Vector3 touchPosition)
    {
        return true;
    }

    virtual public bool TouchEnded(Vector3 touchPosition)
    {
        return true;
    }

    virtual protected void OnEnable()
    {
        InputManager.AddReceiver(this);
    }

    virtual protected void OnDisable()
    {
        InputManager.RemoveReceiver(this);
    }
}
