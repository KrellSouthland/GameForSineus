using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;

    [SerializeField] private AttackEffect[] attacks;
    [SerializeField] private float attackTimer;
    private bool stopCasts;
    [SerializeField] private bool startAttack;
    [SerializeField] private bool dontAttack;
    [SerializeField] private int currentAttackTick;
    [SerializeField]private List<int> currentAttackList;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private float betweenCoolDown;

    [SerializeField] private ShowMagic spells;

    [Header("Sounds")]
    [SerializeField] private AudioClip silentSound;
    [SerializeField] private AudioClip[] StrongSound;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(!DialogueSystem.isOpen())
        if (!stopCasts&&!dontAttack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                spells.ActivateStage(0,currentAttackTick);
                anim.SetTrigger("attack");
                Attack(0);
                SoundHolder.Instance.PlaySound(SoundHolder.Instance.SilentDrum);
            }
            if (Input.GetMouseButtonDown(1))
            {
                spells.ActivateStage(1, currentAttackTick);
                anim.SetTrigger("Strong Attack");
                Attack(1);
                SoundHolder.Instance.PlaySound(SoundHolder.Instance.StronfDrum);

                }
        }
        if (dontAttack)
        {
            CheckCast();
        }
    }
    private void Attack(int Comb)
    {
        if (!startAttack)
        {
            startAttack = true;
            StartCoroutine(AttackCount());
        }
 
        currentAttackList.Add(Comb);
        dontAttack = true;
        StartCoroutine(BetweenAttacks());
        currentAttackTick++;


        if (currentAttackTick >= 3)
        {
            stopCasts = true;
            StopAllCoroutines();
            CheckWhatAttack();

        }
    }

    private void PurifyAttack()
    {
        startAttack = false;
        currentAttackTick = 0;
        currentAttackList.Clear();
        spells.DeactivateStages();

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


    void CheckWhatAttack()
    {
        bool basicAttack = true;
        string combination = null;
        for (int i = 0; i< currentAttackList.Count; i++)
        {
            combination += currentAttackList[i].ToString();
        }
        Debug.Log(combination);
        for (int i = 1;i<attacks.Length;i++)
        {
            if (attacks[i].ReturnCombination(combination))
            {
                attacks[i].ActivateAttack();
                basicAttack = false;
            }
        }
        if (basicAttack)
        {
            attacks[0].ActivateAttack();
        }
        PurifyAttack();
        stopCasts = false;
    }

    private void CheckCast()
    {
        if (!spells.cantCast)
            dontAttack = false;

    }

}