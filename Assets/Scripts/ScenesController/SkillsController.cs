using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SkillsController : MonoBehaviour
{
    [SerializeField] private string moveToSkills = "Skills";

    private bool isLoadingSkills;

    public void OpenSkills()
    {
        if (!isLoadingSkills ||
            SceneManager.GetSceneByName(moveToSkills).isLoaded)
        {
            return;
        }

        StartCoroutine(LoadSkillsScene());
    }

    private IEnumerator LoadSkillsScene()
    {
        isLoadingSkills = true;
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(moveToSkills, LoadSceneMode.Additive);

        if (operation == null)
        {
            isLoadingSkills = false;
            yield break;
        }

        yield return operation;
        isLoadingSkills = false;
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
