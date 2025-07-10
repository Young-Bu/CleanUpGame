using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Animations : MonoBehaviour
{
    public Animation animationComponent;
    public string openAnimationName;
    public string closeAnimationName;

    private bool isOpen = false;

    void OnMouseDown()
    {
        if (isOpen)
        {
            animationComponent.Play(closeAnimationName);
        }
        else
        {
            animationComponent.Play(openAnimationName);
        }

        isOpen = !isOpen;
    }
}
