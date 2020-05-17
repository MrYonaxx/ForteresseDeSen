using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class MinigameButtonMashEnemy : MonoBehaviour
{
    [SerializeField]
    int hp;
    [SerializeField]
    Animator animatorEnemy;

    [SerializeField]
    TextMeshPro textMeshPro;
    [SerializeField]
    Animator textAnimator;
    [SerializeField]
    GameObject hitAnim;
    [SerializeField]
    UnityEvent OnDead;

    int hitIndex = 0;
    bool canHit = true;
    bool dead = false;


    public void Active()
    {
        StartCoroutine(PatternCoroutine());
    }

    private IEnumerator PatternCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            canHit = true;
            textMeshPro.text = "3";
            textAnimator.SetTrigger("Feedback");
            yield return new WaitForSeconds(2f);
            textMeshPro.text = "2";
            textAnimator.SetTrigger("Feedback");
            yield return new WaitForSeconds(2f);
            textMeshPro.text = "1";
            textAnimator.SetTrigger("Feedback");
            canHit = false;
            yield return new WaitForSeconds(2f);
            animatorEnemy.SetTrigger("Attack");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dead == true)
            return;
        if(collision.tag == "Interaction")
        {
            hp -= 1;
            Destroy(collision.gameObject);
            var hit = Instantiate(hitAnim, this.transform);
            hit.SetActive(true);
            hit.transform.localPosition = new Vector3(0 + Random.Range(-0.2f, 0.2f), 0 + Random.Range(-0.2f, 0.2f), 0);
            if (canHit == true)
            {
                hitIndex += 1;
                if (hitIndex >= 2)
                    hitIndex = 0;
                animatorEnemy.SetTrigger("Hit");
                animatorEnemy.SetInteger("HitAnimation", hitIndex);
            }
            if(hp <= 0)
            {
                animatorEnemy.SetTrigger("Dead");
                StopAllCoroutines();
                dead = true;
                OnDead.Invoke();
            }
            //hitAnim
        }
        // Projectile Hit
    }

}
