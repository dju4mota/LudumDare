using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeController : MonoBehaviour
{
    private Transform player;
    [SerializeField] float maxRange;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>().gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; 
        Vector3 direction = mousePosition - player.position;
        direction = Vector3.ClampMagnitude(direction, maxRange); 
        direction = direction.normalized * maxRange;      
        Vector3 clampedMousePosition = player.position + direction;

        transform.position = clampedMousePosition;

        if(Input.GetKeyDown(KeyCode.Z))
            Debug.Log(mousePosition);
    }
}
