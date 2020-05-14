using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamePaperObject : MonoBehaviour
{
    /*[SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    SpriteRenderer shadowSpriteRenderer;
    [SerializeField]
    SpriteRenderer[] debugSpriteRenderer;*/
    [SerializeField]
    Rigidbody rigidbody;
    [SerializeField]
    BoxCollider boxCollider;

    public void HoldObject(Transform parent)
    {
        //SetOrderShadow(100);
        this.transform.SetParent(parent);
        rigidbody.isKinematic = true;
        this.transform.localPosition = Vector3.zero;
    }

    public void ReleaseObject(Vector3 releaseForce)
    {
        //SetOrderShadow(0);
        rigidbody.isKinematic = false;
        rigidbody.AddForce(releaseForce * 20f);
        this.transform.SetParent(null);

    }

    public float GetAngle()
    {
        return this.transform.eulerAngles.y;
    }

    public void SetOrderShadow(int newOrder)
    {
        /*shadowSpriteRenderer.sortingOrder = newOrder-2;
        spriteRenderer.sortingOrder = newOrder-1;
        for(int i = 0; i< debugSpriteRenderer.Length; i++)
        {
            debugSpriteRenderer[i].sortingOrder = newOrder;
        }*/
    }
}
