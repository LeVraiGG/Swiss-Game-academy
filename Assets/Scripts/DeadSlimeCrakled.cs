using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSlimeCrakled : MonoBehaviour {

    // Start is called before the first frame update
    void Start () {
    }

    // Update is called once per frame
    void Update () {
    }

    void OnCollisionEnter(Collider collision)
    {
       if(collision.gameObject.tag == "Player")
       {
          Destroy(gameObject, 3);
       }
    }
}