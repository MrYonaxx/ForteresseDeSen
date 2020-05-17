using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamePaperObject : MonoBehaviour
{

    [SerializeField]
    Rigidbody rigidbody;
    [SerializeField]
    BoxCollider boxCollider;
    [SerializeField]
    UnityEventInt OnEventValidate;

    Transform defaultTransform;

    int playerID = -1;

    public void SetDefaultTransform(Transform parent)
    {
        defaultTransform = parent;
    }

    public void HoldObject(Transform parent, int player)
    {
        playerID = player;
        this.transform.SetParent(parent);
        rigidbody.isKinematic = true;
        this.transform.localPosition = Vector3.zero;
    }

    public void ReleaseObject(Vector3 releaseForce)
    {
        rigidbody.isKinematic = false;
        rigidbody.AddForce(releaseForce * 50f);
        this.transform.SetParent(defaultTransform);

    }

    public float GetAngle()
    {
        return this.transform.eulerAngles.y;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.tag == "Interaction")
        {
            boxCollider.enabled = false;
            this.transform.SetParent(collision.transform);
            OnEventValidate.Invoke(playerID);
            StartCoroutine(MovePaperCoroutine());
        }   
    }

    private IEnumerator MovePaperCoroutine()
    {
        rigidbody.isKinematic = true;
        float t = 0f;
        Vector3 initialPosition = this.transform.localPosition;
        Vector3 initialRotation = this.transform.localEulerAngles;
        while (t<1f)
        {
            t += Time.deltaTime;
            this.transform.localPosition = Vector3.Lerp(initialPosition, Vector3.zero, t);
            this.transform.localEulerAngles = Vector3.Lerp(initialRotation, Vector3.zero, t);
            yield return null;
        }
    }

}
