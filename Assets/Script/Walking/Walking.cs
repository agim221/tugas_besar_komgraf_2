using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour
{
    AudioManager audioManager;
    private float walkSoundDelay = 0.5f; // Delay antara suara langkah (dalam detik)
    private float walkSoundTimer = 0f;

    public void Awake() {
        audioManager = GameObject.FindWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update() {
        // Kurangi timer setiap frame
        walkSoundTimer -= Time.deltaTime;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
            // Jika timer sudah mencapai nol, putar suara dan reset timer
            if (walkSoundTimer <= 0f) {
                audioManager.PlaySFX(audioManager.walking);
                walkSoundTimer = walkSoundDelay; // Reset timer
                print("Walking");
            }
        }
    }
}
