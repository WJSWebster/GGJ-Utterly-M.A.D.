using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum Options { None, Begin, HowTo, Settings, Quit, Return };

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

    //public enum Options { None, Begin, HowTo, Settings, Quit, Return};
    [SerializeField]
    private Options m_Option = Options.None;

    [Space(10)]
    [SerializeField]
    private Animator m_Animator;
    [SerializeField]
    private RotateCurve m_CameraRot;
    [SerializeField]
    private MainMenuCanvasController m_CanvasPan;

    private float m_StartTime;
    private bool m_HoldingDown = false;

    void Start()
    {
        m_StartTime = Time.time;

        if(m_SceneType == SceneTypes.MainMenu)
        {
            string gameObjectName = this.name;
            Debug.Assert(m_CameraRot != null, gameObjectName + "->MenuInputController::Start() - m_CameraRot not set for MainMenu!");

            if(m_Option == Options.HowTo || m_Option == Options.Settings || m_Option == Options.Return)
            {
                Debug.Assert(m_CanvasPan != null, gameObjectName + "->MenuInputController::Start() - m_CanvasPan not set for MainMenu!");
            }
        }
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
                    NextScene.LoadNextScene();
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
                StartCoroutine(BeginMADness());
                break;
            case Options.HowTo:
                HowToPlay();
                break;
            case Options.Settings:
                Settings();
                break;
            case Options.Quit:
                StartCoroutine(Quit());
                break;
            case Options.Return:
                Return();
                break;
            case Options.None:
            default:
                Debug.Assert(false, "MenuInputController::MainMenuHandler() - m_Option is invalid!");
                break;
        }
    }

    IEnumerator BeginMADness()
    {
        // todo: do the text animation stuff here - still not 100% what this should be yet
        // maybe swing camera down/up or something?
        Quaternion rotationAngle = Quaternion.Euler(-90f, 0f, 0f);
        m_CameraRot.SetNewRotation(rotationAngle);
        
        yield return new WaitForSeconds(RotateCurve.m_AnimationTime);

        #if UNITY_EDITOR
            Debug.Log("BEGIN!");  // because cba moving to the next scene every time we test
#endif

        NextScene.LoadNextScene();
    }

    // TODO: for each of the following - OAOO violation:
    void HowToPlay()
    {
        // todo: swing camera to the left to reveal 'How To Play' screen
        Quaternion rotationAngle = Quaternion.Euler(0f, -90f, 0f);
        m_CameraRot.SetNewRotation(rotationAngle);
        m_CanvasPan.ShiftUI(Options.HowTo);

        Debug.Log("HOW TO PLAY!");
    }

    void Settings()
    {
        // todo: swing camera to the right to reveal 'Settings' 
        Quaternion rotationAngle = Quaternion.Euler(0f, 90f, 0f);
        m_CameraRot.SetNewRotation(rotationAngle);
        m_CanvasPan.ShiftUI(Options.Settings);

        Debug.Log("SETTINGS!");
    }

    void Return()
    {
        // todo: swing camera to the right to reveal 'Settings' 
        Quaternion rotationAngle = Quaternion.Euler(0f, 0f, 0f);  // todo: how do we rotate back to centre?
        m_CameraRot.SetNewRotation(rotationAngle);
        m_CanvasPan.ShiftUI(Options.Return);

        Debug.Log("RETURN!");
    }

    public IEnumerator Quit()  // todo: why is this public?
    {
        Quaternion rotationAngle = Quaternion.Euler(90f, 0f, 0f);
        m_CameraRot.SetNewRotation(rotationAngle);

        yield return new WaitForSeconds(RotateCurve.m_AnimationTime);

        #if UNITY_EDITOR
            Debug.Log("QUIT!");
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void ResetCamera()
    {
        Quaternion rotationAngle = Quaternion.Euler(0f, 0f, 0f);
        m_CameraRot.SetNewRotation(rotationAngle);
    }
}
