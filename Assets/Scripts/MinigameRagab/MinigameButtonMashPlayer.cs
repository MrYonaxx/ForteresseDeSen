using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameButtonMashPlayer : MonoBehaviour
{
    [SerializeField]
    Animator animatorPlayer;
    [SerializeField]
    float timeBeforeProtection = 0.3f;
    [SerializeField]
    Transform muzzleFlash;
    [SerializeField]
    GameObject projectilePrefab;

    bool canPlay = false;
    bool isProtected = false;
    private IEnumerator coroutine;

    public void Active()
    {
        canPlay = true;
    }

    public void OnInteraction()
    {
        if (canPlay == true)
        {
            animatorPlayer.SetTrigger("Feedback");
            var projectile = Instantiate(projectilePrefab, muzzleFlash);
            projectile.SetActive(true);
            projectile.transform.localPosition = Vector3.zero;
            isProtected = false;
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = ProtectionCoroutine();
            StartCoroutine(coroutine);
        }

    }
    private IEnumerator ProtectionCoroutine()
    {
        yield return new WaitForSeconds(timeBeforeProtection);
        isProtected = true;
    }

    public void TakeHit()
    {
        if (canPlay == true)
        {
            if (isProtected == true)
            {
                animatorPlayer.SetTrigger("Defend");
                return;
            }
            else
            {
                animatorPlayer.SetTrigger("Dead");
                canPlay = false;
            }
        }
    }
}
