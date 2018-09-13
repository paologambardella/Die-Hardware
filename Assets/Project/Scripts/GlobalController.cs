using UnityEngine;
using System.Collections;

public class GlobalController : MonoBehaviour 
{
    public static GlobalController instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            GameObject.DestroyImmediate(this.gameObject);
        }
    }
}
