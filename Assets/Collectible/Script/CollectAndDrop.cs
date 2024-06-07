// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.Text.RegularExpressions;

// public class CollectAndDrop : MonoBehaviour
// {
//     public GameObject my_object;
//     public Transform equip;
//     public GameObject inventory;
//     public bool is_collected = false;
//     private bool player_in_zone;
//     private string original_name;
//     public GameObject pictures;
//     public GameObject Panel;
//     private GameObject picture;
//     public GameObject slot;

//     void Start()
//     {
//         my_object.GetComponent<Rigidbody>().isKinematic = false;
//         original_name = my_object.name;
//         picture = new GameObject();
//         player_in_zone = false;
//     }

//     void Drop() {
//         if(equip.childCount == 0) return; 

//         my_object = equip.GetChild(0).gameObject;
        
//         if(my_object.transform.tag == "Equip") {
//             equip.DetachChildren();
//             my_object.transform.eulerAngles = new Vector3(my_object.transform.position.x, my_object.transform.position.y, my_object.transform.position.z);
//             my_object.GetComponent<Rigidbody>().isKinematic = false;
//             my_object.GetComponent<MeshCollider>().enabled = true;
//             slot.SetActive(false);

//             foreach (Transform child in Panel.transform) {
//                 if(child.name == my_object.name) {
//                     DestroyImmediate(child.gameObject);
//                 }
//             }

//             my_object.name = original_name;
//             is_collected = false;
//         }
//     }

//     void Collect() {
//         int invent_index = 1;
//         Match match_equip = Match.Empty;
//         Match match_invent = Match.Empty;
    
//         my_object.GetComponent<Rigidbody>().isKinematic = true;
//         my_object.transform.rotation = equip.transform.rotation;
//         my_object.GetComponent<MeshCollider>().enabled = false;

//         //search index
//         if(equip.childCount > 0) {
//             match_equip = Regex.Match(equip.GetChild(0).name, @"\d+$");
//             print(match_equip.Value + " " + invent_index.ToString());
//             if(match_equip.Success && match_equip.Value == invent_index.ToString()) {
//                 invent_index++;
//             }
//         }
//         // if dan foreach ada error
//         foreach (Transform child in inventory.transform) {
//             match_invent = Regex.Match(child.name, @"\d+$");
//             print(match_invent.Value + " " + invent_index.ToString());
//             if(match_invent.Success && match_invent.Value == invent_index.ToString()) {
//                 invent_index++;
//             }
//         }
        
//         // add to equip or inventory
//         if(equip.childCount == 0) {
//             my_object.transform.SetParent(equip);
//             my_object.transform.localPosition = new Vector3(0, 0, 0);
//             my_object.transform.tag = "Equip";

//             slot.SetActive(true);
//             slot.transform.localPosition = new Vector3(-290f + (145 * (invent_index - 1)), 0f, 0f);
//         } else {
//             my_object.transform.SetParent(inventory.transform);
//             my_object.transform.localPosition = new Vector3(0, 0, 0);
//             my_object.transform.tag = "Inventory";
//         }

//         // add to panel
//         foreach (Transform child in pictures.transform) {
//             if(child.name.ToLower() == my_object.transform.name.ToLower()) {
//                 picture = Instantiate(child.gameObject);
//                 break;
//             }
//         }

//         // new obj configurati 
//         picture.transform.SetParent(Panel.transform);
//         picture.transform.name = my_object.name + " - " + invent_index.ToString() ;
        
//         picture.transform.localPosition = new Vector3(-290f + (145 * (invent_index - 1)), 0f, 0f);
//         picture.transform.localScale = new Vector3(1.1f, 3.5f, 1f);

//         // slot configuration
//         my_object.name = my_object.name + " - " + invent_index.ToString();
//         is_collected = true;
//     }
//     void OnTriggerEnter(Collider other) {
//         if (other.tag == "Player") {
//             player_in_zone = true;
//         }
//     }

//     void OnTriggerExit(Collider other) {
//         if (other.tag == "Player") {
//             player_in_zone = false;
//         }
//     }

//     void Update() {
//         if (player_in_zone) {
//             if (Input.GetKeyDown(KeyCode.E) && !is_collected && (equip.childCount + inventory.transform.childCount) <= 5) {
//                 Collect();
//             }
//             if (Input.GetKeyDown(KeyCode.F) && is_collected) {
//                 Drop();
//             }
//         }
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class CollectAndDrop : MonoBehaviour
{
    public GameObject my_object;
    public GameObject inventory;
    private bool is_collected = false;
    private bool player_in_zone;
    private string original_name;
    public GameObject pictures;
    public GameObject panel;
    private GameObject picture;
    public GameObject slot;

    void start() {
        my_object.GetComponent<Rigidbody>().isKinematic = false;
        picture = new GameObject();
        player_in_zone = false;
    }

    void Drop() {
        foreach (Transform child in inventory.transform) {
            if (child.tag == "Equip") {
                my_object = child.gameObject;
                break;
            }
        }

        my_object.transform.eulerAngles = new Vector3(my_object.transform.position.x, my_object.transform.position.y, my_object.transform.position.z);
        my_object.GetComponent<Rigidbody>().isKinematic = false;
        my_object.GetComponent<MeshCollider>().enabled = true;
        slot.SetActive(false);

        foreach (Transform child in panel.transform) {
            if (child.name == my_object.name) {
                DestroyImmediate(child.gameObject);
            }
        }

        print(original_name);
        my_object.transform.name = original_name;
        print(my_object.transform.name);
        my_object.transform.tag = "Untagged";
        is_collected = false;
        my_object.transform.SetParent(inventory.transform.parent.gameObject.transform.parent);
    }

    void Collect() {
        bool equip = true;
        int invent_index = 1;

        my_object.GetComponent<Rigidbody>().isKinematic = true;
        my_object.transform.rotation = inventory.transform.rotation;
        my_object.GetComponent<MeshCollider>().enabled = false;
        my_object.transform.SetParent(inventory.transform);
        my_object.transform.localPosition = new Vector3(0, 0, 0);

        foreach (Transform child in inventory.transform) {
            if(child.tag == "Equip") {
                equip = false;
                invent_index++;
            } else {
                Match match = Regex.Match(child.name, @"\d+$");
                if(match.Success && match.Value == invent_index.ToString()) {
                    invent_index++;
                }
            }
        }

        if(equip) {
            my_object.transform.tag = "Equip";
            slot.SetActive(true);
            slot.transform.localPosition = new Vector3(-290f + (145 * (invent_index - 1)), 0f, 0f);
        } else {
            my_object.transform.tag = "Inventory";
            my_object.SetActive(false);
        }

        foreach (Transform child in pictures.transform) {
            if(child.name.ToLower() == my_object.transform.name.ToLower()) {
                picture = Instantiate(child.gameObject);
                break;
            }
        }

        picture.transform.SetParent(panel.transform);
        picture.transform.name = my_object.name + " - " + invent_index.ToString() ;
        
        picture.transform.localPosition = new Vector3(-290f + (145 * (invent_index - 1)), 0f, 0f);
        picture.transform.localScale = new Vector3(1.1f, 3.5f, 1f);

        // slot configuration
        original_name = my_object.transform.name;
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
            if (Input.GetKeyDown(KeyCode.E) && !is_collected && (inventory.transform.childCount) <= 5) {
                Collect();
            }
            if (Input.GetKeyDown(KeyCode.F) && is_collected) {
                Drop();
            }
        }
    }
}
