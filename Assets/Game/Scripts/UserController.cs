using UnityEngine;

public class UserController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int HP;
    [SerializeField] GayManager gm;
    void Start()
    {
        HP = 20;
    }
    public void hit(){
        HP-=1;
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
