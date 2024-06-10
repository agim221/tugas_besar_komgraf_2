using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class ChangeObject : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject inventory;
    private GameObject temp;
    private int item_active;
    public GameObject slot;

    void ChangeItem(int item_active) {
        Match match = Match.Empty;

        if (inventory.transform.childCount > 0) {
            temp = GameObject.FindWithTag("Equiped");

            if (temp != null) {
                match = Regex.Match(temp.name, @"\d+");
                if (int.Parse(match.Value) == item_active) {
                    slot.SetActive(false);
                    temp.tag = "Inventory";
                    temp.SetActive(false);
                    return;
                }
            }

            foreach (Transform child in inventory.transform) {
                match = Regex.Match(child.name, @"\d+");

                if (match.Success && int.Parse(match.Value) == item_active)
                {
                    child.tag = "Equiped";
                    child.localPosition = new Vector3(0, 0, 0); //ubah ke posisi sekarang
                    child.gameObject.SetActive(true);
                    slot.SetActive(true);
                    slot.transform.localPosition = new Vector3(-200 + (100 * (int.Parse(match.Value) - 1)), -410, 0);
                    break;
                }
            }

            if (temp != null) {
                temp.tag = "Inventory";
                temp.transform.localPosition = new Vector3(0, 0, 0); //ubah ke posisi sekarang
                temp.SetActive(false);
            }
        } 
    }

    void Update() {
        for (int i = 1; i <= 5; i++) {
            if (Input.GetKeyDown(i.ToString())) {
                item_active = i;
                ChangeItem(item_active);
            }
        }
    }
}