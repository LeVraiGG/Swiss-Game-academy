using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    public float playerVelocity = 10;
    public Transform jumpPosition;
    public float raycastRadius;
    public LayerMask mask;
    public float jumpForce = 10;



    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();

    }

    

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 velocity = new Vector2(horizontalInput * playerVelocity, rigidBody2D.velocity.y);
        rigidBody2D.velocity = velocity;
        Vector3 scale = transform.localScale;
        if(velocity.x > 0)
        {
            scale.x = -Mathf.Abs(scale.x);
        }
        else if(velocity.x < 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

        bool canJump = Physics2D.OverlapCircle(jumpPosition.position, raycastRadius, mask);
        if (canJump && Input.GetButtonDown("Jump"))
        {
            rigidBody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Garbage")
        {
            collision.gameObject.SetActive(false);
        }
    }

}
