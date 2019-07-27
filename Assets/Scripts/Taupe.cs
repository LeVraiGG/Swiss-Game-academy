using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taupe : MonoBehaviour {
    public GameObject bullet;
    private int i = 0;
    public GameObject spawnBullet;
    private AudioSource audiosource;
    public AudioClip cliplancer;
    public int cadence = 50;
    // Start is called before the first frame update
    void Start () {
        audiosource = GetComponent<AudioSource>();
        audiosource.clip = cliplancer;

    }

    // Update is called once per frame
    void Update () {

    }

    void FixedUpdate () {
        i++;
        if (i == 50) {
            i = 0;
            GameObject s = Instantiate (bullet, spawnBullet.transform.position, Quaternion.identity);
            audiosource.Play();
        }
    }
}