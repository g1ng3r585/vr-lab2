using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnneauUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _txtPointage;
    private AnneauGameManager _anneauGameManager;

    private void Start()
    {
        _anneauGameManager = FindObjectOfType<AnneauGameManager>();
        _anneauGameManager.EventUpdatePointage += OnEventUpdatePointage;
    }

    private void OnEventUpdatePointage(int nouveauPointage)
    {
        _txtPointage.text = "Pointage : " + nouveauPointage.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
