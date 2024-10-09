using UnityEngine;

public class SoundHolder : MonoBehaviour
{
    public static SoundHolder Instance;

    [Header ("Spell Sounds")]
    public AudioClip[] HealSpell;
    public AudioClip[] SpeedSpell;
    public AudioClip[] JumpSpell;
    public AudioClip[] ShieldSpell;
    public AudioClip[] FireSpell;
    public AudioClip[] WindSpell;
    public AudioClip[] EarthQuakeSpell;

    [Header ("Player Sounds")]
    public AudioClip[] PlayerMove;
    public AudioClip[] PlayerLand;
    public AudioClip[] PlayerHurt;
    public AudioClip[] PlayerDie;
    public AudioClip[] PickUpSouls;


    [Header("Enemy Sounds")]
    public AudioClip[] EnemyHurt;
    public AudioClip[] EnemyDie;
    public AudioClip[] EnemyRangedAttack;
    // public AudioClip[] EnemyRangedAttack;

    [Header ("drums")]
    public AudioClip[] SilentDrum;
    public AudioClip[] StronfDrum;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySound(AudioClip[] currentSound)
    {
        SoundManager.instance.PlaySound(currentSound[Random.Range(0, currentSound.Length)]);
    }
}
