using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HackedDesign 
{
    public class MissilePool : MonoBehaviour
    {
        [SerializeField] private HomingMissile missilePrefab;
        [SerializeField] List<HomingMissile> missileList;

        public void Fire(Vector3 position, Vector3 forward, Transform target, float baseSpeed, string pilot)
        {
            var missile =  missileList.FirstOrDefault(m => !m.fired);

            if(!missile)
            {
                var go = GameObject.Instantiate(missilePrefab.gameObject, position, Quaternion.identity, transform);
                go.transform.forward = forward;
                missile = go.GetComponent<HomingMissile>();
                missileList.Add(missile);
            }

            

            Debug.Log("Fire missile!");
            missile.Reset();
            missile.gameObject.SetActive(true);
            missile.Fire(target, baseSpeed, pilot);
            
        }
    }
}