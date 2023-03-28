// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoList : MonoBehaviour
{
    public static UndoList instance;

    private BirdManager birdManager;
    private PlayButtonGroupManager playButtonGroupManager;

    List<UndoMove> undoTurns = new List<UndoMove>();
    private const int maxUndoTurns = 4;

    void Awake(){
        if (instance == null) instance = this;
    }
    void Start(){
        birdManager = BirdManager.instance;
        playButtonGroupManager = PlayButtonGroupManager.instance;
    }
    public void AddUndoTurn(BirdController bc1, BirdController bc2, List<Bird> birdList, List<Bird> birdOrder){
        if (undoTurns.Count == 4) undoTurns.Remove(undoTurns[0]);
        UndoMove newUndoTurn = new UndoMove {firstBc = bc1, secondBc = bc2, birdsToMove = new List<Bird>(birdList), birdsOgOrder = new List<Bird>(birdOrder)};
        undoTurns.Add(newUndoTurn);
        // Debug.Log("birds to move: " + newUndoTurn.birdsToMove.Count);
    }
    public void UndoMoveBirds(){
        if (undoTurns.Count > 0){
            UndoMove undoMove = undoTurns[undoTurns.Count-1];
            if (undoMove.birdsToMove.Count > 0){
                birdManager.UndoMoveBirds(undoMove.secondBc, undoMove.firstBc, undoMove.birdsToMove);
                // birdManager.UndoMove(undoTurns[undoTurns.Count-1].secondBc, undoTurns[undoTurns.Count-1].firstBc, undoTurns[undoTurns.Count-1].birdsToMove);
                // Debug.Log("birds moved: " + undoMove.birdsToMove.Count);
            } else {
                undoMove.firstBc.RevertBirds(undoMove.birdsOgOrder);
            }
            undoTurns.Remove(undoMove);
            playButtonGroupManager.AfterUndo();
        }
    }
    public int GetUndoTurnsAmount(){
        return undoTurns.Count;
    }
}

class UndoMove
{
    public BirdController firstBc, secondBc;
    public List<Bird> birdsToMove;
    public List<Bird> birdsOgOrder;
}