using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParametersController : MonoBehaviour
{
    public Slider _hpSlider;
    public Text countKill;
    private int kills =0 ;


    // Start is called before the first frame update
    void Start()
    {
        countKill = GameObject.FindWithTag("Kills").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if (kills >= 1)
        {
            countKill.text = kills + "";
        }
        
    }
    public void setMaxHpSlider(int health)
    {
        _hpSlider.maxValue = health;
    }
    public void setHpSlider(int health)
    {
        _hpSlider.value = health;
    }

    public void addKill(int countKills)
    {
        kills = countKills;
    }
    
    public void isMpSlider()
    {

    }

    public void isExpSlider()
    {

    }


}
