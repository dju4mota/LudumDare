using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeController : MonoBehaviour
{
    private GameObject player;
    private Vector3 mousePosition;
    [SerializeField] float maxRange;
    private Vector2 Range;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Range.x = MathF.Min(maxRange, mousePosition.x);
        Range.y = MathF.Min(maxRange, mousePosition.y);
        transform.position = new(Range.x, Range.y, 0);
        Debug.Log(Range);
    }
}
