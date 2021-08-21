using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public string pilot = "";
    [SerializeField] public int maxRacey = 1;
    [SerializeField] public int maxSpacey = 1;
    [SerializeField] public int maxChasey = 1;
    [SerializeField] public RenderTexture renderTexture;
    [SerializeField] public float acceleration = 3.0f;
    [SerializeField] public float decceleration = 1.5f;
    

    [Header("State")]
    [SerializeField] public int currentRacey = 1;
    [SerializeField] public int currentSpacey = 1;
    [SerializeField] public int currentChasey = 1;
    [SerializeField] public string currentChaseyType = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
