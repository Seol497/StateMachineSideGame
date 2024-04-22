using UnityEngine;

public class PlayerDetecter : MonoBehaviour
{
    public Enemy enemy;
    public EnemyState state;
    Player player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !enemy.isSleep && !enemy.isDead && !player.isDead)
        {
            enemy.playerPos = collision.transform;
            enemy.isPlayerDetected = true;
            Vector2 targetPosition = collision.transform.position;
            Vector2 currentPosition = transform.position;
            Vector2 distanceVector = targetPosition - currentPosition;
            float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
            Quaternion newRotation = Quaternion.Euler(0f, 0f, angle);
            transform.rotation = newRotation;
            if(distanceVector.x > 0)
                enemy.angle = 1;
            else if(distanceVector.x < 0)
                enemy.angle = -1;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy.isPlayerDetected = false;
            transform.rotation = enemy.transform.rotation;

        }
    }
}
