using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject pausePanel;
    public GameObject pauseButton; // tombol pause utama

    public UnityEngine.UI.Image pauseButtonIcon;
    public Sprite pauseIcon;
    public Sprite resumeIcon;

    private bool isPaused = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (pauseButton != null)
            pauseButton.SetActive(true);
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;

        if (pausePanel != null) pausePanel.SetActive(true);
        if (pauseButtonIcon != null && resumeIcon != null)
            pauseButtonIcon.sprite = resumeIcon;

        if (AudioManager.Instance != null)
            AudioManager.Instance.PauseMusic();
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;

        if (pausePanel != null) pausePanel.SetActive(false);
        if (pauseButtonIcon != null && pauseIcon != null)
            pauseButtonIcon.sprite = pauseIcon;

        if (AudioManager.Instance != null)
            AudioManager.Instance.ResumeMusic();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
