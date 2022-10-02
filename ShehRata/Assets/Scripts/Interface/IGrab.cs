using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrab
{
    void Grab(Transform owner);
    void Release();
}
