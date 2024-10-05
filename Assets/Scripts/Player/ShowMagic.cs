using System.Collections;
using UnityEngine;

public class ShowMagic : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] stages;
    [SerializeField] private Sprite[] power;
    public float deactivetime;
    public bool cantCast;

    public void ActivateStage(int strength, int tick)
    {
        if (tick < 3&&!cantCast)
        {
            stages[tick].gameObject.SetActive(true);
            stages[tick].sprite = power[strength];
            tick++;
        }
        if (tick==3)
        {
            cantCast = true;
        }
    }

    public void ForceDeactivate()
    {
        foreach (var stage in stages)
        {
            stage.gameObject.SetActive(false);

        }
        cantCast=false;
    }

    public void DeactivateStages()
    {
        StartCoroutine(DeactivateTimer());
    }
    private IEnumerator DeactivateTimer()
    {
        cantCast = true;
        yield return new WaitForSeconds(deactivetime);
        ForceDeactivate();
    }
}
