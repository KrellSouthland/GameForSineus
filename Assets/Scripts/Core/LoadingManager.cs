using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager instance { get; private set; }
    private Animator animator;
    int currentLevel;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Debug.Log("dont destroy!!!!!");
        //Keep this object even when we go to new scene
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //Destroy duplicate gameobjects
        else if (instance != null && instance != this)
            Destroy(gameObject);
         
    }

    private void Start()
    {
        Debug.Log("saasdasd1!!!!!");
        animator = GetComponent<Animator>(); 
    }
      
    public void LoadCurrentLevel()
    {
        Debug.Log("Load Current Level");
        animator.SetBool("FadeIn", true); 
        currentLevel = PlayerPrefs.GetInt("currentLevel", 1); 
    }
    public void LoadDefineLevel(int level)
    {
        Debug.Log("Load Define Level");
        animator.SetBool("FadeIn", true);
        currentLevel = level;
    }
    public void LoadTheSceneEvent()
    {
        Debug.Log("LoadTheSceneEvent");
        SceneManager.LoadScene(currentLevel); 
    }

    public void FadeOutEvent()
    {
        animator.SetBool("FadeOut", false);
        animator.SetBool("FadeIn", false);
    }
    public void Restart()
    {
        //SceneManager.LoadScene(currentLevel);
    }
}