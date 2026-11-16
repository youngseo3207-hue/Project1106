using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("InGame");
    }

    public void OpenSkills()
    {
        SceneManager.LoadScene("Skills");
    }
}
