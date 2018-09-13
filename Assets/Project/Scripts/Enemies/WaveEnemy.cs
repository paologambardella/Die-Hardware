using UnityEngine;
using System.Collections;

public class WaveEnemy : Enemy 
{
    enum State
    {
        Arriving,
        Attacking,
        Retreating,
    }

    [SerializeField] float arriveTime = 1f;
    [SerializeField] float targetDistanceAheadOfPlayer = 3f;
    [SerializeField] EnemyGun gun;
    [SerializeField] WaveEnemySequence sequence;

    State state;

    //variables to arrive nicely
    float startDistanceAheadOfPlayer;
    float distanceAheadOfPlayer;
    float arriveVelocity;
    float arriveTimer = 0f;

    public void Shoot()
    {
        if (gun != null)
        {
            gun.Shoot();
        }
    }

    public void AttackPlayer()
    {
        state = State.Arriving;

        Vector3 myPos = this.transform.position;
        Vector3 playerPos = GameController.instance.player.transform.position;
        startDistanceAheadOfPlayer = distanceAheadOfPlayer = Mathf.Abs(myPos.z - playerPos.z);
        arriveTimer = 0f;
    }

    public void SetPlayerRelativePosition(Vector3 pos)
    {
        Vector3 myPos = this.transform.position;
        Vector3 playerPos = GameController.instance.player.transform.position;

        distanceAheadOfPlayer = pos.z;

        myPos.x = pos.x;
        myPos.y = pos.y;
        myPos.z = playerPos.z + distanceAheadOfPlayer;

        this.transform.position = myPos;
    }

    public Vector3 GetPlayerRelativePosition()
    {
        Vector3 myPosRelativeToPlayer = this.transform.position;
        myPosRelativeToPlayer.z = distanceAheadOfPlayer;

        return myPosRelativeToPlayer;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        if (sequence != null)
        {
            sequence.Setup(this);
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void Update()
    {
        base.Update();

        if (state == State.Arriving)
        {
            UpdateArriving();
        }
        else if (state == State.Attacking)
        {
            UpdateAttacking();
        }
    }

    void UpdateAttacking()
    {
        Vector3 playerPos = GameController.instance.player.transform.position;
        Vector3 myPos = this.transform.position;
        myPos.z = playerPos.z + distanceAheadOfPlayer;

        this.transform.position = myPos;

        if (sequence != null)
        {
            bool isFinished = sequence.Tick();

            if (isFinished)
            {
                state = State.Retreating;
            }
        }
    }

    void UpdateArriving()
    {
        arriveTimer += Time.deltaTime;

        if (arriveTimer >= arriveTime)
        {
            distanceAheadOfPlayer = targetDistanceAheadOfPlayer;

            state = State.Attacking;

            if (sequence != null)
            {
                sequence.Begin();
            }
        }
        else
        {
            float arrivalDelta = arriveTimer / arriveTime;
            float arriveEased = Easing.EaseOut(arrivalDelta, EasingType.Cubic);

            distanceAheadOfPlayer = Mathf.Lerp(startDistanceAheadOfPlayer, targetDistanceAheadOfPlayer, arriveEased);
        }

        Vector3 playerPos = GameController.instance.player.transform.position;
        Vector3 myPos = this.transform.position;
        myPos.z = playerPos.z + distanceAheadOfPlayer;

        this.transform.position = myPos;
    }

    public override void Kill()
    {
        if (state == State.Arriving)
        {
            //you cannot kill me!
        }
        else
        {
            base.Kill();

//            Debug.Log("Killed wave!");
        }
    }
}
