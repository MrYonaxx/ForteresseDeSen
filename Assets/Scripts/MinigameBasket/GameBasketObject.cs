using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBasketObject : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rigidbody;
    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    float speedX = 10;
    [SerializeField]
    float speedY = 10;


    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void CreateObject(Sprite objectSprite)
    {
        spriteRenderer.sprite = objectSprite;
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = new Vector2(speedX, speedY);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
            speedX = -speedX;
        else if (collision.gameObject.tag == "Ground")
            Destroy(this.gameObject);
    }

}
