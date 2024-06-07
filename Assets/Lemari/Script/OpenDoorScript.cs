using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class OpenDoorScript : MonoBehaviour
{
    public GameObject inventory;
    public GameObject text;
    public Animator anim;
    public bool is_locked;
    private GameObject Equiped;
    private bool is_open = false;
    private bool player_in_zone;

    void start() {
       
    }

    void OpenDoor() {
        anim.Play("OpenDoor", 0, 0f);
        is_open = true;
    }

    void OpenLock() {
        // if(key == "kunci") {
        //     OpenDoor();
        //     is_locked = false;
        // }
    }
   
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            player_in_zone = true;
            text.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            player_in_zone = false;
            text.SetActive(false);
        }
    }
    
    void Update() {
        if (player_in_zone) {
            if (Input.GetKeyDown(KeyCode.G) && !is_open) {
                OpenDoor();
            }   
        }
    }
}