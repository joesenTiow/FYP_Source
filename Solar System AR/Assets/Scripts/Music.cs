using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private static Music music = null;
    public static Music Instance
    {
        get
        {
            if (music == null)
            {
                music = (Music)FindObjectOfType(typeof(Music));
            }
            return music;
        }
    }

    private void Awake()
    {
        
        if (Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
