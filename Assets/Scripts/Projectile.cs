using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Projectile : MonoBehaviour {
        private Rigidbody2D rigidBody2D;
    public float projectileVelocity = -2;
    // Start is called before the first frame update
    void Start () {
        rigidBody2D = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update () {
         Vector2 velocity = new Vector2 (projectileVelocity, rigidBody2D.velocity.y);
        rigidBody2D.velocity = velocity;
    }
    private void OnTriggerEnter2D (Collider2D collision) {
        Destroy (gameObject);
    }
}