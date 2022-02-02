using Assets.Scripts.ActionReactionSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoor : MonoBehaviour
{
    [SerializeField] private string[] correctNumbers;
    int currentIndex = 0;

    private Animator animator;

    void Start()
    {
        ReactionManager.Add("OpenDoorButton", ButtonClicked);

        animator = gameObject.GetComponent<Animator>();
    }

    private void ButtonClicked(string[] parameters)
    {
        EnterNumber(parameters[0]);
    }

    private void EnterNumber(string number)
    {
        if(number == correctNumbers[currentIndex])
        {
            currentIndex++;
        }
        else
        {
            currentIndex = 0;
        }

        if (currentIndex == correctNumbers.Length)
        {
            Open();
            currentIndex = 0;
        }
    }

    private void Open()
    {
        animator.Play("door_3_open");
    }
}
