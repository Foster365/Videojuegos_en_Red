using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{

    Roulette rouletteWheel;
    Dictionary<Node, int> rouletteDictionary = new Dictionary<Node, int>();
    Node initialNode;

    private void Start()
    {
        InitRouletteWheel();
    }

    public void InitRouletteWheel()
    {
        rouletteWheel = new Roulette();

        ActionNode faceOne = new ActionNode(ThrowFaceOne);
        ActionNode faceTwo = new ActionNode(ThrowFaceTwo);
        ActionNode faceThree = new ActionNode(ThrowFaceThree);
        ActionNode faceFour = new ActionNode(ThrowFaceFour);
        ActionNode faceFive = new ActionNode(ThrowFaceFive);
        ActionNode faceSix = new ActionNode(ThrowFaceSix);

        rouletteDictionary.Add(faceOne, 35);
        rouletteDictionary.Add(faceTwo, 30);
        rouletteDictionary.Add(faceThree, 30);
        rouletteDictionary.Add(faceFour, 25);
        rouletteDictionary.Add(faceFive, 20);
        rouletteDictionary.Add(faceSix, 10);

    }

    public void RouletteAction()
    {
        Debug.Log("Dice Roulette Wheel");

        Node nodeRoulette = rouletteWheel.Run(rouletteDictionary);
        nodeRoulette.Execute();

    }

    public void ThrowFaceOne()
    {
        ThrowOne();
    }
    public void ThrowFaceTwo()
    {
        ThrowTwo();
    }
    public void ThrowFaceThree()
    {
        ThrowThree();
    }
    public void ThrowFaceFour()
    {
        ThrowFour();
    }
    public void ThrowFaceFive()
    {
        ThrowFive();
    }
    public void ThrowFaceSix()
    {
        ThrowSix();
    }

    #region Numbers

    int ThrowOne()
    {
        return 1;
    }

    int ThrowTwo()
    {
        return 2;
    }
    int ThrowThree()
    {
        return 3;
    }
    int ThrowFour()
    {
        return 4;
    }
    int ThrowFive()
    {
        return 5;
    }
    int ThrowSix()
    {
        return 6;
    }
    #endregion
}
