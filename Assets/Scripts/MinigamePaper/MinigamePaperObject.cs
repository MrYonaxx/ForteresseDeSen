using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamePaperObject : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Rigidbody rigidbody;
    [SerializeField]
    BoxCollider boxCollider;

    public void HoldObject(Transform parent)
    {
        this.transform.SetParent(parent);
        rigidbody.isKinematic = true;
        this.transform.localPosition = Vector3.zero;
    }

    public void ReleaseObject(Vector3 releaseForce)
    {
        rigidbody.isKinematic = false;
        rigidbody.AddForce(releaseForce * 20f);
        this.transform.SetParent(null);

    }
}
