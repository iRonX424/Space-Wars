using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float waitInSeconds = 3.5f;
    public void loadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void loadGame()
    {
        SceneManager.LoadScene(1);
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void loadGameOver()
    {
        StartCoroutine(WaitAndDie());
    }

    IEnumerator WaitAndDie()
    {
        yield return new WaitForSeconds(waitInSeconds);
        SceneManager.LoadScene(2);
    }

    public void quit()
    {
        Application.Quit();
    }
}
