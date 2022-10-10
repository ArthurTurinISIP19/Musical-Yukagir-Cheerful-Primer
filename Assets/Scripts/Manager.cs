using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Manager : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] private Image _currentCharacterImage1;
    [SerializeField] private Sprite[] _characterImage;

    [Header("Fields of Texts")]
    [SerializeField] private TextMeshProUGUI _originalText;
    [SerializeField] private TextMeshProUGUI _translateText;

    [Header("Array of Buttons")]
    [SerializeField] private Button[] _buttonsCharacter;

    [Header("All Characters of Alphabit")]
    [SerializeField] private string[] _charactersCapital;

    [Header("Class of Words (Name, Translate, Array of Characters)")]
    [SerializeField] private List<Word> _allWords = new List<Word>();

    [Header("SoundManager")]
    [SerializeField] private SoundManager _soundManager;

    private List<string> _wordByChar = new List<string>();
    private static int _currentWordNumber;
    private static int _characterIndexInWord;
    public static int _currentCharacterIndexInAphabit;

    [SerializeField] private Color _showTranslateColor;
    private static string _colorRedBegin = "<color=#f55f5f>";
    private static string _colorRedEnd = "</color>";

    private IEnumerator WaitAndSoundCoroutine;
    private IEnumerator ShowTranslateCoroutine;


    private void Start()
    {
        _currentWordNumber = 0;
        _currentCharacterIndexInAphabit = 0;
        _originalText.text = _allWords[_currentCharacterIndexInAphabit]._name;
        _translateText.text = _allWords[_currentCharacterIndexInAphabit]._translateName;
        _translateText.alpha = 0;

        //WaitAndSoundCoroutine = StartCoroutine(WaitAndSound());
        //ShowTranslateCoroutine = StartCoroutine(ShowTranslate());

        SeparateWordOnCharacters();
        AddListenersToButtons();
    }

    private void AddListenersToButtons()
    {
        for (int i = 0; i < _buttonsCharacter.Length; i++)
        {
            int closureIndex = i;
            _buttonsCharacter[closureIndex].onClick.AddListener(() => CheckCharacterInWord(closureIndex));
        }
    }

    public void NextCharacter()
    {
        if(_currentCharacterIndexInAphabit != _charactersCapital.Length - 1)
        {
            _currentCharacterIndexInAphabit++;
            ChangeCharacter();
        }
    }

    public void LastCharacter()
    {
        if (_currentCharacterIndexInAphabit != 0)
        {
            _currentCharacterIndexInAphabit--; 
            ChangeCharacter();
        }
    }

    private void ChangeCharacter()
    {
        //if (ShowTranslateCoroutine != null)
        //    StopCoroutine(ShowTranslateCoroutine);
        //    _translateText.alpha = 0f;
        StopAllCoroutines();
        _translateText.alpha = 0f;

        _currentWordNumber = _currentCharacterIndexInAphabit;
        ChangeCharacterImage(_currentCharacterIndexInAphabit);
        ChangeTartgetWordText(_currentCharacterIndexInAphabit);
        ChangeTranslatetWordText(_currentCharacterIndexInAphabit);
        SeparateWordOnCharacters();
        _characterIndexInWord = 0;
    }

    private void SeparateWordOnCharacters()
    {
        _wordByChar.Clear();

        foreach (string item in _allWords[_currentCharacterIndexInAphabit]._characters)
        {
            _wordByChar.Add(item.ToString());
        }
    }

    private void ChangeCharacterImage(int charNum)
    {
      _currentCharacterImage1.sprite = _characterImage[charNum];
    }

    private void ChangeTartgetWordText(int wordNum)
    {
        _originalText.text = _allWords[wordNum]._name;

    }
    private void ChangeTranslatetWordText(int wordNum)
    {
        _translateText.alpha = 0;
        _translateText.text = _allWords[wordNum]._translateName;
    }

    private void SoundCharacter(int soundNum)
    {
        _soundManager.SoundCharacter(soundNum);
    }

    private void CheckCharacterInWord(int charNum)
    {
        if (_charactersCapital[charNum] == _allWords[_currentCharacterIndexInAphabit]._characters[_characterIndexInWord] 
            || _charactersCapital[charNum].ToLower() == _allWords[_currentCharacterIndexInAphabit]._characters[_characterIndexInWord])
        {
            SoundCharacter(charNum);
            PaintAndFillCharacterInWord();
        }
    }

    private void PaintAndFillCharacterInWord()
    {
        _wordByChar[_characterIndexInWord] = _colorRedBegin + _allWords[_currentCharacterIndexInAphabit]._characters[_characterIndexInWord] + _colorRedEnd;
        string result = string.Join("", _wordByChar);
        _originalText.text = result;
        _characterIndexInWord++;
        if (_characterIndexInWord == _wordByChar.Count())
        {
            OnEndWord();
        }
    }

    private void OnEndWord()
    {
        _translateText.color = Color.Lerp(_translateText.color, _showTranslateColor, Time.deltaTime * 0.5f);

        _characterIndexInWord = 0;
        _wordByChar.Clear();
        StartCoroutine(WaitAndSound());
        StartCoroutine(ShowTranslateCoroutine = ShowTranslate());
    }

    private IEnumerator WaitAndSound()
    {
        yield return new WaitForSeconds(0.5f);
        _soundManager.SoundFullWord(_currentWordNumber);
        yield return new WaitForSeconds(1f);
        NextCharacter();
    }

    private IEnumerator ShowTranslate()
    {
        yield return new WaitForEndOfFrame();

        ShowTranslateCoroutine = ShowTranslate();
        if (_translateText.alpha <= 0.99f)
        {
            _translateText.color = Color.Lerp(_translateText.color, _showTranslateColor, 0.02f);
            StartCoroutine(ShowTranslateCoroutine);
        }
        else
        {
            _translateText.alpha = 1f;
            StopCoroutine(ShowTranslateCoroutine);
        }
    }    
}
