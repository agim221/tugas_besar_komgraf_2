// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.Text.RegularExpressions;

// public class ChangeObject : MonoBehaviour
// {
//     // Start is called before the first frame update
//     public GameObject equip;
//     public GameObject inventory;
//     private bool player_in_zone = false;
//     private GameObject temp;
//     private int item_active;
//     public GameObject slot;

//     void ChangeItem(int item_active) {
//         if (equip.transform.childCount > 0) {

//             temp = equip.transform.GetChild(0).gameObject;

//             foreach (Transform child in inventory.transform) {
//                 Match match = Regex.Match(child.name, @"\d+");
//                 if (match.Success && int.Parse(match.Value) == item_active) {
//                     child.transform.SetParent(equip.transform);
//                     child.transform.tag = "Equip";
//                     child.transform.localPosition = new Vector3(0, 0, 0);
//                     temp.transform.SetParent(inventory.transform);
//                     temp.transform.tag = "Inventory";
//                     temp.transform.localPosition = new Vector3(0, 0, 0);
//                     slot.transform.localPosition = new Vector3(-290 + (145 * (int.Parse(match.Value) - 1)), 0, 0);
//                     break;
//                 }
//             }
//         } else {
//             foreach (Transform child in inventory.transform) {
//                 Match match = Regex.Match(child.name, @"\d+");
//                 if (match.Success && int.Parse(match.Value) == item_active) {
//                     child.transform.SetParent(equip.transform);
//                     child.transform.tag = "Equip";
//                     child.transform.localPosition = new Vector3(0, 0, 0);
//                     slot.transform.localPosition = new Vector3(-290 + (145 * (int.Parse(match.Value) - 1)), 0, 0);
//                     slot.SetActive(true);
//                     break;
//                 }
//             }
        
//         }
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
//             for (int i = 1; i <= 5; i++) {
//                 if (Input.GetKeyDown(i.ToString())) {
//                     item_active = i;
//                     ChangeItem(item_active);
//                 }
//             }
//         }
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class ChangeObject : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject inventory;
    private bool player_in_zone = false;
    private GameObject temp;
    private int item_active;
    public GameObject slot;

    void ChangeItem(int item_active) {
        Match match = Match.Empty;

        print(inventory.transform.childCount);
        if (inventory.transform.childCount > 0) {
            foreach (Transform child in inventory.transform) {
                match = Regex.Match(child.name, @"\d+");
                if (child.tag == "Equip" && int.Parse(match.Value) == item_active) {
                    return;
                } else if(child.tag == "Equip") {
                    temp = child.gameObject;
                    break;
                } else {
                    temp = null;
                }
            }
            
            // if(temp != null) {
            //     foreach (Transform child in inventory.transform) {
            //         match = Regex.Match(child.name, @"\d+");
            //         if (match.Success && int.Parse(match.Value) == item_active) 
            //         {
            //             child.tag = "Equip";
            //             child.localPosition = new Vector3(0, 0, 0);
            //             child.gameObject.SetActive(true);
                        
                        
            //             temp.transform.tag = "Inventory";
            //             temp.transform.localPosition = new Vector3(0, 0, 0);
            //             temp.SetActive(false);
            //             break;
            //         }
            //     }
            // } else {
            //     foreach (Transform child in inventory.transform) {
            //         match = Regex.Match(child.name, @"\d+");
            //         if (match.Success && int.Parse(match.Value) == item_active)
            //         {
            //             child.tag = "Equip";
            //             child.localPosition = new Vector3(0, 0, 0);
            //             child.gameObject.SetActive(true);
            //             break;
            //         }
            //     }
            // }

            foreach (Transform child in inventory.transform) {
                match = Regex.Match(child.name, @"\d+");
                if (match.Success && int.Parse(match.Value) == item_active)
                {
                    child.tag = "Equip";
                    child.localPosition = new Vector3(0, 0, 0);
                    child.gameObject.SetActive(true);
                    break;
                }
            }

            if (temp != null) {
                temp.tag = "Inventory";
                temp.transform.localPosition = new Vector3(0, 0, 0);
                temp.SetActive(false);
            }
        } 

        slot.transform.localPosition = new Vector3(-290 + (145 * (int.Parse(match.Value) - 1)), 0, 0);
    }
    
    void OnTriggerEnter(Collider other) {
        player_in_zone = true;
    }

    void OnTriggerExit(Collider other) {
        player_in_zone = false;
    }

    void Update() {
        if (player_in_zone) {
            for (int i = 1; i <= 5; i++) {
                if (Input.GetKeyDown(i.ToString())) {
                    item_active = i;
                    ChangeItem(item_active);
                }
            }
        }
    }
}