using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;


public class CollectAndDrop : MonoBehaviour
{
    AudioManager audioManager;

    public void Awake() {
        audioManager = GameObject.FindWithTag("Audio").GetComponent<AudioManager>();
    }
    public GameObject myObject;
    public GameObject inventory;
    private bool isCollected = false;
    public GameObject pictures;
    public GameObject panel;
    public GameObject used;
    private string originalName;
    private bool playerInZone;
    private GameObject picture;

    void start() {
        myObject.GetComponent<Rigidbody>().isKinematic = true;
        picture = new GameObject();
        playerInZone = false;
    }

    void Drop() {
        myObject = GameObject.FindWithTag("Equiped");

        myObject.transform.eulerAngles = new Vector3(myObject.transform.position.x, myObject.transform.position.y, myObject.transform.position.z);
        myObject.GetComponent<Rigidbody>().isKinematic = false;
        myObject.GetComponent<MeshCollider>().enabled = true;
        used.SetActive(false);

        foreach (Transform child in panel.transform) {
            if (child.name == myObject.name) {
                DestroyImmediate(child.gameObject);
            }
        }

        myObject.transform.name = originalName;
        myObject.transform.tag = "Untagged";
        isCollected = false;
        myObject.transform.SetParent(inventory.transform.parent.gameObject.transform.parent);
        audioManager.PlaySFX(audioManager.pickUp);
    }

    void Collect() {
        bool equip = true;
        int invent_index = 1;

        myObject.transform.SetParent(inventory.transform);
        myObject.GetComponent<Rigidbody>().isKinematic = true;
        myObject.transform.rotation = inventory.transform.rotation;
        myObject.GetComponent<MeshCollider>().enabled = false;
        myObject.transform.localRotation = Quaternion.Euler(0, -90, 0);
        myObject.transform.localPosition = new Vector3(0.25f, -0.3f, 1f);

        if(GameObject.FindWithTag("Equiped")) {
            equip = false;
        }

        foreach (Transform child in inventory.transform) {
            Match match = Regex.Match(child.name, @"\d+$");
            if(match.Success && (match.Value == invent_index.ToString())) {
                invent_index++;
            }
        }

        // foreach (Transform child in inventory.transform) {
        //     if(child.tag == "Equip") {
        //         equip = false;
        //         invent_index++;
        //     } else {
        //         Match match = Regex.Match(child.name, @"\d+$");
        //         if(match.Success && match.Value == invent_index.ToString()) {
        //             invent_index++;
        //         }
        //     }
        // }

        if(equip) {
            myObject.transform.tag = "Equiped";
            print("equiped");
            used.SetActive(true);
            used.transform.localPosition = new Vector3(-200f + (100 * (invent_index - 1)), -410f, 0f);
        } else {
            myObject.transform.tag = "Inventory";
            myObject.SetActive(false);
        }

        foreach (Transform child in pictures.transform) {
            if(child.name == myObject.transform.name) {
                picture = Instantiate(child.gameObject);
                break;
            }
        }

        picture.transform.SetParent(panel.transform);
        picture.transform.name = myObject.name + " - " + invent_index.ToString() ;
        
        picture.transform.localPosition = new Vector3(-200f + (100 * (invent_index - 1)), -410f, 0f);
        picture.transform.localScale = new Vector3(1f, 1f, 1f);

        originalName = myObject.transform.name;
        myObject.name = myObject.name + " - " + invent_index.ToString();
        isCollected = true;
        audioManager.PlaySFX(audioManager.pickUp);
    }

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
            if (Input.GetKeyDown(KeyCode.E) && !isCollected && (inventory.transform.childCount) <= 5) {
                Collect();
            } else if (Input.GetKeyDown(KeyCode.F) && isCollected) {
                Drop();
            }
        }
    }
}
