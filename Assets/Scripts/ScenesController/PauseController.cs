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
    
    [Header("배경 색")]
    [SerializeField] private GameObject _backGroundPanel;

    private bool _isLoadingSkills;
    
    private void Start()
    {
        _pauseMenu.SetActive(false);
        
        _continueGameButton.onClick.AddListener(ContinueGame);
        _returnToMainButton.onClick.AddListener(ReturnToMain);
        _skillsButton.onClick.AddListener(OpenSkills);
        _exitGameButton.onClick.AddListener(ExitGame);

        if (_backGroundPanel != null)
        {
            _backGroundPanel.SetActive(false);
        }
    }
    
    private void Update()
    {
        if (SceneManager.GetSceneByName("Skills").isLoaded)
            return;
        
        if (Keyboard.current != null && 
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            bool isPause = !_pauseMenu.activeSelf;
            _pauseMenu.SetActive(isPause);

            if (isPause)
            {
                openMenu();
            }
            else
            {
                closeMenu();
            }
        }
    }

    private void ContinueGame()
    {
        _pauseMenu.SetActive(false);

        closeMenu();
    }

    private void ReturnToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(_moveToMainTitle);
    }

    private void OpenSkills()
    {
        if (_pauseMenu != null)
        {
            _pauseMenu.SetActive(false);
        }
        
        openMenu();

        if (_skillsButton != null)
        {
            _skillsController.OpenSkills();
        }
        // gameObject.SetActive(true);
        // Time.timeScale = 0f;
        
        // Debug.Log("_pauseMenu: " + _pauseMenu);
        // Debug.Log("_skillsController: " + _skillsController);
        //
        // _pauseMenu.SetActive(false);
        // Time.timeScale = 0f;
        //
        // _skillsController.OpenSkills();
    }

    private void CloseSkills()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    private void openMenu()
    {
        if (_backGroundPanel != null)
        {
            _backGroundPanel.SetActive(true);
        }
        Time.timeScale = 0f;
    }

    private void closeMenu()
    {
        if (_backGroundPanel != null)
        {
            _backGroundPanel.SetActive(false);
        }
        Time.timeScale = 1f;
    }
    
    private void ExitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif    
    }
}
