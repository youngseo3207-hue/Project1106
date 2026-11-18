using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameSceneController : MonoBehaviour
{
    public void MoveToSkills()
    {
        SceneManager.LoadSceneAsync("Skills", LoadSceneMode.Additive);
        Time.timeScale = 0;
    }
}
