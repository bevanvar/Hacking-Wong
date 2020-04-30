using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Rigidbody2D target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!(target.position.x <= -4.2 || target.position.x >= 4.53))
            transform.position = new Vector3(target.position.x, 0, -10);
    }
}
