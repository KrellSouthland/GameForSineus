using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Linq;
using UnityEngine.UI;
using System.Diagnostics;

public class DialogueSystem : MonoBehaviour
{ 
    [SerializeField] string[] dialogueArray;  
   
    [SerializeField] TextMeshProUGUI _textUI; 
    [SerializeField] TextMeshProUGUI _nameUI;
    [SerializeField] TextMeshProUGUI _lightUI;

    [SerializeField] GameObject _dialoguePanel;
    [SerializeField] Image _characterAvatar;

    [SerializeField] public static GameObject instance;

    [SerializeField] int dialoguePhrase = 0; 

    [SerializeField] private float _betweenHalf = 0.05f;
    [SerializeField] private float _betweenChar = 0.03f;
    [SerializeField] private float _smoothTime = 0.1f;

    [SerializeField] private bool _dialogueIsActive = false;

    [SerializeField] private Animator _animator;

    private List<float> _leftAlphas;
    private List<float> _rightAlphas;

    private bool _isAnimating = false;
    
    void Start()
    {
        instance = gameObject;
        _dialoguePanel = transform.GetChild(0).gameObject;

        _textUI.SetText("");
        _lightUI.gameObject.SetActive(false);
        CountAlphas();

        _dialoguePanel.SetActive(false);
        _animator = GetComponent<Animator>();
 
        if (_nameUI == null)
        {
            _nameUI = GameObject.Find("NameUI").GetComponent<TextMeshProUGUI>();
        } 

        if (_textUI == null)
        {
            _textUI = GameObject.Find("NameUI").GetComponent<TextMeshProUGUI>();
        }
        
    }
     
    public void InitDialogue(string[] _newDialogue, string _character, Sprite _avatar)
    {
        _lightUI.gameObject.SetActive(false);
        _nameUI.text = _character;
        dialogueArray = _newDialogue;
        _characterAvatar.sprite = _avatar;

        dialoguePhrase = 0;
        _dialogueIsActive = true;
        _isAnimating = true;
        _dialoguePanel.SetActive(true);

        _textUI.SetText(dialogueArray[dialoguePhrase]);

        // Инициализация альфа-каналов
        CountAlphas();

        StartCoroutine(Smooth(0));
        _animator.SetBool("IsOpening", true);
    }
  

    void CloseDialogue()
    {
        _animator.SetBool("IsOpening", false);
        _animator.SetBool("IsClosing", false);

        _dialoguePanel.SetActive(false);
        _dialogueIsActive = false;
    }

    void CountAlphas()
    {
        _leftAlphas = new float[_textUI.text.Length].ToList();
        _rightAlphas = new float[_textUI.text.Length].ToList();
    }
    
    void Update()
    {
        if (_dialogueIsActive)
        {
            if (_isAnimating)
                SwitchColor();

            if (Input.GetMouseButtonDown(0) && Time.timeScale ==1)
            {
                if (_isAnimating)
                {
                    _isAnimating = false;
                    Visible(true);
                }
                else
                {
                    SwitchPhrase();
                }
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
            _animator.SetBool("IsClosing", true);
        }
    }

    private void SwitchColor()
    {
        UnityEngine.Debug.Log("Switch Color");
        for (int i = 0; i < _leftAlphas.Count; i++)
        {
            if (i >= _textUI.textInfo.characterInfo.Length)
                break;

            if (_textUI.textInfo.characterInfo[i].character != '\n' &&
                _textUI.textInfo.characterInfo[i].character != ' ')
            {
                int meshIndex = _textUI.textInfo.characterInfo[i].materialReferenceIndex;

                if (meshIndex >= _textUI.textInfo.meshInfo.Length)
                    break;

                int vertexIndex = _textUI.textInfo.characterInfo[i].vertexIndex;

                Color32[] vertexColors = _textUI.textInfo.meshInfo[meshIndex].colors32;

                if (vertexIndex + 3 >= vertexColors.Length)
                    break;

                vertexColors[vertexIndex + 0].a = (byte)_leftAlphas[i];
                vertexColors[vertexIndex + 1].a = (byte)_leftAlphas[i];
                vertexColors[vertexIndex + 2].a = (byte)_rightAlphas[i];
                vertexColors[vertexIndex + 3].a = (byte)_rightAlphas[i];
            }
            _textUI.UpdateVertexData();   
        }
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

    public static bool isOpen()
    {
        if (instance != null)
            return instance.gameObject.GetComponent<DialogueSystem>()._dialoguePanel.active;

        return false;
    }
    
}
