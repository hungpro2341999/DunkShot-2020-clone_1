using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutioControl : MonoBehaviour
{
    public  static AutioControl instance;
    public List<AudioSource> AudioSources = new List<AudioSource>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            AudioSources.Add(transform.GetChild(i).GetComponent<AudioSource>());
     //       Debug.Log(transform.GetChild(i).GetComponent<AudioSource>().name);
        }



        GameController.instance.On_Off_Sound += On_Off_Sound;

       
    }
    // Start is called before the first frame update
                  
    void Start()
    {
       

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public AudioSource GetAudio(string name)
    {
        AudioSource audio = null;
        foreach (var Audio in AudioSources)
        {
            if (Audio.name == name)
            {
             //   Debug.Log(Audio.name);
                audio = Audio;
            }
        }
        return audio;
    }

public void GetAudioPlay(string name)
{
    AudioSource audio = null;
    foreach (var Audio in AudioSources)
    {
        if (Audio.name == name)
        {
            Debug.Log(Audio.name);
            audio = Audio;
            audio.Play();
        }
    }
 
}

public void On_Off_Sound(bool open)
    {
        Debug.Log("f//////////////////////////");
        foreach (var Audio in AudioSources)
        {

          
         if(Audio != null)
            {
              //  Debug.Log(Audio.name);
                Audio.mute = open;
            }
           
        }
    }

    public void Stop()
    {
      
        foreach (var Audio in AudioSources)
        {
            if (Audio.isPlaying)
            {
                Audio.Stop();
            }
        }

    }
    public void Continue()
    {


    }
}
