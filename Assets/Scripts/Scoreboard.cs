using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public static bool isGameEnd = false;

    public TournamentManager tournament;
    [SerializeField] GameObject ScoreBoard;
    public Text RankText1;
    public Text RankText2;
    public Text RankText3;
    public Text RankText4;
    public Text RankText5;
    public Text RankText6;
    public Text ScoreText1;
    public Text ScoreText2;
    public Text ScoreText3;
    public Text ScoreText4;
    public Text ScoreText5;
    public Text ScoreText6;
    public Text TimeText1;
    public Text TimeText2;
    public Text TimeText3;
    public Text TimeText4;
    public Text TimeText5;
    public Text TimeText6;
    public Text NameText1;
    public Text NameText2;
    public Text NameText3;
    public Text NameText4;
    public Text NameText5;
    public Text NameText6;
    
    


    
    
    public void PlayAgain()
    {
        
        tournament.nextCircuit();
        ScoreBoard.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BackMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        // Placement
        (int, int, string, string) p1Info = (tournament.car1.placement, tournament.car1.points,
            tournament.car1.Time, tournament.car1.Name);
        (int, int, string, string) p2Info = (0,0,"","");
        if (tournament.car2 != null)
        {
            p2Info = (tournament.car2.placement, tournament.car2.points,
                tournament.car2.Time, tournament.car2.Name);
        }

        (int, int, string, string) Bot1Info = (tournament.listAI[0].placement, tournament.listAI[0].points,
            tournament.listAI[0].Time, tournament.listAI[0].Name);
        (int, int, string, string) Bot2Info = (tournament.listAI[1].placement, tournament.listAI[1].points,
            tournament.listAI[1].Time, tournament.listAI[1].Name);
        (int, int, string, string) Bot3Info = (tournament.listAI[2].placement, tournament.listAI[2].points,
            tournament.listAI[2].Time, tournament.listAI[2].Name);
        (int, int, string, string) Bot4Info = (tournament.listAI[3].placement, tournament.listAI[3].points,
            tournament.listAI[3].Time, tournament.listAI[3].Name);
        (int, int, string, string) Bot5Info = Bot4Info;
        if (tournament.car2 == null)
        {
            Bot5Info = (tournament.listAI[4].placement, tournament.listAI[4].points,
                tournament.listAI[4].Time, tournament.listAI[4].Name);
        }

        List<(int, int, string, string)> PlacementList2  = new List<(int, int, string, string)>();
        PlacementList2.Add(p1Info);
        if (tournament.car2 != null)
        {
            PlacementList2.Add(p2Info);
        }
        PlacementList2.Add(Bot1Info);
        PlacementList2.Add(Bot2Info);
        PlacementList2.Add(Bot3Info);
        PlacementList2.Add(Bot4Info);
        if (tournament.car2 == null)
        {
            PlacementList2.Add(Bot5Info);

        }
        List<(int, int, string, string)> PlacementList1  = new List<(int, int, string, string)>();
        for (int i = 1; i < 7; i++)
        {
            foreach (var j in PlacementList2)
            {
                if (j.Item1 == i)
                {
                    PlacementList1.Add(j);
                }
                
            }
        }
        RankText1.text = PlacementList1[0].Item1.ToString();
        RankText2.text = PlacementList1[1].Item1.ToString();
        RankText3.text = PlacementList1[2].Item1.ToString();
        RankText4.text = PlacementList1[3].Item1.ToString();
        RankText5.text = PlacementList1[4].Item1.ToString();
        RankText6.text = PlacementList1[5].Item1.ToString();
        ScoreText1.text = PlacementList1[0].Item2.ToString();
        ScoreText2.text = PlacementList1[1].Item2.ToString();
        ScoreText3.text = PlacementList1[2].Item2.ToString();
        ScoreText4.text = PlacementList1[3].Item2.ToString();
        ScoreText4.text = PlacementList1[3].Item2.ToString();
        ScoreText5.text = PlacementList1[4].Item2.ToString();
        ScoreText6.text = PlacementList1[5].Item2.ToString();
        
        TimeText1.text = PlacementList1[0].Item3;
        TimeText2.text = PlacementList1[1].Item3;
        TimeText3.text = PlacementList1[2].Item3;
        TimeText4.text = PlacementList1[3].Item3;
        TimeText5.text = PlacementList1[4].Item3;
        TimeText6.text = PlacementList1[5].Item3;
        NameText1.text = PlacementList1[0].Item4;
        NameText2.text = PlacementList1[1].Item4;
        NameText3.text = PlacementList1[2].Item4;
        NameText4.text = PlacementList1[3].Item4;
        NameText5.text = PlacementList1[4].Item4;
        NameText6.text = PlacementList1[5].Item4;
    }
}
