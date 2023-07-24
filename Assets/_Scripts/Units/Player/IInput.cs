using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInput
{
    public float HorizontalInput { get; set; }
    public float VerticalInput { get; set; }

    public void ReadInput();
}
