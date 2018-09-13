using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerGun gun;

    [SerializeField] float playerLerpSpeed = 3f;
    [SerializeField] float playerSmoothDampTime = 0.2f;

    [SerializeField] float minX = -8f;
    [SerializeField] float maxX = 8f;

    [SerializeField] float speed = 5f;

    [SerializeField] Vector3 colliderSize = new Vector3(1f, 1f, 1f);

    [SerializeField] bool isInvincible = false;

    [SerializeField] Transform geometryRoot;
    [SerializeField] float worldTurnFactor = 30f;
    [SerializeField] float playerTurnFactor = 10f;

    float distanceTravelled = 0f;

    bool isAlive = true;

    float playerSmoothDampVelocity;
    Vector3 targetPosition = Vector3.zero;

    float worldTurnSpeed = 0f;
    float playerTurnSpeed = 0f;

    public void SetWorldTurnAmount(float amount)
    {
        worldTurnSpeed = amount;
    }

    public float GetDistanceTravelled()
    {
        return distanceTravelled;
    }

    public void SetShooting(bool shooting)
    {
        gun.SetShooting(shooting && isAlive);
    }

    public Vector3 GetTargetPosition()
    {
        return targetPosition;
    }

    public void SetHorizontalRelativePosition(float relativeX) //takes an x value scaled between 0 and 1 to match between minX and maxX
    {
//        Debug.Log("Seting horizontal: " + x01);
//        Debug.Log("Setting xval: " + x01 + " " + minX + " " + maxX + " " + Mathf.Lerp(minX, maxX, x01));
        targetPosition.x = maxX * relativeX;
    }

    public Vector3 GetSize()
    {
        return colliderSize;
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }

    public void Kill()
    {
        if (isAlive)
        {
            isAlive = false;
            SetShooting(false);
            Transform explosion = ObjectPooler.instance.GetRandomItem("Explosions");
            explosion.transform.position = this.transform.position;
            this.transform.forward = (Vector3.forward + Vector3.left * Random.Range(-1f, 1f)).normalized;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(this.transform.position, colliderSize);
    }

    void Update()
    {
        if (isAlive)
        {
            Vector3 playerPos = this.transform.position;

            float moveZDistance = speed * Time.deltaTime;

            playerPos.x = Mathf.SmoothDamp(playerPos.x, targetPosition.x, ref playerSmoothDampVelocity, playerSmoothDampTime);
            playerPos.x = Mathf.Lerp(playerPos.x, targetPosition.x, Time.deltaTime * playerLerpSpeed);
            playerPos.z += moveZDistance;

            distanceTravelled += moveZDistance;

            this.transform.position = playerPos;

            Vector3 rot = geometryRoot.localRotation.eulerAngles;
            rot.z = worldTurnSpeed * worldTurnFactor;
            rot.z += (playerPos.x - targetPosition.x) * playerTurnFactor;

            geometryRoot.localRotation = Quaternion.Euler(rot);
        }
    }
}
