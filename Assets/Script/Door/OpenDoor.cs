using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class OpenDoorScript : MonoBehaviour
{
    AudioManager audioManager;

    public void Awake() {
        audioManager = GameObject.FindWithTag("Audio").GetComponent<AudioManager>();
    }
    
    public GameObject text;
    public GameObject panel; 
    public GameObject used;
    public Animator anim;
    public bool isLocked;
    public string state;
    private bool isOpen = false;
    private bool playerInZone;

    void OpenDoor() {
        audioManager.PlaySFX(audioManager.openDoor);
        anim.Play(state, 0, 0f);
        isOpen = true;
    }

    void OpenLock() {
        GameObject temp  = GameObject.FindWithTag("Equiped");
        if(temp != null) {
            Match match = Regex.Match(temp.transform.name, @"(Kunci)");
            if(match.Success && match.Value == "Kunci"){
                foreach (Transform child in panel.transform) {
                    if (child.name == temp.name) {
                        DestroyImmediate(child.gameObject);
                    }
                }
                DestroyImmediate(temp);
                used.SetActive(false);
                OpenDoor();
            }
        }
    }
   
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            playerInZone = true;
            text.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            playerInZone = false;
            text.SetActive(false);
        }
    }
    
    void Update() {
        if (playerInZone) {
            if (Input.GetKeyDown(KeyCode.G) && !isOpen && isLocked) {
                OpenLock();
            } else if(Input.GetKeyDown(KeyCode.G) && !isOpen) {
                OpenDoor();
            }
        }
    }
}