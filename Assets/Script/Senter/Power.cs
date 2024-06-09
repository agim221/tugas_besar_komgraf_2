using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class Power : MonoBehaviour
{
    public GameObject light;
    public GameObject text;
    public bool isOn = false;
    private GameObject equiped;
    private bool playerInZone = false;

    public void On() {
        light.SetActive(true);
        isOn = true;
    }

    public void Off() {
        light.SetActive(false);
        isOn = false;
    }

    // public void OnTriggerEnter(Collider other) {
    //     if (other.tag == "Player") {
    //         playerInZone = true;
    //     }
    // }

    // public void OnTriggerExit(Collider other) {
    //     if (other.tag == "Player") {
    //         playerInZone = false;
    //     }
    // }

    void Update() {
        // if (playerInZone) {
            equiped = GameObject.FindWithTag("Equiped");
            if(Input.GetKeyDown(KeyCode.Q) && !isOn) {
                On();
            } else if(Input.GetKeyDown(KeyCode.Q) && isOn) {
                Off();
            }
        // }
    }
}