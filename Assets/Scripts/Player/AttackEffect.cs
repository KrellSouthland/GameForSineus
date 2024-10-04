
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public int[] combination;
    [SerializeField] private int damage;
    [SerializeField] private bool sleeper;
    [SerializeField] private bool immortal;
    [SerializeField] private bool heal;
    [SerializeField] private bool speeder;
    [SerializeField] private bool quaker;
    [SerializeField] private int slowEffect;
    [SerializeField] private int fastEffect;
    [SerializeField] private int timerimmortal;
    [SerializeField] private Projectile attackProjectile;
    [SerializeField] private Quaker shaker;
    [SerializeField] private float ammountOfHeal;
    [SerializeField] private Health player;
    [SerializeField] private float CDTimer;
    
    public bool ReturnCombination(string combinatio)
    {
        string test = null;
        for (int i = 0; i < combination.Length; i++)
        {
            test += combination[i].ToString();
        }
        Debug.Log("test "+ test);
        return test == combinatio;
    }

    public void ActivateAttack()
    {
        if (sleeper)
        {
            // make sleepAttack
            return;
        }
        else if (heal)
        {
            Debug.Log("HeakStarts");
            player.AddHealth(ammountOfHeal);
            Debug.Log($"{ammountOfHeal}");
        }
        else if (immortal)
        {
            player.MakeImmortal(timerimmortal);
            return;
        }
        else if (quaker)
        {
            Quaker quake = Instantiate(shaker, transform.position, transform.rotation);
            Destroy( quake );
        }
        else
        {
            Projectile ammunition = Instantiate(attackProjectile, transform.position, transform.rotation);

            ammunition.Damage = damage;
            ammunition.Fire();
        }

    }

    private IEnumerator Earthquake()
    {
        Debug.Log("CamShake");
        yield return new WaitForSeconds(timerimmortal);
        Debug.Log("StopCam");
    }

   
}
