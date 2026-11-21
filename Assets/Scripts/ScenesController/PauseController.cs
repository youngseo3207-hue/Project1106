using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [Header("Esc키 눌렀을 때 뜨는 화면")]
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private Button _continueGameButton;
    [SerializeField] private Button _returnToMainButton;
    [SerializeField] private Button _exitGameButton;
    [SerializeField] private Button _optionButton;
   
    [Header("공통 화면")]
    [SerializeField] private GameObject _backGroundPanel;
    [SerializeField] private GameObject[] _inGameIcons;
    [SerializeField] private string _moveToMainTitle;

    [Header("옵션-볼륨 조절 창")]
    [SerializeField] private GameObject _optionPanel;
    [SerializeField] private Button _optionCloseButton;
    [SerializeField] private Button _optionIconButton;
    
    private bool _isOpenOption;
    
    private void Start()
    {
        SetActiveSafe(_pauseMenu, false);
        SetActiveSafe(_optionPanel, false);
        SetActiveSafe(_backGroundPanel, false);
       
        if(_continueGameButton != null) _continueGameButton.onClick.AddListener(ResumeGame);
        if(_returnToMainButton != null) _returnToMainButton.onClick.AddListener(ReturnToMain);
        if (_exitGameButton != null) _exitGameButton.onClick.AddListener(ExitGame);

        if (_optionButton != null) _optionButton.onClick.AddListener(OpenOptionFromPause);
        if (_optionCloseButton != null) _optionCloseButton.onClick.AddListener(CloseOption);
        if (_optionIconButton != null) _optionIconButton.onClick.AddListener(TogglePauseMenu);
    }
    
    private void Update()
    {
        if (SceneManager.GetSceneByName("Skills").isLoaded)
            return;
        
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            HandleEscapeKey();
        }
    }

    private void HandleEscapeKey()
    {
        Debug.Log($"ESC 눌림 | Option:{_optionPanel?.activeSelf}, Pause:{_pauseMenu?.activeSelf}");

        // Option 창이 열려 있다면 -> Option 창부터 닫기
        if (_optionPanel != null && _optionPanel.activeSelf)
        {
            CloseOption();
            return;
        }

        TogglePauseMenu();
    }

    public void TogglePauseMenu()
    {
        // Pause 메뉴가 열려 있다면 -> 게임 재개, 닫혀 있다면 -> Pause 메뉴 열기
        if (_pauseMenu != null && _pauseMenu.activeSelf)
        {
            ResumeGame();
        }
        else
        {
            OpenPauseMenu();
        }
    }

    public void OpenPauseMenu()
    {
        Debug.Log("Option Button 누름");
        _isOpenOption = true;
        
        SetActiveSafe(_pauseMenu, true);
        SetActiveSafe(_optionPanel, false);
        SetActiveSafe(_backGroundPanel, true);
        SetInGameIconsActive(false);
        
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Debug.Log("Resume 호출");
        _isOpenOption = false;
        
        SetActiveSafe(_pauseMenu, false);
        SetActiveSafe(_optionPanel, false);
        SetActiveSafe(_backGroundPanel, false);
        SetInGameIconsActive(true);
        
        Time.timeScale = 1f;
    }

    public void OpenOptionFromPause()
    {
        if (!_isOpenOption)
        {
            return;
        }
        
        SetActiveSafe(_pauseMenu, false);
        SetActiveSafe(_optionPanel, true);
        SetActiveSafe(_backGroundPanel, true);
        SetInGameIconsActive(false);
        
        Time.timeScale = 0f;
        
        EventSystem.current.SetSelectedGameObject(null);
    }
    
    
    
    public void CloseOption()
    {
        Debug.Log($"CloseOption 진입 | _canOpenOption:{_isOpenOption}, _pauseMenu:{_pauseMenu?.name}");
        
        SetActiveSafe(_pauseMenu, true);
        SetActiveSafe(_optionPanel, false);
        SetActiveSafe(_backGroundPanel, true);
        SetInGameIconsActive(false);
        _isOpenOption = false;
        
        Time.timeScale = 0f;
        
        Debug.Log($"CloseOption 완료 후 | Option:{_optionPanel?.activeSelf}, Pause:{_pauseMenu?.activeSelf}");
    }

    private void ReturnToMain()
    {
        _isOpenOption = false;
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(_moveToMainTitle);
    }

    private void ExitGame()
    {
        _isOpenOption = false;
        Time.timeScale = 1f;
        
        Application.Quit();
     
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif        
    }

    private void SetActiveSafe(GameObject target, bool isActive)
    {
        if (target != null)
        {
            target.SetActive(isActive);
        }
    }

    private void SetInGameIconsActive(bool isActive)
    {
        if (_inGameIcons == null) return;

        foreach (GameObject icon in _inGameIcons)
        {
            SetActiveSafe(icon, isActive);
        }
    }
}
