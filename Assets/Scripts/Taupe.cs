using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taupe : MonoBehaviour {
    public GameObject bullet;
    private int i = 0;
    public GameObject spawnBullet;
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    void FixedUpdate () {
        i++;
        if (i == 50) {
            i = 0;
            GameObject s = Instantiate (bullet, spawnBullet.transform.position, Quaternion.identity);
            // s.GetComponent<Rigidbody2D> ().velocity = new Vector2 (10, rigidBody2D.velocity.y);
        }
    }
}