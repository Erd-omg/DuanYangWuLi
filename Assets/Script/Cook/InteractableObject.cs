using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public CookGameManager gameManager;
    public int stepIndex;

    void OnMouseDown()
    {
        gameManager.StartInteraction(stepIndex);
    }
}