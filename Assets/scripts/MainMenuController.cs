using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject panelOption, panelCredit;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayMusic("Theme3");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        SoundManager.Instance.PlaySFX("btnClick");
        SceneManager.LoadScene("manso1");
        Time.timeScale = 1;
    }
    private bool isShowOption;
    public void OpenOption()
    {
        SoundManager.Instance.PlaySFX("btnClick");
        isShowOption = !isShowOption;
        if (isShowOption)
        {
            panelOption.SetActive(true);
        }
        else
        {
            panelOption.SetActive(false);
        }
    }
    private bool isShowCredit;
    public void OpenCredit()
    {
        SoundManager.Instance.PlaySFX("btnClick");
        isShowCredit = !isShowCredit;
        if (isShowCredit)
        {
            panelCredit.SetActive(true);
        }
        else
        {
            panelCredit.SetActive(false);
        }
    }

    public void QuiGame()
    {
        SoundManager.Instance.PlaySFX("btnClick");
        Debug.Log("Tho�t game");
        Application.Quit();
    }
}
