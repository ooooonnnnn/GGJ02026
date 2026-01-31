using UnityEngine;
using UnityEngine.UI;

public class UserController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int HP;
    [SerializeField] GameManager gm;
    [SerializeField] Slider healthSlider;
    void Start()
    {
        HP = 20;
        healthSlider.value = HP;
    }
    public void hit(){
        HP-=1;
        healthSlider.value = HP;
        print("Player hit!");
    }
    // Update is called once per frame
    void Update()
    {
        if(HP < 1){
            gm.GameOver();
        }   
    }
}
