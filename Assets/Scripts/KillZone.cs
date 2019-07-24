﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets._2D {
    public class KillZone : MonoBehaviour {
        private void OnTriggerEnter2D (Collider2D other) {
            if (other.tag == "Player"); {
                SceneManager.LoadScene ("Platform Game");
            }
        }
    }
}