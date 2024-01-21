using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseCell
{
    bool IsActive { get; set; }

    void Interact();
}
