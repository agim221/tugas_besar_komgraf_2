using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class OpenDoorScript : MonoBehaviour
{
    public GameObject equiped;
    public Animator anim = null;
    public bool is_open = false;
    public bool is_locked = true;

    private void OpenDoor() {
        anim.Play("OpenDoor", 0, 0f);
        is_open = true;
    }

    private void OpenLock(string key) {
        if(key == "kunci") {
            print("open locked door");
            OpenDoor();
            is_locked = false;
        }
    }

    public void OnTriggerStay(Collider other) {
        if(other.tag == "Player") {
            if(Input.GetKey(KeyCode.F) && is_locked && !is_open) {
                if(equiped.transform.childCount == 1) {
                    string key = equiped.transform.GetChild(0).name;
                    Match match = Regex.Match(key, @"^(.*?) -");
                    if (match.Success) {
                        key = match.Groups[1].Value;
                        OpenLock(key);
                    }
                };
            } 
        }
    }
    
}
