using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour 
{
    public static GameController instance;

    public PlayerController player;
    public Camera playerCamera;
    public Transform playerCameraRoot;

    public Camera gameUICamera;

    public WorldManager worldManager;

    bool isGameStarted = false;

    int score = 0;

    public bool IsGameStarted()
    {
        return isGameStarted;
    }

    public void StartGame()
    {
        isGameStarted = true;
    }

    public float GetScore()
    {
        return score;
    }

    public void EnemyKilled(Enemy enemy)
    {
        ++score;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Awake()
    {
        instance = this;
    }
}
