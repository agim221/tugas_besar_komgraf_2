using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorStatus : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject text;
    public bool is_open = false;

    void Start()
    {
        text.SetActive(false);
    }


}
