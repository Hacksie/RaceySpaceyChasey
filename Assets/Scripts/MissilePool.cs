using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HackedDesign 
{
    public class MissilePool : MonoBehaviour
    {
        [SerializeField] private HomingMissile missilePrefab;
        [SerializeField] private Mine minePrefab;
        [SerializeField] List<HomingMissile> missileList;
        [SerializeField] List<Mine> mineList;

        public void Reset()
        {
            foreach(var missile in missileList)
            {
                missile.Reset();
            }

            foreach(var mine in mineList)
            {
                mine.Reset();
            }
        }

        public void Fire(Vector3 position, Vector3 forward, Transform target, float baseSpeed, string pilot)
        {
            var missile =  missileList.FirstOrDefault(m => !m.fired);

            if(!missile)
            {
                var go = GameObject.Instantiate(missilePrefab.gameObject, position, Quaternion.identity, transform);
                missile = go.GetComponent<HomingMissile>();
                missileList.Add(missile);
            }

            missile.transform.position = position;
            missile.transform.forward = forward;

            missile.Reset();
            missile.gameObject.SetActive(true);
            missile.Fire(target, baseSpeed, pilot);
        }

        public void Mine(Vector3 position, string pilot)
        {
            var mine =  mineList.FirstOrDefault(m => !m.fired);

            if(!mine)
            {
                var go = GameObject.Instantiate(minePrefab.gameObject, position, Quaternion.identity, transform);
                mine = go.GetComponent<Mine>();
                mineList.Add(mine);
            }

            mine.Reset();
            mine.gameObject.SetActive(true);
            mine.Fire(pilot);
            
        }

    }
}