using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    public float dist = 5f;
    Vector2 position;

    // Update is called once per frame
    void Update()
    {
        position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 directionNorm = (position - (Vector2)transform.parent.position).normalized;
        transform.position = (Vector2)transform.parent.position + directionNorm * dist;
    }
}
