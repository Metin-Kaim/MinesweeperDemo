using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public static int MouseClick { get; private set; }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MouseClick = 0;
        }
        else if (Input.GetMouseButton(1))
        {
            MouseClick = 1;
        }
    }
}
