using UnityEngine;

public class EnemyHealthbar : MonoBehaviour
{
    public float amountX, amountXtoLerp;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
        amountX = 0;
        amountXtoLerp = 1;
    }

    public void UpdateHealthbar(float _hp, float _maxHp)
    { 
        amountXtoLerp = _hp / _maxHp;  
    }
     
    void Update()
    {
        

        amountX = Mathf.Lerp(amountX, amountXtoLerp, 0.01f);
        gameObject.transform.localScale = new Vector3(amountX * 2, 1, 1);
        
    }
}
