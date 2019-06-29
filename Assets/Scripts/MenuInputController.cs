using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInputController : MonoBehaviour
{
    private float StartTime;

    void Start() {
        StartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (((Time.time - StartTime) > 5f) && (Input.GetKeyUp(KeyCode.D) || Input.GetButtonUp("BlueAggressive") || Input.GetKeyUp(KeyCode.A) || Input.GetButtonUp("BluePassive") 
            || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetButtonUp("OrangeAggressive") || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetButtonUp("OrangePassive")))
        {
            Debug.Log("Orange is PASSIVE!");
            // OrangeChosen = true;
            // OrangeTextController.IsPlanting = true;
            // SceneManagement.LoadScene(1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
