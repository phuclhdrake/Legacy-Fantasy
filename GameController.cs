using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening; 
public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panelSetting;
    void Start()
    {
        SoundManager.Instance.PlayMusic("Theme");

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RePlayGame()
    {
        SoundManager.Instance.sfxSource.Stop();
        SoundManager.Instance.PlaySFX("btnClick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        //SoundManager.Instance.PlayMusic("Theme");
    }

    public void OpenSettings()
    {
        SoundManager.Instance.sfxSource.Stop();
        SoundManager.Instance.PlaySFX("btnClick");
        Time.timeScale = 0; // dung scence
        panelSetting.SetActive(true);
        panelSetting.transform.DOScale(1, 0.5f).SetEase(Ease.OutElastic).SetUpdate(true);
    }

    public void BackSettings()
    {
        SoundManager.Instance.sfxSource.Stop();
        SoundManager.Instance.PlaySFX("btnClick");
        Time.timeScale = 1; // chay scence
        panelSetting.transform.DOScale(0, 0.5f).SetEase(Ease.InOutElastic).OnComplete(() =>
        {

        panelSetting.SetActive(false);
        });
    }

    public void BackMainMenu()
    {
        SoundManager.Instance.sfxSource.Stop();
        SoundManager.Instance.PlaySFX("btnClick");
        SceneManager.LoadScene("mainmenu");
        Time.timeScale = 1;
    }

    public void ClickJump()
    {
        PlayerController.Instance.ButtonJump();
    }

    public void ClickAttack()
    {
        PlayerController.Instance.ButtonAttack();
    }

}
