using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float Horizontal { get; set; }
    public float Vertical { get; set; }

    public void GetInput()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");
    }

  
}
