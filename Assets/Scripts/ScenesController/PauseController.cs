using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [Header("Esc키 눌렀을 때 뜨는 화면")]
    [SerializeField] private GameObject _pauseMenu;
    
    [SerializeField] private Button _continueGameButton;
    [SerializeField] private Button _returnToMainButton;
    [SerializeField] private Button _skillsButton;
    [SerializeField] private Button _exitGameButton;
    [SerializeField] private Button _optionButton;

    [Header("이동할 Scene 연결")] 
    [SerializeField] private string _moveToMainTitle;
    
    [Header("SkillsController 연결")]
    [SerializeField] private SkillsController _skillsController;

    private bool _isLoadingSkills;
    
    private void Start()
    {
        _pauseMenu.SetActive(false);
        
        _continueGameButton.onClick.AddListener(ContinueGame);
        _returnToMainButton.onClick.AddListener(ReturnToMain);
        _skillsButton.onClick.AddListener(OpenSkills);
        _exitGameButton.onClick.AddListener(ExitGame);
    }
    
    private void Update()
    {
        if (SceneManager.GetSceneByName("Skills").isLoaded)
            return;
        
        if (Keyboard.current != null && 
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            bool isPause = !_pauseMenu.activeSelf;
            
            _pauseMenu.SetActive(!_pauseMenu.activeSelf);

            if (isPause)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }

    private void ContinueGame()
    {
        _pauseMenu.SetActive(false);
        
        Time.timeScale = 1f;
    }

    private void ReturnToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("_moveToMainTitle");
    }

    private void OpenSkills()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 0f;

        _skillsController.OpenSkills();
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
