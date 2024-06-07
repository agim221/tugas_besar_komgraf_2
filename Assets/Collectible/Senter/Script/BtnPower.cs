using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions; // Add this line

public class BtnPower : MonoBehaviour
{
    public GameObject equiped;
    public GameObject light;
    public string key;
    public bool is_on = false;
    private bool playerInZone = false;

    public void On(string key) {
        light.SetActive(true);
        is_on = true;
        Debug.Log("Light turned on.");
    }

    public void Off(string key) {
        light.SetActive(false);
        is_on = false;
        Debug.Log("Light turned off.");
    }

    public void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            playerInZone = true;
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            playerInZone = false;
        }
    }

    void Update() {
        if (playerInZone) {
            if (equiped.transform.childCount > 0) {
                key = equiped.transform.GetChild(0).tag;
                if (Input.GetKeyDown(KeyCode.Q) && key == "Senter") {
                    if (!is_on) {
                        Debug.Log("Turn on the light");
                        On(key);
                    } else {
                        Debug.Log("Turn off the light");
                        Off(key);
                    }
                }
            }
        }
    }
}