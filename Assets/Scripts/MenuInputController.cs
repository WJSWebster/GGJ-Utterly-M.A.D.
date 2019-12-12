using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInputController : MonoBehaviour
{
    [SerializeField]
    private float m_WaitTime = 5f;  // the amount of time that needs to pass before user can press a button

    // TODO: we may not need this, as, in case of intro, input will be found by update, 
    //       and in the case of menu, input will be handled by Button component calling 
    //       some sort of main menu func, which in turn can then query the m_Option and handle accordingly
    private enum SceneTypes { Intro, MainMenu, Other };
    [Space(20)]
    [SerializeField]
    private SceneTypes m_SceneType = SceneTypes.MainMenu;

    private enum Options { None, Begin, HowTo, Settings, Quit };
    [SerializeField]
    private Options m_Option = Options.None;

    [Space(10)]
    [SerializeField]
    private Animator m_Animator;

    private float m_StartTime;
    private bool m_HoldingDown = false;


    void Start()
    {
        m_StartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.time - m_StartTime) > m_WaitTime)
        {
            if (Input.anyKey)
            {
                m_HoldingDown = true;
            }

            if (m_HoldingDown && !Input.anyKey)
            {
                if (m_SceneType == SceneTypes.Intro)
                {
                    m_Animator.SetTrigger("RipOutTransition");
                }
                else if (m_SceneType == SceneTypes.MainMenu)
                {
                    // note: we no longer do anything within update, MainMenuHandler is instead called by Button::OnClick()
                }
                else if (m_SceneType == SceneTypes.Other)
                {
                    NextScene();
                }
            }
        }
    }

    //-----------------------------------------------------------------------------------------

    public void MainMenuHandler()
    {
        Debug.Assert(m_SceneType == SceneTypes.MainMenu, "MenuInputController::MainMenuHandler() - m_SceneType is not MainMenu!");

        switch (m_Option)
        {
            case Options.Begin:
                BeginMADness();
                break;
            case Options.HowTo:
                HowToPlay();
                break;
            case Options.Settings:
                Settings();
                break;
            case Options.Quit:
                Quit();
                break;
            case Options.None:
            default:
                Debug.Assert(false, "MenuInputController::MainMenuHandler() - m_Option is invalid!");
                break;
        }
    }

    void BeginMADness()
    {
        // todo: do the text animation stuff here - still not 100% what this should be yet
        // maybe swing camera down/up or something?


        #if UNITY_EDITOR
            Debug.Log("BEGIN!");  // because cba moving to the next scene every time we test
        #else
            NextScene();
        #endif
    }

    void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void HowToPlay()
    {
        // todo: swing camera to the left to reveal 'How To Play' screen
        Debug.Log("HOW TO PLAY!");
    }

    void Settings()
    {
        // todo: swing camera to the right to reveal 'Settings' screen
        Debug.Log("SETTINGS!");
    }

    public void Quit()
    {
        Debug.Log("QUIT!");

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
