using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    //Singleton----------------------------------------------------

    private static SoundManager _instance;

    public static SoundManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }

    [SerializeField] AudioClip clapping;

    public void PlayClapping()
    {
        GetComponent<AudioSource>().PlayOneShot(clapping);
    }

    //-------------------------------------------------------------

}
