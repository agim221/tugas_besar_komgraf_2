using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class CollectAndDrop : MonoBehaviour
{
    public GameObject my_object;
    public Transform equip;
    public GameObject inventory;
    public bool is_collected = false;
    private bool player_in_zone;
    private string original_name;
    public GameObject pictures;
    public GameObject Panel;
    private GameObject picture;
    public GameObject slot;

    void Start()
    {
        my_object.GetComponent<Rigidbody>().isKinematic = false;
        original_name = my_object.name;
        picture = new GameObject();
        player_in_zone = false;
    }

    void Drop() {
        if(equip.childCount == 0) return; 

        my_object = equip.GetChild(0).gameObject;
        
        if(my_object.transform.tag == "Equip") {
            equip.DetachChildren();
            my_object.transform.eulerAngles = new Vector3(my_object.transform.position.x, my_object.transform.position.y, my_object.transform.position.z);
            my_object.GetComponent<Rigidbody>().isKinematic = false;
            my_object.GetComponent<MeshCollider>().enabled = true;
            slot.SetActive(false);

            foreach (Transform child in Panel.transform) {
                if(child.name == my_object.name) {
                    DestroyImmediate(child.gameObject);
                }
            }

            my_object.name = original_name;
            is_collected = false;
        }
    }

    void Collect() {
        int invent_index = 1;
        Match match_equip = Match.Empty;
        Match match_invent = Match.Empty;
    
        my_object.GetComponent<Rigidbody>().isKinematic = true;
        my_object.transform.rotation = equip.transform.rotation;
        my_object.GetComponent<MeshCollider>().enabled = false;
    
        //search index
        if(equip.childCount > 0) {
            match_equip = Regex.Match(equip.GetChild(0).name, @"\d+$");
            if(match_equip.Success && match_equip.Value == invent_index.ToString()) {
                print("match_equip: " + match_equip.Value);
                invent_index++;
            }
        }
    
        foreach (Transform child in inventory.transform) {
            match_invent = Regex.Match(child.name, @"\d+$");
            if(match_invent.Success && match_invent.Value == invent_index.ToString()) {
                print("match_invent: " + match_invent.Value);
                invent_index++;
            }
        }
        
        // add to equip or inventory
        if(equip.childCount == 0) {
            my_object.transform.SetParent(equip);
            my_object.transform.localPosition = new Vector3(0, 0, 0);
            my_object.transform.tag = "Equip";

            slot.SetActive(true);
            slot.transform.localPosition = new Vector3(-290f + (145 * (invent_index - 1)), 0f, 0f);
        } else {
            my_object.transform.SetParent(inventory.transform);
            my_object.transform.localPosition = new Vector3(0, 0, 0);
            my_object.transform.tag = "Inventory";
        }

        // add to panel
        foreach (Transform child in pictures.transform) {
            if(child.name.ToLower() == my_object.transform.name.ToLower()) {
                picture = Instantiate(child.gameObject);
                break;
            }
        }

        // new obj configurati n
        picture.transform.SetParent(Panel.transform);
        picture.transform.name = my_object.name + " - " + invent_index.ToString() ;
        
        picture.transform.localPosition = new Vector3(-290f + (145 * (invent_index - 1)), 0f, 0f);
        picture.transform.localScale = new Vector3(1.1f, 3.5f, 1f);

        // slot configuration
        my_object.name = my_object.name + " - " + invent_index.ToString();
        is_collected = true;
    }
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            player_in_zone = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            player_in_zone = false;
        }
    }

    void Update() {
        if (player_in_zone) {
            if (Input.GetKeyDown(KeyCode.E) && !is_collected && (equip.childCount + inventory.transform.childCount) <= 5) {
                Debug.Log(equip.childCount + inventory.transform.childCount);
                Collect();
            }
            if (Input.GetKeyDown(KeyCode.F) && is_collected) {
                Drop();
            }
        }
    }
}
