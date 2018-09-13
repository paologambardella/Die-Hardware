using UnityEngine;
using System.Collections;

abstract public class WaveEnemyCommand : MonoBehaviour 
{
    [SerializeField] float waitBeforeSeconds = 0f;
    [SerializeField] float waitAfterSeconds = 0f;

    enum Stage
    {
        WaitBefore,
        Running,
        WaitAfter,
        Complete,
    }

    float waitBeforeTimeRemaining;
    float waitAfterTimeRemaining;
    Stage stage;

    public void Begin(WaveEnemy enemy)
    {
        stage = Stage.WaitBefore;
        waitBeforeTimeRemaining = waitBeforeSeconds;
        waitAfterTimeRemaining = waitAfterSeconds;

        Setup(enemy);
    }

    public bool Tick() //return true when complete
    {
        if (stage == Stage.WaitBefore)
        {
            waitBeforeTimeRemaining -= Time.deltaTime;

            if (waitBeforeTimeRemaining < 0)
            {
                stage = Stage.Running;
            }
        }

        if (stage == Stage.Running)
        {
            bool isFinished = DoUpdate();

            if (isFinished)
            {
                stage = Stage.WaitAfter;
            }
        }

        if (stage == Stage.WaitAfter)
        {
            waitAfterTimeRemaining -= Time.deltaTime;

            if (waitAfterTimeRemaining < 0f)
            {
                stage = Stage.Complete;
            }
        }

        return stage == Stage.Complete;
    }

    virtual protected void Setup(WaveEnemy enemy) { }

    abstract protected bool DoUpdate(); //return true when the command is complete
}
