using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostController : MonoBehaviour
{
    private bool playerInZone = false;
    
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            playerInZone = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            playerInZone = false;
        }
    }
    
    void Update() {
        if (playerInZone) {
             SceneManager.LoadSceneAsync(0);
        }
    }
}
