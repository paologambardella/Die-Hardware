using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnimateUIImage : MonoBehaviour 
{
    [SerializeField] Image image;

    [SerializeField] Sprite[] sprites;
    [SerializeField] float fps = 30f;

    int frameNum;
    float secondsUntilNextFrame = 0;

    void Update()
    {
        if (sprites.Length > 0)
        {
            secondsUntilNextFrame -= Time.deltaTime;

            if (secondsUntilNextFrame < 0f)
            {
                secondsUntilNextFrame += 1f / fps;

                ++frameNum;

                if (frameNum >= sprites.Length) frameNum = 0;

                image.sprite = sprites[frameNum];
            }
        }
    }
}
