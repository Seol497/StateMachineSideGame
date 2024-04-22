using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetecter : MonoBehaviour
{
    Enemy enemy;
    Player player;
    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !enemy.isDead && !player.isDead)
        {
            enemy.isAttackAlready = true;
        }
        if (player.isDead)
            enemy.isAttackAlready = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !enemy.isDead)
        {
            enemy.isAttackAlready = false;
        }
    }
}
