using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capacity : MonoBehaviour
{
    public float jumpForce;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] PlayerController controller;
    public GameObject spawn;

    public void Jump()
    {
        rigid.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    public void CanPlay()
    {
        controller.canPlay = true;
    }

    public void Respawn()
    {
        controller.gameObject.transform.position = spawn.transform.position;
    }



    public void Freeze()
    {
        rigid.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void DeFreeze()
    {
        rigid.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
    }




}
