using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDamage : MonoBehaviour
{
    Player player;
    Enemy enemy;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy = gameObject.GetComponent<Enemy>();
            Debug.Log("damage");
        }
        else if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (enemy.isSleep == true)
                {
                    enemy.isSleep = false;
                    enemy.angle *= -1;
                }
                enemy.stateMachine.ChangeState(enemy.hitState);
            }
        }
    }
}
