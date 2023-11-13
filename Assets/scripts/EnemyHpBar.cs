using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    
    public void UpdateHpBar(float health, float damePlayer){
        slider.value = 1 + (health - damePlayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
