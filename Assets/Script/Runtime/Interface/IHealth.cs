using System;

namespace Script.Runtime.Interface
{
    public interface IHealth
    {
        Action<int> OnTakeDamageEvent { get; set; }
    }
}