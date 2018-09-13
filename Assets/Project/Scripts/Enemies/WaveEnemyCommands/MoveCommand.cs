using UnityEngine;
using System.Collections;

public class MoveCommand : WaveEnemyCommand 
{
    [Tooltip("The Z value is relative to 'distance ahead of player'")]
    [SerializeField] Vector3 moveToPosition;
    [SerializeField] float duration = 1f;
    [SerializeField] EasingType easing = EasingType.Cubic;

    float timer;
    Vector3 startPosition;
    WaveEnemy enemy;

    protected override void Setup(WaveEnemy enemy)
    {
        base.Setup(enemy);

        this.enemy = enemy;

        timer = 0f;
        startPosition = enemy.GetPlayerRelativePosition();
    }

    protected override bool DoUpdate()
    {
        timer += Time.deltaTime;

        float eased = Easing.EaseInOut(Mathf.Clamp01(timer / duration), easing);

        Vector3 pos = Vector3.Lerp(startPosition, moveToPosition, eased);

        enemy.SetPlayerRelativePosition(pos);

        return timer >= duration;
    }
}
