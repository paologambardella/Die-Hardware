using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveEnemySequence : MonoBehaviour 
{
    [SerializeField] List<WaveEnemyCommand> commands = new List<WaveEnemyCommand>();
    [SerializeField] int loopCount = -1;

    WaveEnemy enemy;

    int currCommandIndex = -1;
    int loopsRemaining;

    public void Setup(WaveEnemy enemy)
    {
        this.enemy = enemy;

        loopsRemaining = loopCount;
    }

    public void Begin()
    {
        if (commands.Count > 0)
        {
            StartCommand(0);
        }
    }

    public bool Tick() //returns true when the sequence is finished
    {
        if (currCommandIndex != -1)
        {
            bool isFinished = commands[currCommandIndex].Tick();

            if (isFinished)
            {
                ++currCommandIndex;

                if (currCommandIndex >= commands.Count)
                {
                    --loopsRemaining;

                    if (loopCount == -1 || loopsRemaining > 0)
                    {
                        currCommandIndex = 0;
                    }
                    else
                    {
                        return true; //we are finished
                    }
                }

                StartCommand(currCommandIndex);
            }
        }

        return false;
    }

    void StartCommand(int commandIndex)
    {
        currCommandIndex = commandIndex;
        commands[currCommandIndex].Begin(enemy);
    }
}
