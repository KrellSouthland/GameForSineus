using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KutInterractor : MonoBehaviour
{
    [SerializeField] private  float kutAlpha = 1f;
    bool isCollect = false;
    [SerializeField] ParticleSystem[] ps; 

    private void Update()
    {
        if (isCollect)
        { 
            Debug.Log(kutAlpha);
            kutAlpha -= 6f * Time.deltaTime;

            foreach (var p in ps) {

                p.startColor = new Color(
                    p.main.startColor.color.r,
                    p.main.startColor.color.g,
                    p.main.startColor.color.b,
                    kutAlpha);
            }

            if (kutAlpha < -44f)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Shaman" && isCollect == false)
        {
            KutMission.AddKut(); 
            isCollect = true;
        }
    }
     
}
