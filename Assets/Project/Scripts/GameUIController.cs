using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUIController : MonoBehaviour 
{
    [SerializeField] Text scoreText;
    [SerializeField] GameObject deathUI;

    [SerializeField] GameObject slideToStart;

    bool hasStarted = false;

    void Update()
    {
        if (!hasStarted && GameController.instance.IsGameStarted())
        {
            hasStarted = true;
            slideToStart.SetActive(false);
        }

        float score = GameController.instance.GetScore();
        int scoreInt = Mathf.FloorToInt(score);
        scoreText.text = scoreInt.ToString();

        if (!GameController.instance.player.IsAlive())
        {
            deathUI.SetActive(true);
        }
    }
}
