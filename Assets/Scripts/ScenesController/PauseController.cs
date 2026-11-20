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
    // [SerializeField] private Button _skillsButton;
    [SerializeField] private Button _exitGameButton;
    [SerializeField] private Button _optionButton;

    [Header("이동할 Scene 연결")] 
    [SerializeField] private string _moveToMainTitle;
    
    [Header("SkillsController 연결")]
    [SerializeField] private SkillsController _skillsController;
    
    [Header("배경 색")]
    [SerializeField] private GameObject _backGroundPanel;

    [Header("인게임 아이콘")] 
    [SerializeField] private GameObject[] _inGameIcons;

    [Header("옵션 창")]
    [SerializeField] private GameObject _optionPanel;
    [SerializeField] private Button _optionCloseButton;
    
    private bool _isLoadingSkills;
    
    private void Start()
    {
        _pauseMenu.SetActive(false);
        
        _continueGameButton.onClick.AddListener(ContinueGame);
        _returnToMainButton.onClick.AddListener(ReturnToMain);
        _exitGameButton.onClick.AddListener(ExitGame);

        if (_backGroundPanel != null)
        {
            _backGroundPanel.SetActive(false);
        }

        if (_optionPanel != null)
        {
            _optionPanel.SetActive(false);
        }

        if (_optionButton != null)
        {
            _optionButton.onClick.AddListener(OpenOptionMenu);
        }

        if (_optionCloseButton != null)
        {
            _optionCloseButton.onClick.AddListener(CloseOptionMenu);
        }
    }
    
    private void Update()
    {
        if (SceneManager.GetSceneByName("Skills").isLoaded)
            return;
        
        if (Keyboard.current != null && 
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (_optionPanel != null && _optionPanel.activeSelf)
            {
                CloseOptionMenu();
                return;
            }
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
    
    // private void OpenSkills()
    // {
    //     if (_pauseMenu != null)
    //     {
    //         _pauseMenu.SetActive(false);
    //     }
    //     
    //     openMenu();
    //
    //     if (_skillsButton != null)
    //     {
    //         _skillsController.OpenSkills();
    //     }
    //     // gameObject.SetActive(true);
    //     // Time.timeScale = 0f;
    //     
    //     // Debug.Log("_pauseMenu: " + _pauseMenu);
    //     // Debug.Log("_skillsController: " + _skillsController);
    //     //
    //     // _pauseMenu.SetActive(false);
    //     // Time.timeScale = 0f;
    //     //
    //     // _skillsController.OpenSkills();
    // }

    private void CloseSkills()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    private void openMenu()
    {
        Debug.Log("open menu 호출 됨");
        if (_backGroundPanel != null)
        {
            _backGroundPanel.SetActive(true);
        }
        SetInGameIconsActive(false);
        Time.timeScale = 0f;
    }

    private void closeMenu()
    {
        if (_backGroundPanel != null)
        {
            _backGroundPanel.SetActive(false);
        }
        SetInGameIconsActive(true);
        Time.timeScale = 1f;
    }

    private void SetInGameIconsActive(bool isActive)
    {
        if (_inGameIcons == null) return;

        foreach (GameObject icon in _inGameIcons)
        {
            if(icon != null)
            {
                icon.SetActive(isActive);
            }
        }
    }

    public void OpenOptionMenu()
    {
        Debug.Log("Option 호출");
        if (_pauseMenu != null)
        {
            _pauseMenu.SetActive(false);
        }

        if (_backGroundPanel != null)
        {
            _backGroundPanel.SetActive(true);
        }

        if (_optionPanel != null)
        {
            _optionPanel.SetActive(true);
        }
        
        SetInGameIconsActive(false);
        Time.timeScale = 0f;
    }

    private void CloseOptionMenu()
    {
        if (_optionPanel != null)
        {
            _optionPanel.SetActive(false);
        }

        if (_pauseMenu != null)
        {
            _pauseMenu.SetActive(true);
        }

        if (_backGroundPanel != null)
        {
            _backGroundPanel.SetActive(true);
        }
        
        SetInGameIconsActive(false);
        Time.timeScale = 0f;
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
