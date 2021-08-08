using Assets.Scripts.DistanceRiddle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Assets.Scripts;
using System;

public class distanceRiddle : MonoBehaviour
{
    private float maxPlayerDistance = 5f;
    [SerializeField] private DistanceChecker distanceChecker = null;

    public CheckerObject[] checkers;

    // Start is called before the first frame update
    void Start()
    {
        //!!! IF ! SERVER DESTROY ! PLAN A

        checkers = FindObjectsOfType<CheckerObject>();
        CheckerObject.triggerUpdate += ActivationUpdate;
    }

    public void ActivationUpdate()
    {
        if (distanceChecker?.Valid ?? false) 
            return;

        var allEntered = true;
        foreach (var checker in checkers)
            allEntered &= checker.entered; 

        if(allEntered)
            ActivateRiddle();
    }

    // Update is called once per frame
    void Update()
    {
        if (distanceChecker?.Valid ?? false)
        {
            //PLAN B : Effekt nur auf eigenene player ! -> jeder client checkt und setzt sich selbst zurück. PLAN A besser, weil a) weniiger rechnen, b) delays vermeiden
            distanceChecker.DebugDraw(maxPlayerDistance);
        }


        var debug = distanceChecker._playerB.localPosition;
        var debug2 = distanceChecker._playerA.localPosition;

        Debug.Log(debug);

        Debug.Log(debug2);
    }

    void ActivateRiddle()
    {
        if (distanceChecker == null)
            distanceChecker = new DistanceChecker();

        distanceChecker.Init();
        
    }
}
