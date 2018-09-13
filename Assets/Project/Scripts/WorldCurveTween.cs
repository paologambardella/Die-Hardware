using UnityEngine;
using System.Collections;
using VacuumShaders.CurvedWorld;

public class WorldCurveTween : MonoBehaviour 
{
    [SerializeField] CurvedWorld_Controller curvedWorld;

    [SerializeField] float minTurnAmount = -1.5f;
    [SerializeField] float maxTurnAmount = 1.5f;
    [SerializeField] float minSecondsBetweenTurns = 5f;
    [SerializeField] float maxSecondsBetweenTurns = 10f;
    [SerializeField] float minSecondsToAttackTurn = 2f;
    [SerializeField] float maxSecondsToAttackTurn = 10f;
    [SerializeField] float minSecondsToHoldTurn = 0f;
    [SerializeField] float maxSecondsToHoldTurn = 10f;
    [SerializeField] float minSecondsToReleaseTurn = 2f;
    [SerializeField] float maxSecondsToReleaseTurn = 10f;

    [SerializeField] float minCurvature = -8f;
    [SerializeField] float maxCurvature = 8f;
    [SerializeField] float minSecondsBetweenCurvature = 0f;
    [SerializeField] float maxSecondsBetweenCurvature = 10f;
    [SerializeField] float minSecondsAttackCurvature = 3f;
    [SerializeField] float maxSecondsAttackCurvature = 10f;

    Vector3 bend;

    PasrTimer turnPasr;
    float turnTarget;

    bool curving = false;
    float curveTimer;
    float curveStart;
    float targetCurve;
    float curveDuration;

    void Start()
    {
        bend = curvedWorld.GetBend();

        SetupNextTurn();
        curveTimer = Random.Range(minSecondsBetweenCurvature, maxSecondsBetweenCurvature);
    }

    void Update()
    {
        if (GameController.instance.player.IsAlive())
        {
            UpdateTurning();
            UpdateEarthCurvature();
            UpdateZWhatsThat();

            UpdateBend();
        }

        GameController.instance.player.SetWorldTurnAmount(bend.y);
    }

    void UpdateTurning()
    {
        float turnFactor = turnPasr.Tick(Time.deltaTime);
        float turnAmount = Easing.EaseInOut(turnFactor, EasingType.Cubic);
        bend.y = turnAmount;

        if (turnPasr.isFinished())
        {
            SetupNextTurn();
        }
    }

    void SetupNextTurn()
    {
        turnPasr = new PasrTimer(Random.Range(minSecondsBetweenTurns, maxSecondsBetweenTurns), 
            Random.Range(minSecondsToAttackTurn, maxSecondsToAttackTurn), 
            Random.Range(minSecondsToHoldTurn, maxSecondsToHoldTurn), 
            Random.Range(minSecondsToReleaseTurn, maxSecondsToReleaseTurn));
        turnPasr.Reset();
        turnTarget = Random.Range(minTurnAmount, maxTurnAmount);
    }

    void UpdateEarthCurvature()
    {
        if (curving)
        {
            curveTimer += Time.deltaTime;

            if (curveTimer > curveDuration)
            {
                curveTimer = Random.Range(minSecondsBetweenCurvature, maxSecondsBetweenCurvature);
                curving = false;
                bend.x = targetCurve;
            }
            else
            {
                float curveDelta = Mathf.Clamp01(curveTimer / curveDuration);
                bend.x = Mathf.Lerp(curveStart, targetCurve, Easing.EaseInOut(curveDelta, EasingType.Cubic));
            }
        }
        else
        {
            curveTimer -= Time.deltaTime;

            if (curveTimer < 0f)
            {
                curving = true;
                curveStart = bend.x;
                curveDuration = Random.Range(minSecondsAttackCurvature, maxSecondsAttackCurvature);
                curveTimer = 0f;
                targetCurve = Random.Range(minCurvature, maxCurvature);
            }
        }
    }

    void UpdateZWhatsThat()
    {
        
    }

    void UpdateBend()
    {
        curvedWorld.SetBend(bend);
        curvedWorld.ForceUpdate();
    }
}
