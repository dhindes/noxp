using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class References
{
    public static PlayerBehaviour thePlayer;
    public static CanvasBehaviour canvas;
    public static List<EnemySpawner> spawners = new List<EnemySpawner>();
    public static List<EnemyBehaviour> allEnemies = new List<EnemyBehaviour>();
    public static List<Usable> usables = new List<Usable>();
    public static List<PlinthBehaviour> plinths = new List<PlinthBehaviour>();
    public static LevelManager levelManager;
    public static AlarmManager alarmManager;
    public static ScoreManager scoreManager;
    public static Persistent essentials;
    public static LevelGenerator levelGenerator;

    public static CameraTools cameraTools;
    public static List<NavPoint> navPoints = new List<NavPoint>();
    public static float maxDistanceInALevel = 1000;
    public static LayerMask wallsLayer = LayerMask.GetMask("Walls");
    public static LayerMask enemiesLayer = LayerMask.GetMask("Enemies");
}
