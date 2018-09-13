using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour 
{
    public static InputManager instance;

    public static void AddReceiver(InputReceiver receiver)
    {
        instance.receivers.Add(receiver);
        instance.SortReceivers();
    }

    public static void RemoveReceiver(InputReceiver receiver)
    {
        instance.receivers.Remove(receiver);
    }

    List<InputReceiver> receivers = new List<InputReceiver>();

    InputReceiver current = null;
    InputReceiverComparer inputReceiverComparer = new InputReceiverComparer();

    void SortReceivers()
    {
        receivers.Sort(inputReceiverComparer);
    }

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < receivers.Count; ++i)
            {
                if (receivers[i].TouchBegan(Input.mousePosition))
                {
                    current = receivers[i];
                    break;
                }
            }
        }
        else if (current != null)
        {
            if (Input.GetMouseButton(0))
            {
                current.TouchUpdated(Input.mousePosition);
            }
            else
            {
                current.TouchEnded(Input.mousePosition);
                current = null;
            }
        }
    }

    class InputReceiverComparer : IComparer<InputReceiver>
    {
        public int Compare(InputReceiver a, InputReceiver b)
        {
            return a.GetPriority().CompareTo(b.GetPriority());
        }
    }
}
