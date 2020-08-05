using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int hitsNeeded = 15;
    private int hitsTaken;
    public GameObject hit;
    public GameObject Explosion;

        void OnTriggerEnter(Collider other)
        {
            hitsTaken += 1;
            Instantiate(hit, transform.position, Quaternion.identity);
    
            if (hitsTaken >= hitsNeeded)
            {
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            }   
        }

        private void OnDestroy()
        {
            GameController.BoosDefeated = true;
        }
}
    
