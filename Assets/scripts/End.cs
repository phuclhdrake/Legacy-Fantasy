using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    public Animator anim;
    public GameObject bgYouWin;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("isOpen", true);
            SoundManager.Instance.sfxSource.Stop();
            SoundManager.Instance.PlaySFX("YouWinMusic");
            StartCoroutine(EndGameAfterDelay(1f));
        }
    }

    private IEnumerator EndGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        //Time.timeScale = 0; // dung scence
        bgYouWin.SetActive(true);
    }
}
