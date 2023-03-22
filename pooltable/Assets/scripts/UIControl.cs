using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    [SerializeField] GameObject resume, restart, quit ,pause, panelPause;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;            
        GameManager.data.GameOn = true;
        panelPause.SetActive(false);
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        GameManager.data.GameOn = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Pause()
    {
        if (!panelPause.activeInHierarchy)
        {
            panelPause.SetActive(true);
            Time.timeScale = 0f;
            GameManager.data.GameOn = false;
            GameManager.data.win1.SetActive(false);
            GameManager.data.win2.SetActive(false);
        }
        else if (panelPause.activeInHierarchy)
        {
            panelPause.SetActive(false);
            Time.timeScale = 1f;
            GameManager.data.GameOn = true;
            GameManager.data.win1.SetActive(false);
            GameManager.data.win2.SetActive(false);
        }
    }

    public void Quit()
    {
        Debug.Log("quit");
        Application.Quit();
        
    }
}
