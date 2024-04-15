using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class lerolero : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] TMP_Text textMeshPro;

    private void Start()
    {
        
        
    }

    private void Update()
    {
        textMeshPro.text = player.energy.ToString();
    }
}
