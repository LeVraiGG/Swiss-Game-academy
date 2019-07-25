using System.Collections;
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
    public bool isWait = false;
    public float timerWait = 1;
    public Vector3 positionDeath;

    public int numLeaves;

    void Start () {
        rigidBody2D = GetComponent<Rigidbody2D> ();
        spawn = GameObject.FindWithTag ("spawn");
        Player = GameObject.FindWithTag ("Player");
    }

    void Update () {
        float horizontalInput = Input.GetAxis ("Horizontal");
        Vector2 velocity = new Vector2 (horizontalInput * playerVelocity, rigidBody2D.velocity.y);
        rigidBody2D.velocity = velocity;
        Vector3 scale = transform.localScale;
        if (velocity.x > 0) {
            scale.x = -Mathf.Abs (scale.x);
        } else if (velocity.x < 0) {
            scale.x = Mathf.Abs (scale.x);
        }
        transform.localScale = scale;

        bool canJump = Physics2D.OverlapCircle (jumpPosition.position, raycastRadius, mask);
        if (canJump && Input.GetButtonDown ("Jump")) {
            rigidBody2D.AddForce (new Vector2 (0, jumpForce), ForceMode2D.Impulse);
        }

        if (Input.GetButtonDown ("Fire2")) {
            var deadSlime = GameObject.FindWithTag ("deadSlime");
            Player = GameObject.FindWithTag ("Player");
            spawn = GameObject.FindWithTag ("spawn");

            Instantiate (deadSlime, Player.transform.position, Quaternion.identity);
            Player.transform.position = spawn.transform.position;
        }

        if (isWait) {
            timerWait -= Time.deltaTime;
            Player.transform.position = positionDeath;
            if (timerWait <= 0) {
                isWait = false;
                Player.transform.position = spawn.transform.position;
                timerWait = 1;
            }
        }
    }
    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.tag == "Door") {
            nextScene = nextInt.ToString ();
            SceneManager.LoadScene (nextScene);
            nextInt++;
            nextScene = nextInt.ToString ();
        }

        if (collision.tag == "Spike") {
            isWait = true;
            positionDeath = Player.transform.position;
        }
    }
}