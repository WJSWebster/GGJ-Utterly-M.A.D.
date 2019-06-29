using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewsRoom : MonoBehaviour
{
    public AudioClip Speech;
	public AudioSource Source;

    public int NextSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        Source.clip = Speech;
		Source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(Source.isPlaying == false)
        {
            SceneManager.LoadScene(NextSceneIndex);
        }
    }
}
