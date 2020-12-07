using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlacementLD : MonoBehaviour
{
    public LevelGenerationInfo levelInfo;
    private int distanceBossMin, distanceBossMax, nombreDeSalleMin, nombreDeSalleMax, nombreCoffreMin, nombreCoffreMax, nombreMarchandMin, nombreMarchandMax, nombreMarchandMauditMin, nombreMarchandMauditMax, nombreDeSalleDeDefiOutBossMax, nombreDeSalleDeDefiOutBossMin, nombreDeSalleDeDefiForBossMax, nombreDeSalleDeDefiForBossMin;
    private int distanceBoss, nombreDeSalle, nombreCoffre, nombreMarchand, nombreMarchandMaudit, nombreDeSalleDeDefiOutBoss, nombreDeSalleDeDefiForBoss;
    private int rightSide = 1, highPos, xPos, yPos;
    public int repereASelectionner;
    int doorUsable;
    public GameObject[] room;

    private List<Vector2> nodePosition = new List<Vector2>();
    private List<Vector2> nodeBlackList = new List<Vector2>();

    private Dictionary<string, Vector2> bossRoom = new Dictionary<string, Vector2>();

    public bool north, est, south, west;

	private void Awake()
	{
        distanceBossMin = levelInfo.distanceBossMin;
        distanceBossMax = levelInfo.distanceBossMax;
        nombreDeSalleMin = levelInfo.nombreDeSalleMin;
        nombreDeSalleMax = levelInfo.nombreDeSalleMax;
        nombreCoffreMin = levelInfo.nombreCoffreMin;
        nombreCoffreMax = levelInfo.nombreCoffreMax;
        nombreMarchandMin = levelInfo.nombreMarchandMin;
        nombreMarchandMax = levelInfo.nombreMarchandMax;
        nombreMarchandMauditMin = levelInfo.nombreMarchandMauditMin;
        nombreMarchandMauditMax = levelInfo.nombreMarchandMauditMax;
        nombreDeSalleDeDefiOutBossMin = levelInfo.nombreDeSalleDeDefiOutBossMin;
        nombreDeSalleDeDefiOutBossMax = levelInfo.nombreDeSalleDeDefiOutBossMax;
        nombreDeSalleDeDefiForBossMax = levelInfo.nombreDeSalleDeDefiForBossMax;
        nombreDeSalleDeDefiForBossMin = levelInfo.nombreDeSalleDeDefiForBossMin;

    }
    void Start()
    {
        if (distanceBossMin <= 0)
        {
            distanceBossMin = 1;
            Debug.Log("La valeur de la variable 'distanceBossMin' ne peux pas être inférieur ou égale à 0, c'est pour cela que nous l'avons mis par défaut à 1.");
        }
        if (nombreDeSalleMin < (distanceBossMin + nombreCoffreMin + nombreMarchandMauditMin + nombreMarchandMin + nombreDeSalleDeDefiOutBossMin + nombreDeSalleDeDefiForBossMin))
        {
            nombreDeSalleMin = (distanceBossMin + nombreCoffreMin + nombreMarchandMauditMin + nombreMarchandMin + nombreDeSalleDeDefiOutBossMin + nombreDeSalleDeDefiForBossMin);
            Debug.Log("La valeur de la variable 'nombreDeSalleMin' ne peux pas être inférieur à " + (distanceBossMin + nombreCoffreMin + nombreMarchandMauditMin + nombreMarchandMin + nombreDeSalleDeDefiOutBossMin + nombreDeSalleDeDefiForBossMin) + " , c'est pour cela que nous l'avons mis par défaut à " + (distanceBossMin + nombreCoffreMin + nombreMarchandMauditMin + nombreMarchandMin + nombreDeSalleDeDefiOutBossMin + nombreDeSalleDeDefiForBossMin) + ".");
        }
        if (nombreCoffreMin <= 0)
        {
            nombreCoffreMin = 1;
            Debug.Log("La valeur de la variable 'nombreCoffreMin' ne peux pas être inférieur ou égale à 0, c'est pour cela que nous l'avons mis par défaut à 1.");
        }
        if (nombreMarchandMauditMin <= 0)
        {
            nombreMarchandMauditMin = 1;
            Debug.Log("La valeur de la variable 'nombreMarchandMauditMin' ne peux pas être inférieur ou égale à 0, c'est pour cela que nous l'avons mis par défaut à 1.");
        }
        if (nombreMarchandMin <= 0)
        {
            nombreMarchandMin = 1;
            Debug.Log("La valeur de la variable 'nombreMarchandMin' ne peux pas être inférieur ou égale à 0, c'est pour cela que nous l'avons mis par défaut à 1.");
        }
        if (nombreDeSalleDeDefiOutBossMin <= 0)
        {
            nombreDeSalleDeDefiOutBossMin = 1;
            Debug.Log("La valeur de la variable 'nombreDeSalleDeDefiOutBossMin' ne peux pas être inférieur ou égale à 0, c'est pour cela que nous l'avons mis par défaut à 1.");
        }
        if (nombreDeSalleDeDefiForBossMin <= 0)
        {
            nombreDeSalleDeDefiForBossMin = 1;
            Debug.Log("La valeur de la variable 'nombreDeSalleDeDefiForBossMin' ne peux pas être inférieur ou égale à 0, c'est pour cela que nous l'avons mis par défaut à 1.");
        }
        if (distanceBossMax < distanceBossMin)
        {
            distanceBossMax = distanceBossMin;
            Debug.Log("La valeur de la variable 'distanceBossMax' ne peux pas être inférieur à la variable 'distanceBossMin'. Par conséquent, 'distanceBossMax' est désormais égale à 'distanceBossMin'.");
        }
        if (nombreDeSalleMax < nombreDeSalleMin)
        {
            nombreDeSalleMax = nombreDeSalleMin;
            Debug.Log("La valeur de la variable 'nombreDeSalleMax' ne peux pas être inférieur à la variable 'nombreDeSalleMin'. Par conséquent, 'nombreDeSalleMax' est désormais égale à 'nombreDeSalleMin'.");
        }
        if (nombreCoffreMax < nombreCoffreMin)
        {
            nombreCoffreMax = nombreCoffreMin;
            Debug.Log("La valeur de la variable 'nombreCoffreMax' ne peux pas être inférieur à la variable 'nombreCoffreMin'. Par conséquent, 'nombreCoffreMax' est désormais égale à 'nombreCoffreMin'.");
        }
        if (nombreMarchandMax < nombreMarchandMin)
        {
            nombreMarchandMax = nombreMarchandMin;
            Debug.Log("La valeur de la variable 'nombreMarchandMax' ne peux pas être inférieur à la variable 'nombreMarchandMin'. Par conséquent, 'nombreMarchandMax' est désormais égale à 'nombreMarchandMin'.");
        }
        if (nombreMarchandMauditMax < nombreMarchandMauditMin)
        {
            nombreMarchandMauditMax = nombreMarchandMauditMin;
            Debug.Log("La valeur de la variable 'nombreMarchandMauditMax' ne peux pas être inférieur à la variable 'nombreMarchandMauditMin'. Par conséquent, 'nombreMarchandMauditMax' est désormais égale à 'nombreMarchandMauditMin'.");
        }
        if (nombreDeSalleDeDefiOutBossMax < nombreDeSalleDeDefiOutBossMin)
        {
            nombreDeSalleDeDefiOutBossMax = nombreDeSalleDeDefiOutBossMin;
            Debug.Log("La valeur de la variable 'nombreDeSalleDeDefiOutBossMax' ne peux pas être inférieur à la variable 'nombreDeSalleDeDefiOutBossMin'. Par conséquent, 'nombreDeSalleDeDefiOutBossMax' est désormais égale à 'nombreDeSalleDeDefiOutBossMin'.");
        }
        if (nombreDeSalleDeDefiForBossMax < nombreDeSalleDeDefiForBossMin)
        {
            nombreDeSalleDeDefiForBossMax = nombreDeSalleDeDefiForBossMin;
            Debug.Log("La valeur de la variable 'nombreDeSalleDeDefiForBossMax' ne peux pas être inférieur à la variable 'nombreDeSalleDeDefiForBossMin'. Par conséquent, 'nombreDeSalleDeDefiForBossMax' est désormais égale à 'nombreDeSalleDeDefiForBossMin'.");
        }

        //Tirage au sort (equiprobable) afin de fixer le nombre les infos du niveau qui va être généré. 

        distanceBoss = TirageAuSort(distanceBossMax, distanceBossMin);
        nombreCoffre = TirageAuSort(nombreCoffreMax, nombreCoffreMin);
        nombreMarchand = TirageAuSort(nombreMarchandMauditMax, nombreMarchandMauditMin);
        nombreMarchandMaudit = TirageAuSort(nombreMarchandMauditMax, nombreMarchandMauditMin);
        nombreDeSalleDeDefiOutBoss = TirageAuSort(nombreDeSalleDeDefiOutBossMax, nombreDeSalleDeDefiOutBossMin);
        nombreDeSalleDeDefiForBoss = TirageAuSort(nombreDeSalleDeDefiForBossMax, nombreDeSalleDeDefiForBossMin);
        nombreDeSalle = TirageAuSort(nombreDeSalleMax, nombreDeSalleMin);
        if (nombreDeSalle < (distanceBoss + nombreCoffre + nombreMarchand + nombreMarchandMaudit + nombreDeSalleDeDefiOutBoss + nombreDeSalleDeDefiForBoss)) nombreDeSalle = (distanceBoss + nombreCoffre + nombreMarchand + nombreMarchandMaudit + nombreDeSalleDeDefiOutBoss + nombreDeSalleDeDefiForBoss);

        //Creation du Tron Principal
        //Position de la salle du boss.
        //Gauche ou droite ?
        if (TirageAuSort(1, 0) == 0) rightSide = -1;
        if (TirageAuSort(1, 0) == 0) highPos = 1;

        yPos = TirageAuSort((distanceBoss + highPos), 1);
        xPos = ((distanceBoss + 1) - yPos) * rightSide;

        nodePosition.Add(new Vector2(xPos, yPos));

        //Node entre la salle du début et la salle du bosse

        for (int i = 0; i < distanceBoss; i++)
        {
            if (yPos > 0 && xPos != 0)
            {
                if (TirageAuSort(1, 0) == 0) yPos--;
                else xPos -= 1 * rightSide;
            }
            else if (yPos > 0 && xPos == 0) yPos--;
            else if (yPos == 0 && xPos != 0) xPos -= 1 * rightSide;

            nodePosition.Add(new Vector2(xPos, yPos));
        }

        //Placer les nodes supplémentaire
        while (nombreDeSalle > 0)
        {
            AddNode();
            nombreDeSalle--;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            RandomRoomSelection();
        }
    }

    //Tirage au sort
    int TirageAuSort(int Max, int Min)
    {
        int resultat = Random.Range(Min, Max);
        return resultat;
    }

    //Creation des autres cellules hors sur le chemin du boss
    void AddNode()
    {
        int selectedRoom = TirageAuSort(nodePosition.Count - 1, 0);
        Vector2 roomPos;
        Vector2[] direction = new Vector2[] {new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 0), new Vector2(-1, 0) };
        int directionSelected = TirageAuSort(0, 3);

        roomPos = nodePosition[selectedRoom];
        if (nodeBlackList.Contains(roomPos))
        {
            AddNode();
            return;
        }

        for (int i = 0; i < 5; i++)
        {
            if (i == 4)
            {
                nodeBlackList.Add(roomPos);
                AddNode();
                return;
            }
            if (nodePosition.Contains(roomPos + direction[directionSelected]))
            {
                if (directionSelected == 0) directionSelected++;
                else if (directionSelected == 1) directionSelected++;
                else if (directionSelected == 2) directionSelected++;
                else directionSelected = 0;

                i++;
            }
            else
            {
                if (HowManyNeighbour(roomPos + direction[directionSelected], direction) <= 1)
                {
                    if (HowManyNeighbour(roomPos, direction) <= 1)
                    {
                        AddNode();
                        return;
                    }
                    else
                    {
                        nodePosition.Add(roomPos + direction[directionSelected]);
                        i = 5;
                    }
                }
                else
                {
                    nodePosition.Add(roomPos + direction[directionSelected]);
                    i = 5;
                }
            }
        }
    }

    int HowManyNeighbour(Vector2 roomPosition, Vector2[] direction)
    {
        int neighbourCount = 0;

        foreach (Vector2 dir in direction)
        {
            if (nodePosition.Contains(roomPosition + dir)) neighbourCount++;
        }

        return neighbourCount;
    }
    //Choisir une room aléatoirement
    void RandomRoomSelection()
    {
        int roomNumber = Random.Range(0, room.Length - 1);
        int doorNumber = room[roomNumber].transform.childCount;
        Transform[] doorPosition = new Transform[doorNumber];

        for (int i = 0; i < doorNumber; i++) doorPosition[i] = room[roomNumber].transform.GetChild(i);
        
        Debug.Log("salle numéro : " + roomNumber);
        Debug.Log("il y a " + doorNumber + " portes dans cette salle.");
        Debug.Log("taille du tableau " + doorPosition.Length);

        RoomGeneration(doorPosition);
    }

    //placer la salle
    void RoomGeneration(Transform[] doorPosition)
    {
        foreach (Transform doorAngle in doorPosition)
        {
            if (north) if (doorAngle.rotation.eulerAngles.z == 0f) doorUsable++;
            else if (est) if (doorAngle.rotation.eulerAngles.z == 270f) doorUsable++;
            else if (south) if (doorAngle.rotation.eulerAngles.z == 180f) doorUsable++;
            else
            {
                if (doorAngle.rotation.eulerAngles.z == 90f) doorUsable++;
            }
        }
        if (doorUsable == 0)
        {
            RandomRoomSelection();
            return;
        }
        Debug.Log(doorUsable);
        doorUsable = 0;


    }
}
