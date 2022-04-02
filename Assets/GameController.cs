using UnityEngine;

public class GameController : MonoBehaviour
{
    private Computer computer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (computer != null)
        {
            if (computer.SoftwareUpdateHasHappened())
            {
                print("You lose!!!");
            }
        }
        else
        {
            computer = Computer.Instance();
        }
    }
}
