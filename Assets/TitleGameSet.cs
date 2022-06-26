using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleGameSet : MonoBehaviour
{
    [SerializeField] GameObject OAR;
    // Start is called before the first frame update
    void Start()
    {
        OAR.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GameStart()
    {
        SceneManager.LoadScene("FPS00");
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void OptionAndRankBoard()
    {
        OAR.SetActive(true);
        OAR.GetComponentInChildren<TitleComponent>().isOption = true;
    }
}
