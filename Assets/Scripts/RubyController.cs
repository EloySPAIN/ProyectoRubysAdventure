using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    private float velocidad = 5f;
    // Start is called before the first frame update
    void Start() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * velocidad * Time.deltaTime;
            
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.right * -velocidad * Time.deltaTime;
            
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * velocidad * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.up * -velocidad * Time.deltaTime;

        }
    }
    
}
