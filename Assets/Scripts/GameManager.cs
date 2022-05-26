using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Image _currentCharacterImage1;
    [SerializeField] private Sprite[] _characterImage;

    [SerializeField] private Text _originalText;
    [SerializeField] private Text _translateText;

    [SerializeField] private Button[] _buttonsCharacter;

    [SerializeField] private AudioClip[] _charactersAudioClipArray;
    [SerializeField] private AudioClip[] _wordsAudioClipArray;
    [SerializeField] private AudioSource source;

    private List<string> _wordByChar = new List<string>();
    private static int _indexInArray = 0;

    [SerializeField] private string[] _charactersCapital;
    [SerializeField] private string[] _charactersCursive = new string[39];

    [SerializeField] private static int _currentCharacterNumber;
    [SerializeField] private string[] _originalWords;
    [SerializeField] private string[] _translateWords;
    [SerializeField] private static int _currentWordNumber;

    private void Start()
    {
        for (int i = 0; i < _charactersCapital.Length; i++)
        {
            _charactersCursive[i] = _charactersCapital[i].ToLower();
        }

        _currentWordNumber = 0;
        _currentCharacterNumber = 0;
        _originalText.text = _originalWords[_currentCharacterNumber];
        _translateText.text = _translateWords[_currentCharacterNumber];

        SeparateWordOnCharacters();


        for (int i = 0; i < _buttonsCharacter.Length; i++)
        {
            int closureIndex = i;
            _buttonsCharacter[closureIndex].onClick.AddListener(() => TaskOnClick(closureIndex));
        }
    }
    public void TaskOnClick(int buttonIndex)
    {
        SoundCharacter(buttonIndex);

        CheckCharacterInWord(buttonIndex);

    }

    public void NextCharacter()
    {
        if(_currentCharacterNumber != _charactersCapital.Length - 1)
        {
            _currentCharacterNumber++;
            _currentWordNumber = _currentCharacterNumber;
            ChangeCharacterImage(_currentCharacterNumber);
            ChangeTartgetWordText(_currentCharacterNumber);
            ChangeTranslatetWordText(_currentCharacterNumber);
            SeparateWordOnCharacters();
        }
    }

    public void LastCharacter()
    {
        if (_currentCharacterNumber != 0)
        {
            _currentCharacterNumber--;
            _currentWordNumber = _currentCharacterNumber;
            ChangeCharacterImage(_currentCharacterNumber);
            ChangeTartgetWordText(_currentCharacterNumber);
            ChangeTranslatetWordText(_currentCharacterNumber);
            SeparateWordOnCharacters();
        }
    }

    private void SeparateWordOnCharacters()
    {
        _wordByChar.Clear();
        foreach (char item in _originalWords[_currentCharacterNumber])
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
        _originalText.text = _originalWords[wordNum];
    }
    private void ChangeTranslatetWordText(int wordNum)
    {
        _translateText.text = _translateWords[wordNum];
    }

    private void SoundCharacter(int soundNum)
    {
        if (soundNum < _charactersAudioClipArray.Length)
        {
            source.clip = _charactersAudioClipArray[soundNum];
            source.PlayOneShot(source.clip);
            source.Play();
        }
    }

    private void CheckCharacterInWord(int charNum)
    {
        if(_indexInArray == 0)
        {
            if (_charactersCapital[charNum] == _wordByChar[_indexInArray])
            {
                _wordByChar[_indexInArray] = "<color=#FF0000>" + _wordByChar[_indexInArray] + "</color>";
                string result = string.Join("",_wordByChar);
                _originalText.text = result;
                _indexInArray++;
            }
        }
        else
        {
            if (_charactersCursive[charNum] == _wordByChar[_indexInArray])
            {
                _wordByChar[_indexInArray] = "<color=#FF0000>" + _wordByChar[_indexInArray] + "</color>";
                string result = string.Join("", _wordByChar);
                _originalText.text = result;
                _indexInArray++;
                if (_indexInArray == _wordByChar.Count())
                {
                    OnEndWord();
                }
            }
        }

    }

    private void OnEndWord()
    {

        _indexInArray = 0;
        _wordByChar.Clear();
        source.clip = _wordsAudioClipArray[0];
        source.PlayOneShot(source.clip);
        source.Play();
        NextCharacter();
    }
}
