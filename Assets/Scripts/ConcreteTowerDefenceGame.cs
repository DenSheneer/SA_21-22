using System.Collections;
using System.Collections.Generic;
using System;

public class ConcreteTowerDefenceGame
{
    int money;
    int waveNumber;
    float timeBetweenRounds;

    public static Action OnGameOver;
    Timer roundTimer;

    EnemySpawnHandler enemySpawnHandler;
}
