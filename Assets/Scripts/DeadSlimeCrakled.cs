using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSlimeCrakled : MonoBehaviour {
    public bool isWait = false;
    public float timerWait = 3;
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (isWait) {
            timerWait -= Time.deltaTime;
            if (timerWait <= 0) {
                isWait = false;
                Destroy (gameObject);
                timerWait = 1;
            }
        }
    }
    private void OnCollisionEnter (Collider2D collision) {
        isWait = true;
    }
}