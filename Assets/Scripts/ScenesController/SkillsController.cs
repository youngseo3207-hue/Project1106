using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SkillsController : MonoBehaviour
{
    [SerializeField] private string _moveToSkills = "Skills";

    private bool _isLoadingSkills;

    public void OpenSkills()
    {
        if (!_isLoadingSkills ||
            SceneManager.GetSceneByName(_moveToSkills).isLoaded)
        {
            return;
        }

        StartCoroutine(LoadSkillsScene());
    }

    private IEnumerator LoadSkillsScene()
    {
        _isLoadingSkills = true;
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(_moveToSkills, LoadSceneMode.Additive);

        if (operation == null)
        {
            _isLoadingSkills = false;
            yield break;
        }

        yield return operation;
        _isLoadingSkills = false;
    }
    
    public void MoveToInGame()
    {
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync("Skills");
    }

    public void MoveToMain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainTitle");
    }
}
