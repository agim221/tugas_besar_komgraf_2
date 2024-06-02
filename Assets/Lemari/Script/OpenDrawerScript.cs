using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDrawerScript : MonoBehaviour
{
    public Animator anim = null;
    public string name;
    public bool is_open = false;
    public bool is_locked = true;

    private void OpenDrawer() {
        anim.Play(name, 0, 0f);
        is_open = true;
    }

    public void OnTriggerStay(Collider other) {
        if(other.tag == "Player") {
            if(Input.GetKey(KeyCode.F) && !is_open) {
                OpenDrawer();
            }
        }
    }
}
