﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    Vector2 superStart, fromTo;

    public GameObject player, grid;

    public RoomGenerationInfo roomID;

    private List<GameObject> startRoom = new List<GameObject>();
    private List<GameObject> bossRoom = new List<GameObject>();
    private List<GameObject> sellerRoom = new List<GameObject>();
    private List<GameObject> darkSellerRoom = new List<GameObject>();
    private List<GameObject> challengeRoom = new List<GameObject>();
    private List<GameObject> chestRoom = new List<GameObject>();
    private List<GameObject> enemyRoom = new List<GameObject>();

    private Vector2[] direction = new Vector2[] { new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 0), new Vector2(-1, 0) };

    private List<Vector2> blackList = new List<Vector2>();
    private List<Vector2> defiBossNodePos = new List<Vector2>();
    private List<Vector2> marchandBossNodePos = new List<Vector2>();
    private List<Vector2> chestPossibility = new List<Vector2>();
    private List<Vector2> challengePossibility = new List<Vector2>();
    private List<Vector2> sellerPossibility = new List<Vector2>();
    private List<Vector2> blackSellerPossibility = new List<Vector2>();

    private List<Vector2> bossPath = new List<Vector2>();

    private List<GameObject> roomPos = new List<GameObject>();
    private List<GameObject> roomTransf = new List<GameObject>();
    private List<GameObject> xExtremis = new List<GameObject>();
    private List<GameObject> yExtremis = new List<GameObject>();
    private List<float> sizeX = new List<float>();
    private List<float> sizeY = new List<float>();

    private Dictionary<string, Vector2> cellule = new Dictionary<string, Vector2>();

    /*
     * nothing
     * enemy
     * challenge
     * chest
     * boss
     * blackSeller
     * seller
    */

    public int distanceMinBoss = 4;
    public int distanceMaxBoss = 4;
    public int distanceBoss = 4;

    public int nombreMachandBossMin = 1;
    public int nombreMachandBossMax = 1;
    public int nombreMarchandBoss = 1;

    public int defiBossMin = 1;
    public int defiBossMax = 1;
    public int defiBoss = 1;

    public int nombreDeSalleMin = 11;
    public int nombreDeSalleMax = 11;
    public int nombreDeSalle = 11;

    public int nombreDeCoffreMin = 2;
    public int nombreDeCoffreMax = 2;
    public int nombreDeCoffre = 2;

    public int nombreDeMarchandMin = 1;
    public int nombreDeMarchandMax = 1;
    public int nombreDeMarchand = 1;

    public int nombreDeMarchandNoirMin = 1;
    public int nombreDeMarchandNoirMax = 1;
    public int nombreDeMarchandNoir = 1;

    public int nombreDeDefiMax = 2;
    public int nombreDeDefiMin = 2;
    public int nombreDeDefi = 2;

    public int xDirection = 1;

    public float marge = 2f;

    private int nombreAssigneDeSalle = 6;
    private bool lineNumber = false;
    public LayerMask roomLayer;

    public GameObject[] refObj;

    public Transform SpawnPoint;

    void Start()
    {
        foreach (GameObject obj in roomID.start)
        {
            startRoom.Add(obj);
        }
        foreach (GameObject obj in roomID.boss)
        {
            bossRoom.Add(obj);
        }
        foreach (GameObject obj in roomID.seller)
        {
            sellerRoom.Add(obj);
        }
        foreach (GameObject obj in roomID.blackSeller)
        {
            darkSellerRoom.Add(obj);
        }
        foreach (GameObject obj in roomID.enemy)
        {
            enemyRoom.Add(obj);
        }
        foreach (GameObject obj in roomID.challenge)
        {
            challengeRoom.Add(obj);
        }
        foreach (GameObject obj in roomID.chest)
        {
            chestRoom.Add(obj);
        }

        Generation();
    }

    void Generation()
    {
        //distanceBossCheck  
        if (distanceMaxBoss < 4) distanceMaxBoss = 4;
        if (distanceMinBoss < 4) distanceMinBoss = 4;
        if (distanceMinBoss > distanceMaxBoss) distanceMinBoss = distanceMaxBoss;

        distanceBoss = TirageAuSort(distanceMinBoss, distanceMaxBoss) + 1;

        //nombreMarchand Check
        if (nombreMachandBossMax > Mathf.RoundToInt((distanceBoss - 5) / 2)) nombreMachandBossMax = (distanceBoss - 5) / 2;
        if (nombreMachandBossMin > nombreMachandBossMax) nombreMachandBossMin = nombreMachandBossMax;

        nombreMarchandBoss = TirageAuSort(nombreMachandBossMin, nombreMachandBossMax);

        //defiBossMax Check
        if ((distanceBoss - 6) % 2 == 1) nombreAssigneDeSalle--;
        if (defiBossMax > (distanceBoss - nombreAssigneDeSalle) / 2) defiBossMax = (distanceBoss - nombreAssigneDeSalle) / 2;
        if (defiBossMin > defiBossMax) defiBossMin = defiBossMax;

        defiBoss = TirageAuSort(defiBossMin, defiBossMax);

        //nombreDeCoffre Check
        if (nombreDeCoffreMin < 2) nombreDeDefiMin = 2;
        if (nombreDeCoffreMax < nombreDeCoffreMin) nombreDeDefiMin = nombreDeDefiMax;

        nombreDeCoffre = TirageAuSort(nombreDeCoffreMin, nombreDeCoffreMax);

        //nombreDeDefiCheck
        if (nombreDeDefiMin < 2) nombreDeDefiMin = 2;
        if (nombreDeDefiMax < nombreDeDefiMin) nombreDeDefiMin = nombreDeDefiMax;

        nombreDeDefi = TirageAuSort(nombreDeDefiMin, nombreDeDefiMax);

        //nombreDeMarchan Check
        if (nombreDeMarchandMax < nombreDeMarchandMin) nombreDeMarchandMin = nombreDeMarchandMax;

        nombreDeMarchand = TirageAuSort(nombreDeMarchandMin, nombreDeMarchandMax);

        //nombreDeMarchandNoir Check
        if (nombreDeMarchandNoirMin < 1) nombreDeMarchandNoirMin = 1;
        if (nombreDeMarchandNoirMax < nombreDeMarchandNoirMin) nombreDeMarchandNoirMin = nombreDeMarchandNoirMax;

        nombreDeMarchandNoir = TirageAuSort(nombreDeMarchandNoirMin, nombreDeMarchandNoirMax);

        //nombreDeSalle Check
        if (nombreDeSalleMin < distanceBoss + 1 + nombreDeCoffre + nombreDeDefi + nombreDeMarchand + nombreDeMarchandNoir) nombreDeSalleMin = distanceBoss + 1 + nombreDeCoffre + nombreDeDefi + nombreDeMarchand + nombreDeMarchandNoir;
        if (nombreDeSalleMax < nombreDeSalleMin) nombreDeSalleMin = nombreDeSalleMax;

        nombreDeSalle = TirageAuSort(nombreDeSalleMin, nombreDeSalleMax);

        BossPathGeneration();
        BossPathRoomGenerationStart();
        
        blackList.Add(bossPath[0]);
        blackList.Add(bossPath[distanceBoss - 1]);
        /*for (int i = 1; i <= nombreDeSalle; i++)
        {
            DungeonPathGeneration();
        }
        DungeonPathGenerationStart();*/

        SortingVector();
        Secoure();
    }

    int TirageAuSort(int min, int max)
    {
        int nombreChoisie = 0;

        nombreChoisie = Random.Range(min, max + 1);

        return nombreChoisie;
    }

    void BossPathGeneration()
    {
        int xPos, yPos, highPos = 1;

        if (TirageAuSort(0, 1) == 0) highPos = 0;

        grid.transform.position = new Vector3(grid.transform.position.x * xDirection, grid.transform.position.y, 0f);

        yPos = TirageAuSort(1, (distanceBoss + highPos)) - 1;
        xPos = ((distanceBoss) - yPos) * xDirection;

        for (int i = 0; i < distanceBoss; i++)
        {
            if (yPos > 0 && xPos != 0)
            {
                if (TirageAuSort(0, 1) == 0) yPos--;
                else xPos -= 1 * xDirection;
            }
            else if (yPos > 0 && xPos == 0) yPos--;
            else if (yPos == 0 && xPos != 0) xPos -= 1 * xDirection;

            bossPath.Add(new Vector2(xPos, yPos));
        }
    }

    void BossPathRoomGenerationStart()
    {
        cellule.Add("boss1", bossPath[0]);
        cellule.Add("seller1", bossPath[1]);
        cellule.Add("challenge1", bossPath[2]);
        cellule.Add("start1", bossPath[distanceBoss - 1]);
        cellule.Add("enemy1", bossPath[distanceBoss - 2]);

        CheckPossibility();
        PositionAttribution();

    }

    void CheckPossibility()
    {
        foreach (Vector2 node in bossPath)
        {
            marchandBossNodePos.Add(node);
            defiBossNodePos.Add(node);
        }

        foreach (Vector2 nonePossibility in cellule.Values)
        {
            marchandBossNodePos.Remove(nonePossibility);
            defiBossNodePos.Remove(nonePossibility);
        }
        for (int i = 1; i > 0; i++)
        {
            if (!cellule.ContainsKey("challenge" + i.ToString())) i = -1;
            else
            {
                foreach (Vector2 dir in direction)
                {
                    defiBossNodePos.Remove(cellule["challenge" + i.ToString()] + dir);
                }
            }
        }
        for (int i = 1; i > 0; i++)
        {
            if (!cellule.ContainsKey("seller" + i.ToString())) i = -1;
            else
            {
                foreach (Vector2 dir in direction)
                {
                    defiBossNodePos.Remove(cellule["seller" + i.ToString()] + dir);
                }
            }
        }
    }

    void PositionAttribution()
    {
        List<Vector2> defiBossNodeTemp = new List<Vector2>();
        List<Vector2> marchandBossNodeTemp = new List<Vector2>();
        List<Vector2> enemyNode = new List<Vector2>();

        foreach (Vector2 node in defiBossNodePos)
        {
            defiBossNodeTemp.Add(node);
        }
        foreach (Vector2 node in marchandBossNodePos)
        {
            marchandBossNodeTemp.Add(node);
        }
        foreach (Vector2 node in bossPath)
        {
            enemyNode.Add(node);
        }
        for (int i = 1; i <= defiBoss; i++)
        {
            Vector2 roomSelected = defiBossNodeTemp[TirageAuSort(0, defiBossNodeTemp.Count - 1)];

            defiBossNodeTemp.Remove(roomSelected);

            foreach (Vector2 dir in direction)
            {
                if (defiBossNodeTemp.Contains(roomSelected + dir)) defiBossNodeTemp.Remove(roomSelected + dir);
            }

            float possibilityleft = defiBossNodeTemp.Count;
            if (defiBossNodeTemp.Count == defiBoss - i)
            {
                bool neighbours = false;
                foreach (Vector2 node in defiBossNodeTemp)
                {
                    foreach (Vector2 dir in direction)
                    {
                        if (defiBossNodeTemp.Contains(node + dir)) neighbours = true;
                    }
                }
                if (neighbours)
                {
                    defiBossNodePos.Remove(roomSelected);
                    defiBossNodeTemp = new List<Vector2>();
                    foreach (Vector2 node in defiBossNodePos)
                    {
                        defiBossNodeTemp.Add(node);
                    }
                    i--;
                }
                else
                {
                    for (int x = 2; x > 0; x++)
                    {
                        if (!cellule.ContainsKey("challenge" + x.ToString()))
                        {
                            cellule.Add("challenge" + x.ToString(), roomSelected);
                            defiBossNodePos = new List<Vector2>();
                            foreach (Vector2 node in defiBossNodeTemp)
                            {
                                defiBossNodePos.Add(node);
                            }
                            x = -1;
                        }
                    }
                }
            }
            else if (possibilityleft / 2 < defiBoss - i - 0.5f)
            {
                defiBossNodePos.Remove(roomSelected);
                defiBossNodeTemp = new List<Vector2>();
                foreach (Vector2 node in defiBossNodePos)
                {
                    defiBossNodeTemp.Add(node);
                }
                i--;
            }
            else
            {
                for (int x = 2; x > 0; x++)
                {
                    if (!cellule.ContainsKey("challenge" + x.ToString()))
                    {
                        cellule.Add("challenge" + x.ToString(), roomSelected);
                        defiBossNodePos = new List<Vector2>();
                        foreach (Vector2 node in defiBossNodeTemp)
                        {
                            defiBossNodePos.Add(node);
                        }
                        x = -1;
                    }
                }
            }
        }

        for (int i = 1; i > 0; i++)
        {
            if (cellule.ContainsKey("challenge" + i.ToString()))
            {
                marchandBossNodePos.Remove(cellule["challenge" + i.ToString()]);
                marchandBossNodeTemp.Remove(cellule["challenge" + i.ToString()]);
            }
            else i = -1;
        }

        for (int i = 1; i <= nombreMarchandBoss; i++)
        {
            Vector2 roomSelected = marchandBossNodeTemp[TirageAuSort(0, marchandBossNodeTemp.Count - 1)];

            marchandBossNodeTemp.Remove(roomSelected);

            foreach (Vector2 dir in direction)
            {
                if (marchandBossNodeTemp.Contains(roomSelected + dir)) marchandBossNodeTemp.Remove(roomSelected + dir);
            }

            float possibilityleft = marchandBossNodeTemp.Count;
            if (marchandBossNodeTemp.Count == nombreMarchandBoss - i)
            {
                bool neighbours = false;
                foreach (Vector2 node in marchandBossNodeTemp)
                {
                    foreach (Vector2 dir in direction)
                    {
                        if (marchandBossNodeTemp.Contains(node + dir)) neighbours = true;
                    }
                }
                if (neighbours)
                {
                    marchandBossNodePos.Remove(roomSelected);
                    marchandBossNodeTemp = new List<Vector2>();
                    foreach (Vector2 node in marchandBossNodePos)
                    {
                        marchandBossNodeTemp.Add(node);
                    }
                    i--;
                }
                else
                {
                    for (int x = 2; x > 0; x++)
                    {
                        if (!cellule.ContainsKey("seller" + x.ToString()))
                        {
                            cellule.Add("seller" + x.ToString(), roomSelected);
                            marchandBossNodePos = new List<Vector2>();
                            foreach (Vector2 node in marchandBossNodeTemp)
                            {
                                marchandBossNodePos.Add(node);
                            }
                            x = -1;
                        }
                    }
                }
            }
            else if (possibilityleft / 2 < nombreMarchandBoss - i - 0.5f)
            {
                marchandBossNodePos.Remove(roomSelected);
                marchandBossNodeTemp = new List<Vector2>();
                foreach (Vector2 node in marchandBossNodePos)
                {
                    marchandBossNodeTemp.Add(node);
                }
                i--;
            }
            else
            {
                for (int x = 2; x > 0; x++)
                {
                    if (!cellule.ContainsKey("seller" + x.ToString()))
                    {
                        cellule.Add("seller" + x.ToString(), roomSelected);
                        marchandBossNodePos = new List<Vector2>();
                        foreach (Vector2 node in marchandBossNodeTemp)
                        {
                            marchandBossNodePos.Add(node);
                        }
                        x = -1;
                    }
                }
            }
        }

        foreach (Vector2 node in cellule.Values)
        {
            enemyNode.Remove(node);
        }
        foreach (Vector2 node in enemyNode)
        {
            for (int i = 1; i > 0; i++)
            {
                if (!cellule.ContainsKey("enemy" + i.ToString()))
                {
                    cellule.Add("enemy" + i.ToString(), node);
                    i = -1;
                }
            }
        }
    }

    void DungeonPathGeneration()
    {
        int roomSelected = TirageAuSort(0, bossPath.Count - 1);
        int directionSelected = TirageAuSort(0, 3);

        if (blackList.Contains(bossPath[roomSelected]))
        {
            DungeonPathGeneration();
            return;
        }

        for (int i = 0; i < 5; i++)
        {
            if (i == 4)
            {
                blackList.Add(bossPath[roomSelected]);
                DungeonPathGeneration();
                return;
            }
            if (bossPath.Contains(bossPath[roomSelected] + direction[directionSelected]))
            {
                if (directionSelected == 3) directionSelected = -1;
                directionSelected++;
            }
            else
            {
                if (HowManyNeighbour(bossPath[roomSelected] + direction[directionSelected], false) <= 1)
                {
                    if (HowManyNeighbour(bossPath[roomSelected], false) <= 1)
                    {
                        DungeonPathGeneration();
                        return;
                    }
                    else
                    {
                        bossPath.Add(bossPath[roomSelected] + direction[directionSelected]);
                        i = 5;
                    }
                }
                else
                {
                    bossPath.Add(bossPath[roomSelected] + direction[directionSelected]);
                    i = 5;
                }
            }
        }
    }

    void DungeonPathGenerationStart()
    {
        ChestRoomPossibilityCheckStart();
        ChallengeRoomPossibilityCheck();
        SellerPossibilityCheck();
        BlackSellerPossibilityCheck();

        DungeonRoomAttribution();
    }

    int HowManyNeighbour(Vector2 roomPosition, bool chestTest)
    {
        int neighbourCount = 0;

        if (!chestTest)
        {
            foreach (Vector2 dir in direction)
            {
                if (bossPath.Contains(roomPosition + dir)) neighbourCount++;
                else if (chestTest)
                {
                    neighbourCount = 0;
                    foreach (Vector2 check in direction)
                    {
                        if (bossPath.Contains(roomPosition + dir + check)) neighbourCount++;
                    }
                }
            }
        }

        return neighbourCount;
    }

    void ChestRoomPossibilityCheckStart()
    {
        foreach (Vector2 node in bossPath)
        {
            ChestRoomPossibilityCheckUpdate(node);
        }
        foreach (Vector2 node in cellule.Values)
        {
            if (chestPossibility.Contains(node)) chestPossibility.Remove(node);
        }
        while (nombreDeCoffre >= chestPossibility.Count)
        {
            DungeonPathGeneration();
            ChestRoomPossibilityCheckUpdate(bossPath[bossPath.Count - 1]);
        }
    }

    void ChestRoomPossibilityCheckUpdate(Vector2 node)
    {
        int chestOkay = 0;
        if (HowManyNeighbour(node, false) == 1)
        {
            if (!chestPossibility.Contains(node)) chestPossibility.Add(node);
        }
        else if (HowManyNeighbour(node, false) == 3)
        {
            if (HowManyNeighbour(node, true) == 1)
            {
                if (!chestPossibility.Contains(node)) chestPossibility.Add(node);
            }
        }
        else if (HowManyNeighbour(node, false) == 4)
        {
            foreach (Vector2 dir in direction)
            {
                if (HowManyNeighbour(node + dir, false) == 4) chestOkay++;
            }
            if (chestOkay == 4)
            {
                if (!chestPossibility.Contains(node)) chestPossibility.Add(node);
            }
        }
    }

    void ChallengeRoomPossibilityCheck()
    {
        foreach (Vector2 node in bossPath)
        {
            challengePossibility.Add(node);
        }
        foreach (Vector2 node in cellule.Values)
        {
            challengePossibility.Remove(node);
        }
        for (int x = 1; x > 0; x++)
        {
            if (cellule.ContainsKey("challenge" + x.ToString()))
            {
                foreach (Vector2 dir in direction)
                {
                    challengePossibility.Remove(cellule["challenge" + x.ToString()] + dir);
                }
            }
            else x = -1;
        }
        if ((challengePossibility.Count / 2) < nombreDeDefi)
        {
            DungeonPathGeneration();
            DungeonPathGenerationStart();
        }
    }

    void SellerPossibilityCheck()
    {
        foreach (Vector2 node in bossPath)
        {
            sellerPossibility.Add(node);
        }
        foreach (Vector2 node in cellule.Values)
        {
            sellerPossibility.Remove(node);
        }
        for (int x = 1; x > 0; x++)
        {
            if (cellule.ContainsKey("seller" + x.ToString()))
            {
                foreach (Vector2 dir in direction)
                {
                    sellerPossibility.Remove(cellule["seller" + x.ToString()] + dir);
                }
            }
            else x = -1;
        }
        if ((sellerPossibility.Count / 2) < nombreDeMarchand)
        {
            DungeonPathGeneration();
            DungeonPathGenerationStart();
        }
    }

    void BlackSellerPossibilityCheck()
    {
        foreach (Vector2 node in bossPath)
        {
            blackSellerPossibility.Add(node);
        }
        foreach (Vector2 node in cellule.Values)
        {
            blackSellerPossibility.Remove(node);
        }
        for (int x = 1; x > 0; x++)
        {
            if (cellule.ContainsKey("seller" + x.ToString()))
            {
                foreach (Vector2 dir in direction)
                {
                    blackSellerPossibility.Remove(cellule["seller" + x.ToString()] + dir);
                }
            }
            else x = -1;
        }
        if (blackSellerPossibility.Count - nombreDeMarchand - nombreDeDefi < nombreDeMarchandNoir)
        {
            DungeonPathGeneration();
            DungeonPathGenerationStart();
        }
    }

    void DungeonRoomAttribution()
    {
        List<Vector2> challengePossibilityTemp = new List<Vector2>();
        List<Vector2> sellerPossibilityTemp = new List<Vector2>();
        List<Vector2> blackSellerPossibilityTemp = new List<Vector2>();
        List<Vector2> chestPossibilityTemp = new List<Vector2>();
        List<Vector2> enemyNode = new List<Vector2>();

        foreach (Vector2 node in challengePossibility)
        {
            challengePossibilityTemp.Add(node);
        }
        foreach (Vector2 node in sellerPossibility)
        {
            sellerPossibilityTemp.Add(node);
        }
        foreach (Vector2 node in blackSellerPossibility)
        {
            blackSellerPossibilityTemp.Add(node);
        }
        foreach (Vector2 node in chestPossibility)
        {
            chestPossibilityTemp.Add(node);
        }
        foreach (Vector2 node in bossPath)
        {
            enemyNode.Add(node);
        }
        for (int i = 1; i <= nombreDeCoffre; i++)
        {
            Vector2 roomSelected = chestPossibilityTemp[TirageAuSort(0, chestPossibilityTemp.Count - 1)];

            chestPossibilityTemp.Remove(roomSelected);

            /*for (int x = 1; x > 0; x++)
            {
                if (!cellule.ContainsKey("chest" + x.ToString()))
                {
                    cellule.Add("chest" + x.ToString(), roomSelected);
                    x = -1;
                }
            }*/
            foreach (Vector2 dir in direction)
            {
                if (chestPossibilityTemp.Contains(roomSelected + dir)) chestPossibilityTemp.Remove(roomSelected + dir);
            }

            float possibilityleft = chestPossibilityTemp.Count;
            if (chestPossibilityTemp.Count == nombreDeCoffre - i)
            {
                bool neighbours = false;
                foreach (Vector2 node in chestPossibilityTemp)
                {
                    foreach (Vector2 dir in direction)
                    {
                        if (chestPossibilityTemp.Contains(node + dir)) neighbours = true;
                    }
                }
                if (neighbours)
                {
                    chestPossibility.Remove(roomSelected);
                    chestPossibilityTemp = new List<Vector2>();
                    foreach (Vector2 node in chestPossibility)
                    {
                        chestPossibilityTemp.Add(node);
                    }
                    i--;
                }
                else
                {
                    for (int x = 1; x > 0; x++)
                    {
                        if (!cellule.ContainsKey("chest" + x.ToString()))
                        {
                            cellule.Add("chest" + x.ToString(), roomSelected);
                            chestPossibility = new List<Vector2>();
                            foreach (Vector2 node in chestPossibilityTemp)
                            {
                                chestPossibility.Add(node);
                            }
                            x = -1;
                        }
                    }
                }
            }
            else if (possibilityleft / 2 < nombreDeCoffre - i - 0.5f)
            {
                chestPossibility.Remove(roomSelected);
                chestPossibilityTemp = new List<Vector2>();
                foreach (Vector2 node in chestPossibility)
                {
                    chestPossibilityTemp.Add(node);
                }
                i--;
            }
            else
            {
                for (int x = 1; x > 0; x++)
                {
                    if (!cellule.ContainsKey("chest" + x.ToString()))
                    {
                        cellule.Add("chest" + x.ToString(), roomSelected);
                        chestPossibility = new List<Vector2>();
                        foreach (Vector2 node in chestPossibilityTemp)
                        {
                            chestPossibility.Add(node);
                        }
                        x = -1;
                    }
                }
            }
            if (chestPossibility.Count == 0) i = nombreDeCoffre + 1;
        }

        foreach (Vector2 node in cellule.Values)
        {
            challengePossibility.Remove(node);
            challengePossibilityTemp.Remove(node);
        }

        for (int i = 1; i <= nombreDeDefi; i++)
        {
            Vector2 roomSelected = challengePossibilityTemp[TirageAuSort(0, challengePossibilityTemp.Count - 1)];

            challengePossibilityTemp.Remove(roomSelected);

            foreach (Vector2 dir in direction)
            {
                if (challengePossibilityTemp.Contains(roomSelected + dir)) challengePossibilityTemp.Remove(roomSelected + dir);
            }

            float possibilityleft = challengePossibilityTemp.Count;
            if (challengePossibilityTemp.Count == nombreDeDefi - i)
            {
                bool neighbours = false;
                foreach (Vector2 node in challengePossibilityTemp)
                {
                    foreach (Vector2 dir in direction)
                    {
                        if (challengePossibilityTemp.Contains(node + dir)) neighbours = true;
                    }
                }
                if (neighbours)
                {
                    challengePossibility.Remove(roomSelected);
                    challengePossibilityTemp = new List<Vector2>();
                    foreach (Vector2 node in challengePossibility)
                    {
                        challengePossibilityTemp.Add(node);
                    }
                    i--;
                }
                else
                {
                    for (int x = 2; x > 0; x++)
                    {
                        if (!cellule.ContainsKey("challenge" + x.ToString()))
                        {
                            cellule.Add("challenge" + x.ToString(), roomSelected);
                            challengePossibility = new List<Vector2>();
                            foreach (Vector2 node in challengePossibilityTemp)
                            {
                                challengePossibility.Add(node);
                            }
                            x = -1;
                        }
                    }
                }
            }
            else if (possibilityleft / 2 < nombreDeDefi - i - 0.5f)
            {
                challengePossibility.Remove(roomSelected);
                challengePossibilityTemp = new List<Vector2>();
                foreach (Vector2 node in challengePossibility)
                {
                    challengePossibilityTemp.Add(node);
                }
                i--;
            }
            else
            {
                for (int x = 2; x > 0; x++)
                {
                    if (!cellule.ContainsKey("challenge" + x.ToString()))
                    {
                        cellule.Add("challenge" + x.ToString(), roomSelected);
                        challengePossibility = new List<Vector2>();
                        foreach (Vector2 node in challengePossibilityTemp)
                        {
                            challengePossibility.Add(node);
                        }
                        x = -1;
                    }
                }
            }
        }

        foreach (Vector2 node in cellule.Values)
        {
            sellerPossibility.Remove(node);
            sellerPossibilityTemp.Remove(node);
        }

        for (int i = 1; i <= nombreDeMarchand; i++)
        {
            Vector2 roomSelected = sellerPossibilityTemp[TirageAuSort(0, sellerPossibilityTemp.Count - 1)];

            sellerPossibilityTemp.Remove(roomSelected);

            foreach (Vector2 dir in direction)
            {
                if (sellerPossibilityTemp.Contains(roomSelected + dir)) sellerPossibilityTemp.Remove(roomSelected + dir);
            }

            float possibilityleft = sellerPossibilityTemp.Count;
            if (sellerPossibilityTemp.Count == nombreDeMarchand - i)
            {
                bool neighbours = false;
                foreach (Vector2 node in sellerPossibilityTemp)
                {
                    foreach (Vector2 dir in direction)
                    {
                        if (sellerPossibilityTemp.Contains(node + dir)) neighbours = true;
                    }
                }
                if (neighbours)
                {
                    sellerPossibility.Remove(roomSelected);
                    sellerPossibilityTemp = new List<Vector2>();
                    foreach (Vector2 node in sellerPossibility)
                    {
                        sellerPossibilityTemp.Add(node);
                    }
                    i--;
                }
                else
                {
                    for (int x = 2; x > 0; x++)
                    {
                        if (!cellule.ContainsKey("seller" + x.ToString()))
                        {
                            cellule.Add("seller" + x.ToString(), roomSelected);
                            sellerPossibility = new List<Vector2>();
                            foreach (Vector2 node in sellerPossibilityTemp)
                            {
                                sellerPossibility.Add(node);
                            }
                            x = -1;
                        }
                    }
                }
            }
            else if (possibilityleft / 2 < nombreDeMarchand - i - 0.5f)
            {
                sellerPossibility.Remove(roomSelected);
                sellerPossibilityTemp = new List<Vector2>();
                foreach (Vector2 node in sellerPossibility)
                {
                    sellerPossibilityTemp.Add(node);
                }
                i--;
            }
            else
            {
                for (int x = 2; x > 0; x++)
                {
                    if (!cellule.ContainsKey("seller" + x.ToString()))
                    {
                        cellule.Add("seller" + x.ToString(), roomSelected);
                        sellerPossibility = new List<Vector2>();
                        foreach (Vector2 node in sellerPossibilityTemp)
                        {
                            sellerPossibility.Add(node);
                        }
                        x = -1;
                    }
                }
            }
        }

        foreach (Vector2 node in cellule.Values)
        {
            blackSellerPossibility.Remove(node);
            blackSellerPossibilityTemp.Remove(node);
        }

        for (int i = 1; i <= nombreDeMarchandNoir; i++)
        {
            Vector2 roomSelected = blackSellerPossibilityTemp[TirageAuSort(0, blackSellerPossibilityTemp.Count - 1)];

            blackSellerPossibilityTemp.Remove(roomSelected);

            foreach (Vector2 dir in direction)
            {
                if (blackSellerPossibilityTemp.Contains(roomSelected + dir)) blackSellerPossibilityTemp.Remove(roomSelected + dir);
            }

            float possibilityleft = blackSellerPossibilityTemp.Count;
            if (blackSellerPossibilityTemp.Count == nombreDeMarchandNoir - i)
            {
                bool neighbours = false;
                foreach (Vector2 node in blackSellerPossibilityTemp)
                {
                    foreach (Vector2 dir in direction)
                    {
                        if (blackSellerPossibilityTemp.Contains(node + dir)) neighbours = true;
                    }
                }
                if (neighbours)
                {
                    blackSellerPossibility.Remove(roomSelected);
                    blackSellerPossibilityTemp = new List<Vector2>();
                    foreach (Vector2 node in blackSellerPossibility)
                    {
                        blackSellerPossibilityTemp.Add(node);
                    }
                    i--;
                }
                else
                {
                    for (int x = 1; x > 0; x++)
                    {
                        if (!cellule.ContainsKey("blackSeller" + x.ToString()))
                        {
                            cellule.Add("blackSeller" + x.ToString(), roomSelected);
                            blackSellerPossibility = new List<Vector2>();
                            foreach (Vector2 node in blackSellerPossibilityTemp)
                            {
                                blackSellerPossibility.Add(node);
                            }
                            x = -1;
                        }
                    }
                }
            }
            else if (possibilityleft / 2 < nombreDeMarchandNoir - i - 0.5f)
            {
                blackSellerPossibility.Remove(roomSelected);
                blackSellerPossibilityTemp = new List<Vector2>();
                foreach (Vector2 node in blackSellerPossibility)
                {
                    blackSellerPossibilityTemp.Add(node);
                }
                i--;
            }
            else
            {
                for (int x = 1; x > 0; x++)
                {
                    if (!cellule.ContainsKey("blackSeller" + x.ToString()))
                    {
                        cellule.Add("blackSeller" + x.ToString(), roomSelected);
                        blackSellerPossibility = new List<Vector2>();
                        foreach (Vector2 node in blackSellerPossibilityTemp)
                        {
                            blackSellerPossibility.Add(node);
                        }
                        x = -1;
                    }
                }
            }
        }

        foreach (Vector2 node in cellule.Values)
        {
            enemyNode.Remove(node);
        }
        foreach (Vector2 node in enemyNode)
        {
            for (int i = 1; i > 0; i++)
            {
                if (!cellule.ContainsKey("enemy" + i.ToString()))
                {
                    cellule.Add("enemy" + i.ToString(), node);
                    i = -1;
                }
            }
        }
    }

    void SortingVector()
    {
        List<Vector2> nodePos = new List<Vector2>();
        List<Vector2> nodePosSorted = new List<Vector2>();
        float[] posX = new float[cellule.Count], posY = new float[cellule.Count];
        int[] roomPerFloor;
        float[] biggestSizeY, biggestSizeX;
        int currentLine;

        foreach (Vector2 node in cellule.Values)
        {
            nodePos.Add(node);
        }

        for (int i = 0; i < nodePos.Count; i++)
        {
            posX[i] = nodePos[i].x;
            posY[i] = nodePos[i].y;
        }
        System.Array.Sort(posX);
        System.Array.Sort(posY);

        //Debug.Log(Mathf.RoundToInt(posY[posY.Length - 1]) + Mathf.Abs(Mathf.RoundToInt(posY[0])) + 1);
        roomPerFloor = new int[Mathf.RoundToInt(posY[posY.Length - 1]) + Mathf.Abs(Mathf.RoundToInt(posY[0])) + 1];
        biggestSizeY = new float[Mathf.RoundToInt(posY[posY.Length - 1]) + Mathf.Abs(Mathf.RoundToInt(posY[0])) + 1];
        biggestSizeX = new float[Mathf.RoundToInt(posX[posX.Length - 1]) + Mathf.Abs(Mathf.RoundToInt(posX[0])) + 1];
        currentLine = Mathf.RoundToInt(posY[0]);
        for (float y = posY[0]; y <= posY[posY.Length - 1]; y++)
        {
            for (float x = posX[0]; x <= posX[posX.Length - 1]; x++)
            {
                if (cellule.ContainsValue(new Vector2(x, y))) //nodePosSorted.Add(new Vector2(x, y));
                {
                    //placement des salles
                    for (int i = 1; i <= nombreDeSalle + distanceBoss; i++)
                    {
                        if (cellule.ContainsKey("enemy" + i.ToString()))
                        {
                            if (cellule["enemy" + i.ToString()] == new Vector2(x, y))
                            {
                                RoomInstantiate("enemy", new Vector3(x, y, 0f));
                                i = nombreDeSalle + distanceBoss;
                            }
                        }

                        if (cellule.ContainsKey("challenge" + i.ToString()))
                        {
                            if (cellule["challenge" + i.ToString()] == new Vector2(x, y))
                            {
                                RoomInstantiate("challenge", new Vector3(x, y, 0f));
                                i = nombreDeSalle + distanceBoss;
                            }
                        }

                        if (cellule.ContainsKey("seller" + i.ToString()))
                        {
                            if (cellule["seller" + i.ToString()] == new Vector2(x, y))
                            {
                                RoomInstantiate("seller", new Vector3(x, y, 0f));
                                i = nombreDeSalle + distanceBoss;
                            }
                        }

                        if (cellule.ContainsKey("chest" + i.ToString()))
                        {
                            if (cellule["chest" + i.ToString()] == new Vector2(x, y))
                            {
                                RoomInstantiate("chest", new Vector3(x, y, 0f));
                                i = nombreDeSalle + distanceBoss;
                            }
                        }

                        if (cellule.ContainsKey("blackSeller" + i.ToString()))
                        {
                            if (cellule["blackSeller" + i.ToString()] == new Vector2(x, y))
                            {
                                RoomInstantiate("darkSeller", new Vector3(x, y, 0f));
                                i = nombreDeSalle + distanceBoss;
                            }
                        }

                        if (cellule.ContainsKey("start" + i.ToString()))
                        {
                            if (cellule["start" + i.ToString()] == new Vector2(x, y))
                            {
                                RoomInstantiate("start", new Vector3(x, y, 0f));
                                i = nombreDeSalle + distanceBoss;
                            }
                        }

                        if (cellule.ContainsKey("boss" + i.ToString()))
                        {
                            if (cellule["boss" + i.ToString()] == new Vector2(x, y))
                            {
                                RoomInstantiate("boss", new Vector3(x, y, 0f));
                                i = nombreDeSalle + distanceBoss;
                            }
                        }
                    }
                }
            }
        }
        GameObject.Instantiate<GameObject>(refObj[0], new Vector3(posX[0], posY[posY.Length - 1], 0f), new Quaternion(0f, 0f, 0f, 0f));
        GameObject.Instantiate<GameObject>(refObj[1], new Vector3(posX[posX.Length - 1], posY[0], 0f), new Quaternion(0f, 0f, 0f, 0f));

        //Trouver les salles les plus gauches par lignes.
        float currentStage = posY[0];
        xExtremis.Add(roomTransf[0]);
        foreach (GameObject room in roomTransf)
        {
            if (room.transform.position.y != currentStage)
            {
                currentStage++;
                xExtremis.Add(room);
            }
        }

        //Trouver les salles les plus hautes
        for (float i = posX[0]; i <= posX[posX.Length - 1]; i++)
        {
            
            RaycastHit2D[] hit = Physics2D.BoxCastAll(new Vector2(i, posY[posY.Length - 1]), new Vector2(0.1f, 0.1f), 0f, Vector2.down, nombreDeSalle + 3000, roomLayer);

            if (hit.Length != 0)
            {
                yExtremis.Add(hit[0].collider.gameObject);
            }
        }

        for (int i = 0; i < xExtremis.Count; i++)
        {
            RayCastDetection(Vector2.right, xExtremis[i].transform.position, new Vector2(posX[0], posY[0]));
        }

        /*foreach (GameObject obj in xExtremis)
        {
            if (posX[0] > obj.transform.position.x) posX[0] = obj.transform.position.x;
        }*/

        for (int i = 0; i < yExtremis.Count; i++)
        {
            RayCastDetection(Vector2.down, yExtremis[i].transform.position, new Vector2(posX[posX.Length - 1], posY[0]));
        }
        this.GetComponent<Core>().SellerStart();

        Transform[] refPos = new Transform[2];
        refPos[0] = GameObject.Find("RoomReference1(Clone)").GetComponent<Transform>();
        refPos[1] = GameObject.Find("RoomReference2(Clone)").GetComponent<Transform>();

        GameObject.Find("Playable_Character").transform.position = GameObject.Find("Spawn_Point").transform.position;
        player.SetActive(true);
    }

    void RoomInstantiate(string roomName, Vector3 pos)
    {
        Collider2D collider;
        int roomSelected;
        if (roomName == "enemy")
        {
            roomSelected = TirageAuSort(0, enemyRoom.Count - 1);

            roomPos.Add(enemyRoom[roomSelected]);
            GameObject.Instantiate<GameObject>(roomPos[roomPos.Count - 1], pos, new Quaternion(0f, 0f, 0f, 0f));
            sizeX.Add(enemyRoom[roomSelected].GetComponent<tailleDeLaSalle>().sixeX);
            sizeY.Add(enemyRoom[roomSelected].GetComponent<tailleDeLaSalle>().sizeY);
        }
        else if (roomName == "challenge")
        {
            roomSelected = TirageAuSort(0, challengeRoom.Count - 1);
            roomPos.Add(challengeRoom[roomSelected]);
            GameObject.Instantiate<GameObject>(roomPos[roomPos.Count - 1], pos, new Quaternion(0f, 0f, 0f, 0f));
            sizeX.Add(challengeRoom[roomSelected].GetComponent<tailleDeLaSalle>().sixeX);
            sizeY.Add(challengeRoom[roomSelected].GetComponent<tailleDeLaSalle>().sizeY);
        }
        else if (roomName == "seller")
        {
            roomSelected = TirageAuSort(0, sellerRoom.Count - 1);
            roomPos.Add(sellerRoom[roomSelected]);
            GameObject.Instantiate<GameObject>(roomPos[roomPos.Count - 1], pos, new Quaternion(0f, 0f, 0f, 0f));
            sizeX.Add(sellerRoom[roomSelected].GetComponent<tailleDeLaSalle>().sixeX);
            sizeY.Add(sellerRoom[roomSelected].GetComponent<tailleDeLaSalle>().sizeY);
        }
        else if (roomName == "chest")
        {
            roomSelected = TirageAuSort(0, chestRoom.Count - 1);
            roomPos.Add(chestRoom[roomSelected]);
            GameObject.Instantiate<GameObject>(roomPos[roomPos.Count - 1], pos, new Quaternion(0f, 0f, 0f, 0f));
            sizeX.Add(chestRoom[roomSelected].GetComponent<tailleDeLaSalle>().sixeX);
            sizeY.Add(chestRoom[roomSelected].GetComponent<tailleDeLaSalle>().sizeY);
        }
        else if (roomName == "darkSeller")
        {
            roomSelected = TirageAuSort(0, darkSellerRoom.Count - 1);
            roomPos.Add(darkSellerRoom[roomSelected]);
            GameObject.Instantiate<GameObject>(roomPos[roomPos.Count - 1], pos, new Quaternion(0f, 0f, 0f, 0f));
            sizeX.Add(darkSellerRoom[roomSelected].GetComponent<tailleDeLaSalle>().sixeX);
            sizeY.Add(darkSellerRoom[roomSelected].GetComponent<tailleDeLaSalle>().sizeY);
        }
        else if (roomName == "start")
        {
            roomSelected = TirageAuSort(0, startRoom.Count - 1);
            roomPos.Add(startRoom[roomSelected]);
            GameObject.Instantiate<GameObject>(roomPos[roomPos.Count - 1], pos, new Quaternion(0f, 0f, 0f, 0f));
            sizeX.Add(startRoom[roomSelected].GetComponent<tailleDeLaSalle>().sixeX);
            sizeY.Add(startRoom[roomSelected].GetComponent<tailleDeLaSalle>().sizeY);
        }
        else
        {
            roomSelected = TirageAuSort(0, bossRoom.Count - 1);
            roomPos.Add(bossRoom[roomSelected]);
            GameObject.Instantiate<GameObject>(roomPos[roomPos.Count - 1], pos, new Quaternion(0f, 0f, 0f, 0f));
            sizeX.Add(bossRoom[roomSelected].GetComponent<tailleDeLaSalle>().sixeX);
            sizeY.Add(bossRoom[roomSelected].GetComponent<tailleDeLaSalle>().sizeY);
        }
        collider = Physics2D.OverlapCircle(pos, 0.1f);
        roomTransf.Add(collider.gameObject);
    }

    void RayCastDetection(Vector2 direction, Vector2 pos, Vector2 deep)
    {
        Transform[] refPos = new Transform[2];
        refPos[0] = GameObject.Find("RoomReference1(Clone)").GetComponent<Transform>();
        refPos[1] = GameObject.Find("RoomReference2(Clone)").GetComponent<Transform>();


        RaycastHit2D[] hit = Physics2D.BoxCastAll(pos, new Vector2(0.1f, 0.1f), 0f, direction, nombreDeSalle + 3000, roomLayer);
        for (int i = 0; i < hit.Length - 1; i++)
        {
            float[] sizeX = new float[2], sizeY = new float[2], posX = new float[2], posY = new float[2];

            sizeX[0] = hit[i].collider.GetComponent<tailleDeLaSalle>().sixeX;
            sizeX[1] = hit[i + 1].collider.GetComponent<tailleDeLaSalle>().sixeX;

            sizeY[0] = hit[i].collider.GetComponent<tailleDeLaSalle>().sizeY;
            sizeY[1] = hit[i + 1].collider.GetComponent<tailleDeLaSalle>().sizeY;

            posX[0] = hit[i].collider.transform.position.x;
            posX[1] = hit[i + 1].collider.transform.position.x;
            
            posY[0] = hit[i].collider.transform.position.y;
            posY[1] = hit[i + 1].collider.transform.position.y;

            if (direction == Vector2.right)
            {
                hit[i].collider.GetComponent<tailleDeLaSalle>().teleporteurPosition[1].GetComponent<Teleporteur>().linkedTeleport = hit[i + 1].collider.GetComponent<tailleDeLaSalle>().teleporteurPosition[3];
                hit[i + 1].collider.GetComponent<tailleDeLaSalle>().teleporteurPosition[3].GetComponent<Teleporteur>().linkedTeleport = hit[i].collider.GetComponent<tailleDeLaSalle>().teleporteurPosition[1];
                /*if ((sizeX[0] + sizeX[1]) / 2 > Mathf.Abs(posX[1] - posX[0]))
                {
                    Vector2 start = new Vector2((refPos[0].position.x - posX[0]) / 2 + posX[0], posY[0]);
                    Vector2 size = new Vector2(Mathf.Abs(refPos[0].position.x - posX[0]) + 0.1f, (refPos[0].position.y - deep.y) * 2 + 0.1f);
                    float newDistance = (sizeX[0] + sizeX[1]) / 2 + marge;

                    Collider2D[] roomCol = Physics2D.OverlapBoxAll(start, size, 0f, roomLayer);
                    if (roomCol.Length != 0)
                    {
                        foreach (Collider2D col in roomCol)
                        {
                            col.transform.position -= new Vector3(newDistance, 0f, 0f);
                        }
                        refPos[0].position -= new Vector3(newDistance, 0f, 0f);
                    }
                }*/
            }
            else
            {
                hit[i].collider.GetComponent<tailleDeLaSalle>().teleporteurPosition[2].GetComponent<Teleporteur>().linkedTeleport = hit[i + 1].collider.GetComponent<tailleDeLaSalle>().teleporteurPosition[0];
                hit[i + 1].collider.GetComponent<tailleDeLaSalle>().teleporteurPosition[0].GetComponent<Teleporteur>().linkedTeleport = hit[i].collider.GetComponent<tailleDeLaSalle>().teleporteurPosition[2];
                /*if ((sizeY[0] + sizeY[1]) / 2 > Mathf.Abs(posY[1] - posY[0]))
                {
                    Vector2 start = new Vector2(posX[0], (refPos[0].position.y - posY[0]) / 2 + posY[0]);
                    Vector2 size = new Vector2(Mathf.Abs(deep.x - refPos[0].position.x) * 2 + 0.1f, Mathf.Abs(refPos[0].position.y - posY[0]) + 0.1f);
                    float newDistance = (sizeY[0] + sizeY[1]) / 2 + marge;

                    Collider2D[] roomCol = Physics2D.OverlapBoxAll(start, size, 0f, roomLayer);

                    Debug.Log(roomCol.Length);
                    if (roomCol.Length != 0)
                    {
                        foreach (Collider2D col in roomCol)
                        {
                            col.transform.position += new Vector3(0f, newDistance, 0f);
                        }
                        refPos[0].position += new Vector3(0f, newDistance, 0f);
                    }
                }*/
            }
        }
    }

    void Secoure()
    {
        Transform[] refPos = new Transform[2];
        refPos[0] = GameObject.Find("RoomReference1(Clone)").GetComponent<Transform>();
        refPos[1] = GameObject.Find("RoomReference2(Clone)").GetComponent<Transform>();

        float newDistance = (sizeX[0] + sizeX[1]) / 2 + marge;

        Collider2D[] roomCol = Physics2D.OverlapBoxAll(transform.position, new Vector2(10000, 10000), 0f, roomLayer);

        foreach (Collider2D room in roomCol)
        {
            room.transform.position *= marge;
        }
    }
}
