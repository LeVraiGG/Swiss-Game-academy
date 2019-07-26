using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capacity : MonoBehaviour
{
    public float jumpForce;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] PlayerController controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jump()
    {
        rigid.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    public void CanPlay()
    {
        controller.canPlay = true;
    }
}
