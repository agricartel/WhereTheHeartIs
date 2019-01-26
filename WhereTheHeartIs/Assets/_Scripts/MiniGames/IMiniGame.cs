using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public enum MiniGameType
{
    TEST = 0,

    NUM_GAMES,
}


public interface IMiniGame
{

    bool finishedMiniGame();
}
