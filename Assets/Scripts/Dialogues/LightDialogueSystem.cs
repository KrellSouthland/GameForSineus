using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Linq;
using UnityEngine.UI;
using System.Diagnostics;

public class LightDialogueSystem : MonoBehaviour
{ 
    [SerializeField] string[] dialogueArray;  
   
    [SerializeField] TextMeshProUGUI _textUI; 

    [SerializeField] public static GameObject instance;

    [SerializeField] int dialoguePhrase = 0; 

    [SerializeField] private float _betweenHalf = 0.05f;
    [SerializeField] private float _betweenChar = 0.03f;
    [SerializeField] private float _smoothTime = 0.1f;

    [SerializeField] private bool _dialogueIsActive = false;

    private List<float> _leftAlphas;
    private List<float> _rightAlphas;

    private bool _isAnimating = false;

    void Start()
    {
        instance = gameObject;
  
        CountAlphas();

        if (_textUI == null)
        {
            _textUI = GameObject.Find("TextUILight").GetComponent<TextMeshProUGUI>();
        } 
    }
     
    public void InitDialogue(string[] _newDialogue)
    {
        dialogueArray = _newDialogue;

        dialoguePhrase = 0;
        _dialogueIsActive = true;
        _isAnimating = true;
        _textUI.gameObject.SetActive(true);
        _textUI.SetText(dialogueArray[dialoguePhrase]);

        CountAlphas();

        StartCoroutine(Smooth(0));
    }
  

    void CloseDialogue()
    {
        _textUI.SetText("");
        _dialogueIsActive = false;
        _textUI.gameObject.SetActive(false);
    }

    void CountAlphas()
    {
        if (_textUI.text.Length == 0)
        {
            UnityEngine.Debug.Log("EMPTY COUNT ALPHAS");
            return;
        }
        _leftAlphas = new float[_textUI.text.Length].ToList();
        _rightAlphas = new float[_textUI.text.Length].ToList();
    }
    
    void Update()
    {
        if (_dialogueIsActive)
        {
            if (_isAnimating)
            {
                SwitchColor();
            }

            if (_leftAlphas[_leftAlphas.Count-1] >= 255) {

                StartCoroutine(Wait());
                
            }
             
        }
    }

    private void Visible(bool Visible)
    {
        StopAllCoroutines();
        DOTween.Kill(1);

        for (int i = 0; i < _leftAlphas.Count; i++)
        {
            _leftAlphas[i] = Visible ? 255 : 0;
            _rightAlphas[i] = Visible ? 255 : 0;
        }

        SwitchColor();
    }

    void SwitchPhrase (){
        int arraySize = dialogueArray.Length;

        if (dialoguePhrase < arraySize-1)
        { 
            _isAnimating = true;
            Visible(false);
            StartCoroutine(Smooth(0));
            dialoguePhrase++;
            _textUI.SetText(dialogueArray[dialoguePhrase]);
            CountAlphas();
        }
        else
        { 
            CloseDialogue();
        }
    }

    private void SwitchColor()
    {
        UnityEngine.Debug.Log("Switch Color");
        int characterCount = _textUI.textInfo.characterCount;

        for (int i = 0; i < characterCount && i < _leftAlphas.Count; i++)
        {
            if (_textUI.textInfo.characterInfo[i].character != '\n' &&
                _textUI.textInfo.characterInfo[i].character != ' ')
            {
                int meshIndex = _textUI.textInfo.characterInfo[i].materialReferenceIndex;
                int vertexIndex = _textUI.textInfo.characterInfo[i].vertexIndex;

                Color32[] vertexColors = _textUI.textInfo.meshInfo[meshIndex].colors32;

                vertexColors[vertexIndex + 0].a = (byte)_leftAlphas[i];
                vertexColors[vertexIndex + 1].a = (byte)_leftAlphas[i];
                vertexColors[vertexIndex + 2].a = (byte)_rightAlphas[i];
                vertexColors[vertexIndex + 3].a = (byte)_rightAlphas[i];
            }
            _textUI.UpdateVertexData();   
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        SwitchPhrase();
    }

    private IEnumerator Smooth(int i)
    {
        if (i >= _leftAlphas.Count || _leftAlphas == null || _rightAlphas == null)
        {
            yield break;
        }

        DOTween.To(
            () => _leftAlphas[i],
            x => _leftAlphas[i] = x, 255,
            _smoothTime)
            .SetEase(Ease.Linear)
            .SetId(1);
        yield return new WaitForSeconds(_betweenHalf);
        SwitchColor();
        DOTween.To(
            () => _rightAlphas[i],
            x => _rightAlphas[i] = x, 255,
            _smoothTime)
            .SetEase(Ease.Linear)
            .SetId(1);
        yield return new WaitForSeconds(_betweenChar);
        SwitchColor();
        StartCoroutine(Smooth(i + 1));

    }

 
    
}
