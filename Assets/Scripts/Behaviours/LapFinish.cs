using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class LapFinish : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            var player = other.gameObject.GetComponentInParent<PlayerController>();

            Debug.Log("Lap finished + " + other.tag);

            if(player)
            {
                GameManager.Instance.IncLap();
                Debug.Log("Lap finished");
            }
        }
    }
}
