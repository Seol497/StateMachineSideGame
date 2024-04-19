using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetecter : MonoBehaviour
{
    public Enemy enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy.isAttackAlready = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy.isAttackAlready = false;
        }
    }
}
