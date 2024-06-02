using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class ChangeObject : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject equip;
    public GameObject inventory;
    private bool player_in_zone = false;
    private GameObject temp;
    private int item_active;
    public GameObject slot;

    void ChangeItem(int item_active) {
        if (equip.transform.childCount > 0) {

            temp = equip.transform.GetChild(0).gameObject;

            foreach (Transform child in inventory.transform) {
                Match match = Regex.Match(child.name, @"\d+");
                if (match.Success && int.Parse(match.Value) == item_active) {
                    child.transform.SetParent(equip.transform);
                    child.transform.tag = "Equip";
                    child.transform.localPosition = new Vector3(0, 0, 0);
                    temp.transform.SetParent(inventory.transform);
                    temp.transform.tag = "Inventory";
                    temp.transform.localPosition = new Vector3(0, 0, 0);
                    slot.transform.localPosition = new Vector3(-290 + (145 * (int.Parse(match.Value) - 1)), 0, 0);
                    break;
                }
            }
        } else {
            foreach (Transform child in inventory.transform) {
                Match match = Regex.Match(child.name, @"\d+");
                if (match.Success && int.Parse(match.Value) == item_active) {
                    child.transform.SetParent(equip.transform);
                    child.transform.tag = "Equip";
                    child.transform.localPosition = new Vector3(0, 0, 0);
                    slot.transform.localPosition = new Vector3(-290 + (145 * (int.Parse(match.Value) - 1)), 0, 0);
                    slot.SetActive(true);
                    break;
                }
            }
        
        }
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
            for (int i = 1; i <= 5; i++) {
                if (Input.GetKeyDown(i.ToString())) {
                    item_active = i;
                    print(item_active);
                    ChangeItem(item_active);
                }
            }
        }
    }
}