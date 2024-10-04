using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound;

    [SerializeField] private AttackEffect[] attacks;
    [SerializeField] private float attackTimer;

    private bool startAttack;
    [SerializeField] private bool dontAttack;
    [SerializeField] private int currentAttackTick;
    [SerializeField]private List<int> currentAttackList;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private float betweenCoolDown;
    [SerializeField] private GameObject silent, strong;
    [SerializeField] private Color scolor, stcolor;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack(0);
            //StartCoroutine(ShowAndFade(silent));
        }
        if (Input.GetMouseButtonDown(1))
        {
            Attack(1);
           // StartCoroutine(ShowAndFade(strong));
        }
 

        /*if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack()
            && Time.timeScale > 0)
            Attack();

        cooldownTimer += Time.deltaTime;*/
    }

    /*private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }*/
    private void Attack(int Comb)
    {
        if (!startAttack)
        {
            startAttack = true;
            StartCoroutine(AttackCount());
        }
        if (!dontAttack)
        {
            currentAttackList.Add(Comb);
            dontAttack = true;
            StartCoroutine(BetweenAttacks());
            currentAttackTick++;
        }

        if (currentAttackTick == 3)
        {
            StopCoroutine(AttackCount());
            CheckWhatAttack();

        }
    }

    private void PurifyAttack()
    {
        startAttack = false;
        currentAttackTick = 0;
        currentAttackList.Clear();

    }

    private IEnumerator AttackCount()
    {
        yield return new WaitForSeconds(attackTimer);
        PurifyAttack();
    }
    private IEnumerator BetweenAttacks()
    {
        yield return new WaitForSeconds(betweenCoolDown);
        dontAttack = false;

    }
    private IEnumerator fadeOut(GameObject fader)
    {
        yield return null;
    }


    void CheckWhatAttack()
    {
        bool basicAttack = true;
        string combination = null;
        Debug.Log(combination);
        for (int i = 0; i< currentAttackList.Count; i++)
        {
            combination += currentAttackList[i].ToString();
        }
        Debug.Log(combination);
        for (int i = 1;i<attacks.Length;i++)
        {
            Debug.Log(attacks[i].ReturnCombination(combination));
            if (attacks[i].ReturnCombination(combination))
            {
                attacks[i].ActivateAttack();
                basicAttack = false;
                return;
            }
        }
        if (basicAttack)
        {
            attacks[0].ActivateAttack();
        }
        PurifyAttack();


    }

    private IEnumerator ShowAndFade(GameObject fader) 
    {
        fader.SetActive(true);
        yield return new WaitForSeconds(1f);
        fader.SetActive(false);
    }

}