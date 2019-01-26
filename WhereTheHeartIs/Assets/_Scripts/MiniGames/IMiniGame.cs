using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum MiniGameType
{
    TEST = 0,

    NUM_GAMES,
}


public interface IMiniGame
{
    GameObject ControllerObject { get; }
    bool IsFinished();
    bool DidComplete();
}
