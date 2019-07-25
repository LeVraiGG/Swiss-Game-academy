﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rigidBody2D;
    public float playerVelocity = 10;
    public Transform jumpPosition;
    public float raycastRadius;
    public LayerMask mask;
    public float jumpForce = 10;

    public static int nextInt = 2;
    public string nextScene = "";
    public GameObject spawn;
    public GameObject Player;
    public GameObject treePrefab;

    public int numLeaves;

    void Start () {
        rigidBody2D = GetComponent<Rigidbody2D> ();
    }

    void Update () {
        float horizontalInput = Input.GetAxis ("Horizontal");
        bool canJump = Physics2D.OverlapCircle(jumpPosition.position, raycastRadius, mask);

        Vector2 velocity = new Vector2 (horizontalInput * playerVelocity, rigidBody2D.velocity.y);
        if (!canJump)
        {
            Debug.Log("IN AIR");
            velocity.x = velocity.x / Mathf.Sqrt(2);
        }
        rigidBody2D.velocity = velocity;

        Vector3 scale = transform.localScale;
        if (velocity.x > 0) {
            scale.x = -Mathf.Abs (scale.x);
        } else if (velocity.x < 0) {
            scale.x = Mathf.Abs (scale.x);
        }
        transform.localScale = scale;

        
        if (canJump && Input.GetButtonDown ("Jump")) {
            rigidBody2D.AddForce (new Vector2 (0, jumpForce), ForceMode2D.Impulse);
        }

  /*       if (Input.GetButton ("Bouton B")) {
            var deadSlime = GameObject.FindWithTag ("deadSlime");
            Player = GameObject.FindWithTag ("Player");
            spawn = GameObject.FindWithTag ("spawn");

            Instantiate (deadSlime, Player.transform.position, Quaternion.identity);
            Player.transform.position = spawn.transform.position;
        }*/
    }
    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.tag == "Door") {
            nextScene = nextInt.ToString ();
            SceneManager.LoadScene (nextScene);
            nextInt++;
            nextScene = nextInt.ToString ();
        }

        if (collision.tag == "Spike") {
            Player = GameObject.FindWithTag ("Player");
            spawn = GameObject.FindWithTag ("spawn");

            Player.transform.position = spawn.transform.position;
        }
    }
}