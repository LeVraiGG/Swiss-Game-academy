using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public GameObject deadSlime;
    public GameObject deadSlimeCrakled;
    public bool isWait = false;
    public float timerWait = 1;
    public int nbBloc = 3;
    public int nbBlocCrakled = 3;
    private bool crakledIsSelected;
    public Text textVar1;
    public Text textVar2;

    public int numLeaves;

    void Start () {
        rigidBody2D = GetComponent<Rigidbody2D> ();
        spawn = GameObject.FindWithTag ("spawn");
        Player = GameObject.FindWithTag ("Player");
    }

    void Update () {
        textVar1.text = (" " + nbBloc.ToString () + " ");
        textVar2.text = (" " + nbBlocCrakled.ToString () + " ");

        if (!crakledIsSelected) {
            textVar1.color = Color.red;
            textVar2.color = Color.white;
        }
        if (crakledIsSelected) {
            textVar1.color = Color.white;
            textVar2.color = Color.red;
        }

        if (!isWait) {
            float horizontalInput = Input.GetAxis ("Horizontal");
            bool canJump = Physics2D.OverlapCircle (jumpPosition.position, raycastRadius, mask);
            Vector2 velocity = new Vector2 (horizontalInput * playerVelocity, rigidBody2D.velocity.y);
            if (!canJump) {
                velocity.x = velocity.x / Mathf.Sqrt (2);
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

            if (Input.GetButtonDown ("Fire2")) {
                Player = GameObject.FindWithTag ("Player");
                spawn = GameObject.FindWithTag ("spawn");

                if (crakledIsSelected) {
                    if (nbBlocCrakled > 0) {
                        Instantiate (deadSlimeCrakled, Player.transform.position, Quaternion.identity);
                        Player.transform.position = spawn.transform.position;
                        nbBlocCrakled--;
                    }
                } else if (nbBloc > 0) {
                    Instantiate (deadSlime, Player.transform.position, Quaternion.identity);
                    Player.transform.position = spawn.transform.position;
                    nbBloc--;
                }
            }
        }
        if (Input.GetButtonDown ("Submit")) {
            crakledIsSelected = !crakledIsSelected;
        }
        if (isWait) {
            rigidBody2D.gravityScale = 0;
            timerWait -= Time.deltaTime;
            rigidBody2D.velocity = new Vector2 ();
            if (timerWait <= 0) {
                rigidBody2D.velocity = new Vector2 ();
                rigidBody2D.gravityScale = 0;
                Player.transform.position = spawn.transform.position;
                isWait = false;
                rigidBody2D.gravityScale = 5;
                timerWait = 1;
            }
        }
        if (Input.GetButtonDown ("Fire3") && !isWait) {
            Player = GameObject.FindWithTag ("Player");
            spawn = GameObject.FindWithTag ("spawn");

            Player.transform.position = spawn.transform.position;
        }
        if (Input.GetButtonDown ("Cancel")) {
            nextInt--;
            nextScene = nextInt.ToString ();
            SceneManager.LoadScene (nextScene);
            nextInt++;
            nextScene = nextInt.ToString ();
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
        }
    }
    void OnCollisionEnter2D (Collision2D col) {
        if (col.gameObject.tag == "deadSlimeCrakled") {
            Destroy (col.gameObject, 3);
        }
    }
}