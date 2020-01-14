using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    private float resourceRed;
    [SerializeField]
    private float resourceGreen;

    [SerializeField]
    Text greens;

    [SerializeField]
    Text reds;

    [SerializeField]
    Text win;

    private int unitsRed;
    private int unitsGreen;

    private int buldingsRed;
    private int buildingsGreen;

    private int wizards;

    public float ResourceRed { get => resourceRed; set => resourceRed = value; }
    public float ResourceGreen { get => resourceGreen; set => resourceGreen = value; }
    public int UnitsRed { get => unitsRed; set => unitsRed = value; }
    public int UnitsGreen { get => unitsGreen; set => unitsGreen = value; }
    public int BuldingsRed { get => buldingsRed; set => buldingsRed = value; }
    public int BuildingsGreen { get => buildingsGreen; set => buildingsGreen = value; }
    public int Wizards { get => wizards; set => wizards = value; }

    void Start()
    {
        greens.text = "Green's Resources:\n" + ResourceGreen;
        reds.text = "Red's Resources: " + ResourceRed;
    }

    void Update() //Updating Resources
    {
        greens.text = "Green's Resources: " + ResourceGreen;
        reds.text = "Red's Resources: " + ResourceRed;
        CheckUnits();
    }

    void CheckUnits() // Declaring Winner
    {
        if (GameObject.FindGameObjectsWithTag("GreenTeam") == null)
        {
            win.text = "RED WINS!";
        }
        else if (GameObject.FindGameObjectsWithTag("RedTeam") == null)
        {
            win.text = "GREEN WINS!";
        }


    }
}