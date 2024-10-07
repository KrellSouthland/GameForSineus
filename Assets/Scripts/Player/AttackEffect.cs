
using System.Collections;
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
    [SerializeField] private bool feather;
    [SerializeField] private int jumpEffect;
    [SerializeField] private int fastEffect;
    [SerializeField] private int timerimmortal;
    [SerializeField] private float fastTimer;
    [SerializeField] private float jumpTimer;
    [SerializeField] private Projectile attackProjectile;
    [SerializeField] private Quaker shaker;
    [SerializeField] private float ammountOfHeal;
    [SerializeField] private Health player;
    [SerializeField] private float CDTimer;
    [SerializeField] private bool onCD;

    [SerializeField] private AudioClip[] CastSound;
    
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
        //SoundHolder.Instance.PlaySound(CastSound);
        if (!onCD)
        {
            if (sleeper)
            {
                // make sleepAttack
                return;
            }
            else if (heal)
            {
                SoundHolder.Instance.PlaySound(SoundHolder.Instance.HealSpell);
                player.AddHealth(ammountOfHeal);
            }
            else if (immortal)
            {
                SoundHolder.Instance.PlaySound(SoundHolder.Instance.ShieldSpell);
                player.MakeImmortal(timerimmortal);
            }
            else if (quaker)
            {
                Quaker quake = Instantiate(shaker, transform.position, transform.rotation);
                StartCoroutine(QuakeTimer(quake.gameObject));
                StartCoroutine(Earthquake());

            }
            else if(speeder)
            {
                SoundHolder.Instance.PlaySound(SoundHolder.Instance.SpeedSpell);
                StartCoroutine(SpeedEffect());

            }
            else if (feather)
            {
                SoundHolder.Instance.PlaySound(SoundHolder.Instance.JumpSpell);
                StartCoroutine(JumpEffect());
            }
            else
            {
                SoundHolder.Instance.PlaySound(SoundHolder.Instance.FireSpell);
                Projectile ammunition = Instantiate(attackProjectile, transform.position, transform.rotation);

                ammunition.Damage = damage;
                ammunition.Fire();
            }
            onCD = true;
            StartCoroutine(CD());
        }
    }

    private IEnumerator Earthquake()
    {
        Debug.Log("CamShake");
        yield return new WaitForSeconds(timerimmortal);
        Debug.Log("StopCam");
    }

    private IEnumerator CD()
    {
        yield return new WaitForSeconds(CDTimer);
        onCD = false;
    }

    private IEnumerator QuakeTimer(GameObject quaker)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(quaker);
    }

    private IEnumerator SpeedEffect()
    {
        player.GetComponent<PlayerMovement>().ChangeSpeed(fastEffect);
        yield return new WaitForSeconds(fastTimer);
        player.GetComponent<PlayerMovement>().ChangeSpeed(-fastEffect);
    }

    private IEnumerator JumpEffect()
    {
        player.GetComponent<PlayerMovement>().ChangeJump(jumpEffect);
        yield return new WaitForSeconds(jumpTimer);
        player.GetComponent<PlayerMovement>().ChangeJump(-jumpEffect);
    }

}
