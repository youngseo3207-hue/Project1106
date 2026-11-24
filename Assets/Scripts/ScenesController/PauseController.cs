using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [Header("Esc키 눌렀을 때 뜨는 화면")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button continueGameButton;
    [SerializeField] private Button returnToMainButton;
    [SerializeField] private Button exitGameButton;
    [SerializeField] private Button optionButton;
   
    [Header("공통 화면")]
    [SerializeField] private GameObject backGroundPanel;
    [SerializeField] private GameObject[] inGameIcons;
    [SerializeField] private string moveToMainTitle;

    [Header("옵션-볼륨 조절 창")]
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private Button optionCloseButton;
    [SerializeField] private Button optionIconButton;
    
    private bool _isOpenOption;
    
    private void Start()
    {
        SetActiveSafe(pauseMenu, false);
        SetActiveSafe(optionPanel, false);
        SetActiveSafe(backGroundPanel, false);
       
        if(continueGameButton != null) continueGameButton.onClick.AddListener(ResumeGame);
        if(returnToMainButton != null) returnToMainButton.onClick.AddListener(ReturnToMain);
        if (exitGameButton != null) exitGameButton.onClick.AddListener(ExitGame);

        if (optionButton != null) optionButton.onClick.AddListener(OpenOptionFromPause);
        if (optionCloseButton != null) optionCloseButton.onClick.AddListener(CloseOption);
        if (optionIconButton != null) optionIconButton.onClick.AddListener(TogglePauseMenu);
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
        if (optionPanel != null && optionPanel.activeSelf)
        {
            CloseOption();
            return;
        }

        TogglePauseMenu();
    }

    public void TogglePauseMenu()
    {
        if (pauseMenu != null && pauseMenu.activeSelf)
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
        
        SetActiveSafe(pauseMenu, true);
        SetActiveSafe(optionPanel, false);
        SetActiveSafe(backGroundPanel, true);
        SetInGameIconsActive(false);
        
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Debug.Log("Resume 호출");
        _isOpenOption = false;
        
        SetActiveSafe(pauseMenu, false);
        SetActiveSafe(optionPanel, false);
        SetActiveSafe(backGroundPanel, false);
        SetInGameIconsActive(true);
        
        Time.timeScale = 1f;
    }

    public void OpenOptionFromPause()
    {
        if (!_isOpenOption)
        {
            return;
        }
        
        SetActiveSafe(pauseMenu, false);
        SetActiveSafe(optionPanel, true);
        SetActiveSafe(backGroundPanel, true);
        SetInGameIconsActive(false);
        
        Time.timeScale = 0f;
        
        EventSystem.current.SetSelectedGameObject(null);
    }
    
    
    
    public void CloseOption()
    {
        SetActiveSafe(pauseMenu, false);
        SetActiveSafe(optionPanel, true);
        SetActiveSafe(backGroundPanel, true);

        Debug.Log($"CloseOption 진입 | _canOpenOption:{_isOpenOption}, _pauseMenu:{pauseMenu?.name}");
        
        SetActiveSafe(pauseMenu, true);
        SetActiveSafe(optionPanel, false);
        SetActiveSafe(backGroundPanel, true);
        SetInGameIconsActive(false);
        _isOpenOption = false;
        
        Time.timeScale = 0f;
    }

    private void ReturnToMain()
    {
        _isOpenOption = false;
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(moveToMainTitle);
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
        if (inGameIcons == null) return;

        foreach (GameObject icon in inGameIcons)
        {
            SetActiveSafe(icon, isActive);
        }
    }
}
