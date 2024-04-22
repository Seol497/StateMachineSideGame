using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerSkill : MonoBehaviour
{

    GameObject playerpos;
    Player player;
    private float duration = 0.5f;
    Animator anim;

    private void Awake()
    {
        playerpos = GameObject.FindGameObjectWithTag("Player");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
        Vector2 pos = new Vector2(playerpos.transform.position.x, playerpos.transform.position.y + 1.58f);
        transform.position = pos;
        StartCoroutine(ScaleOverTime(1));
    }

    private void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 0.7f)
        {
            StartCoroutine(ScaleOverTime(0));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player != null)
            {
                player.GetDamage(150);
            }
        }
    }

    IEnumerator ScaleOverTime(float num)
    {
        float timer = 0f;
        Vector3 startScale = transform.localScale;
        Vector3 endScale = new Vector3(num, num, num);
        while (timer < duration)
        {
            Vector3 scaledSize = Vector3.Lerp(startScale, endScale, timer / duration);
            transform.localScale = scaledSize;
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localScale = endScale;
        if (num == 0)
        {
            Destroy(gameObject);
        }
    }
}
