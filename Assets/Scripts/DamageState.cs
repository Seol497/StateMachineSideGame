using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDamage : MonoBehaviour
{
    Player player;
    Enemy enemy;
    public bool isEnemy;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isEnemy)
        {
            enemy = gameObject.transform.parent.GetComponent<Enemy>();
            if (player != null)
            {
                player.GetDamage(enemy.damage);
            }
        }
        else if (collision.CompareTag("Enemy") && !isEnemy)
        {
            enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (enemy.isSleep == true)
                {
                    enemy.isSleep = false;
                    enemy.angle *= -1;
                }
                enemy.GetDamage(player.damage);
            }
        }
    }
}
