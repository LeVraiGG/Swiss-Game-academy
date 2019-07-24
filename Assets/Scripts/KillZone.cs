using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour {
    void Start () {
        Update ();
    }
    void Update () {
        //     OnTriggerEnter2D();
    }
    public void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "Player"); {
            SceneManager.LoadScene ("Level1");
        }
    }
}