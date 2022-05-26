using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Image _currentCharacterImage1;
    [SerializeField] private Sprite[] _characterImage;

    [SerializeField] private Text _originalText;
    [SerializeField] private Text _translateText;

    [SerializeField] private Button[] _buttonsCharacter;

    [SerializeField] private AudioClip[] audioClipArray;
    [SerializeField] private AudioSource source;

    [SerializeField] private string[] _characters;
    [SerializeField] private static int _currentCharacterNumber;
    [SerializeField] private string[] _originalWords;
    [SerializeField] private string[] _translateWords;
    [SerializeField] private static int _currentWordNumber;

    private void Start()
    {
        _currentWordNumber = 0;
        _currentCharacterNumber = 0;

        for (int i = 0; i < _buttonsCharacter.Length; i++)
        {
            int closureIndex = i;
            _buttonsCharacter[closureIndex].onClick.AddListener(() => TaskOnClick(closureIndex));
        }
    }
    public void TaskOnClick(int buttonIndex)
    {
        SoundCharacter(buttonIndex);

        Debug.Log("You have clicked the button #" + buttonIndex, _buttonsCharacter[buttonIndex]);
    }

    public void NextCharacter()
    {
        if(_currentCharacterNumber != _characters.Length - 1)
        {
            _currentCharacterNumber++;
            _currentWordNumber = _currentCharacterNumber;
            ChangeCharacterImage(_currentCharacterNumber);
            ChangeTartgetWordText(_currentCharacterNumber);
            ChangeTranslatetWordText(_currentCharacterNumber);
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
        source.clip = audioClipArray[soundNum];
        source.PlayOneShot(source.clip);
        source.Play();
    }
}
