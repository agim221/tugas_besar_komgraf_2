using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class PutObject : MonoBehaviour
{
    AudioManager audioManager;

    public void Awake() {
        audioManager = GameObject.FindWithTag("Audio").GetComponent<AudioManager>();
    }

    public GameObject Place;
    public Animator anim;
    public string state;
    private GameObject Object;
    private bool playerInZone = false;
    public int count = 0;
    public GameObject panel;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            playerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            playerInZone = false;
        }
    }
    
    public void Update() {
        if (playerInZone) {
            if(Input.GetKeyDown(KeyCode.R)) {
                Object = GameObject.FindWithTag("Equiped");
                Match match = Regex.Match(Object.name, @"^\w+");
                Object.transform.SetParent(Place.transform);
                Object.transform.name = match.Value;
                Object.transform.tag = "Untagged";

                if (Object.name == "Boneka") {
                    Object.transform.localPosition = new Vector3(-1.076f, 8.9f, -5.8f);
                } else if(Object.name == "Jarum") {
                    Object.transform.localPosition = new Vector3(-2.032f, 8.9f, -6.869f);
                } else if(Object.name == "Buku") {
                    Object.transform.localPosition = new Vector3(-3.066f, 8.9f, -6.585f);
                } else if(Object.name == "Figura Creepy") {
                    Object.transform.localPosition = new Vector3(-2.8f, 8.9f, -5.45f);
                }

                count++;
                audioManager.PlaySFX(audioManager.pickUp);

                foreach (Transform child in panel.transform) {
                    match = Regex.Match(child.name, @"^\w+");
                    if (child.name == Object.name) {
                        DestroyImmediate(child.gameObject);
                }
        }

                if(count == 4) {
                    anim.Play(state, 0, 0f);
                    audioManager.PlaySFX(audioManager.openDoor);
                }
            }
        }
    }
}
 