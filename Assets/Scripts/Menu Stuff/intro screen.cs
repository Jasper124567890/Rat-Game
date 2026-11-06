using UnityEngine;
using UnityEngine.SceneManagement;

public class introscreen : MonoBehaviour
{
    public void Continue()
    {
        SceneManager.LoadScene("Game");
    }

    
}