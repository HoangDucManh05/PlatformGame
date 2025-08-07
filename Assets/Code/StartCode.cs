using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.SceneManagement;
using Application = UnityEngine.Application;

public class StartCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void start()
    {
        SceneManager.LoadScene(3);
    }
    public void thoat()
    {
        Application.Quit();
    }
}
