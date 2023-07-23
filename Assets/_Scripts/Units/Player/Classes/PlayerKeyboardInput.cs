using UnityEngine;

public class PlayerKeyboardInput : IInput
{
    public float HorizontalInput { get; set; }
    public float VerticalInput { get; set; }

    public void ReadInput()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
    }

  
}
