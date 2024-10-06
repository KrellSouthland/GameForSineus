 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KutMission : MonoBehaviour
{
    // Start is called before the first frame update

    public static KutMission instance;
    [SerializeField] private TMP_Text kutsText;
    [SerializeField] static int kutsCount = 10, kutsCollected = 0;
    [SerializeField] private GameObject dialogTrigger;

    void Start()
    {
        kutsCollected = 0;
        kutsText.gameObject.SetActive(false);

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        UpdateKutsStats();
    }
    public static void ActivateMission()
    {
        instance.kutsText.gameObject.SetActive(true);
    }
    public static void AddKut()
    {
        Debug.Log("Подобрал частицу");
        kutsCollected++;

        instance.UpdateKutsStats();
        if (kutsCollected >= kutsCount)
        {
            instance.kutsText.gameObject.SetActive(false);
            Instantiate(instance.dialogTrigger, GameObject.Find("Shaman").transform.position, Quaternion.identity);
        }
    }
    
    void UpdateKutsStats()
    {
        kutsText.SetText("Частиц душ собрано: " + kutsCollected.ToString() +"/"+ kutsCount.ToString());
    }
}
