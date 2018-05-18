using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class curtainChangeScene : MonoBehaviour {

    public string level = "PrototypeBackStage";
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update () {

    }

    // Use this for initialization
    void OnCollisionEnter2D(Collision2D Colider)
    {
        if (Colider.gameObject.tag == "Player")
        Application.LoadLevel(level);
    }
    
}
