using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeadUI : MonoBehaviour
{
    #region Serialized

    [SerializeField]
    private float fadeSpeed = 2;

    [SerializeField]
    private GameObject startGamePanel;

    [SerializeField]
    private GameObject tutorialPanel;

    [SerializeField]
    private GameObject inGamePanel;

    [SerializeField]
    private GameObject endGamePanel;

    [SerializeField]
    private GameObject pausePanel;

    [SerializeField]
    private GameObject shopPanel;

    [SerializeField]
    private Transform handGesture;

    #endregion

    private GameObject currentActivePanel;
    private Animator handAnimator;

    private void Awake()
    {
        handAnimator = handGesture.GetComponent<Animator>();
        if (startGamePanel.activeSelf)
            currentActivePanel = startGamePanel;
        else
            currentActivePanel = tutorialPanel;
        EventsPool.PlayerFallenEvent.AddListener((bool w) => FinishGame(false));
        EventsPool.PlayerWonEvent.AddListener(() => FinishGame(true));
    }

    private void Update()
    {
        handGesture.transform.position = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            handAnimator.SetTrigger("Click");
        }
    }
    public void StartTutorial()
    {
        ShowPanel(tutorialPanel);
    }
    public void StartGame()
    {
        EventsPool.GameStartedEvent.Invoke();
        FadeToPanel(inGamePanel);
    }
    public void Next()
    {
        int ind = SceneManager.GetActiveScene().buildIndex;
        if (ind < SceneManager.sceneCountInBuildSettings - 1)
            SceneManager.LoadScene(ind + 1);
        else
            SceneManager.LoadScene(0);
    }
    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void PauseUnpause()
    {
        if (Time.timeScale > 0)
        {
            ShowPanel(pausePanel);
            Time.timeScale = 0;
        }
        else
        {
            ShowPanel(inGamePanel);
            Time.timeScale = 1;
        }

    }
    public void Shop()
    {
        if(currentActivePanel != shopPanel)
            FadeToPanel(shopPanel);
        else
            FadeToPanel(startGamePanel);

    }
    public void Exit()
    {
        
    }
    private void FadeToPanel(GameObject panel, float wait = 0)
    {
        CanvasGroup panelCG = panel.GetComponent<CanvasGroup>();
        CanvasGroup curCG = currentActivePanel.GetComponent<CanvasGroup>();
        panelCG.alpha = 0;
        panel.SetActive(true);
        IEnumerator fade()
        {
            yield return new WaitForSeconds(wait);
            while(panelCG.alpha < 1)
            {
                panelCG.alpha += Time.deltaTime * fadeSpeed;
                curCG.alpha -= Time.deltaTime * fadeSpeed;
                yield return new WaitForEndOfFrame();  
            }
            currentActivePanel.SetActive(false);
            curCG.alpha = 0;
            currentActivePanel = panel;
        }
        StartCoroutine(fade());
    }
    private void ShowPanel(GameObject panel)
    {
        currentActivePanel.SetActive(false);
        panel.SetActive(true);
        currentActivePanel = panel;
    }
    private void FinishGame(bool win)
    {
        EndGame end = endGamePanel.GetComponent<EndGame>();
        if (win)
            end.Win();
        else
            end.Lose();
        FadeToPanel(endGamePanel, 1.5f);

    }
}
