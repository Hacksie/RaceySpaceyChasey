using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class Stars : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Transform worldOrigin;
        [SerializeField] private int count = 100;
        [SerializeField] private float minRadius = 12000.0f;
        [SerializeField] private float maxRadius = 20000.0f;

        [Header("Referenced Game Objects")]
        [SerializeField] private Transform parent;

        [Header("Prefabs")]
        [SerializeField] private List<GameObject> starsSprites;

        // Start is called before the first frame update
        void Start()
        {
            ResetStars();
        }

        public void ResetStars()
        {
            for (int i = 0; i < count; i++)
            {
                Vector3 pos = Random.onUnitSphere * Random.Range(minRadius, maxRadius);
                GameObject.Instantiate(starsSprites[Random.Range(0, starsSprites.Count)], Random.onUnitSphere * Random.Range(minRadius, maxRadius), Quaternion.LookRotation((pos - worldOrigin.position), Vector3.up), parent);
            }
        }
    }
}