using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public void loadScene(int idx)
    {
        SceneManager.LoadScene(idx);
    }

    public void openPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void exit()
    {
        Application.Quit();
    }
}
