using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnneauGameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _anneaux;
    [SerializeField] private int _pointsParAnneau = 10;
    
    private Vector3[] _positionAnneauxDepart;
    private Quaternion[] _rotationAnneauxDepart;
    private int _pointage;

    public event Action<int> EventUpdatePointage;

    private void Start()
    {
        _positionAnneauxDepart = new Vector3[_anneaux.Length];
        _rotationAnneauxDepart = new Quaternion[_anneaux.Length];

        for (int i = 0; i < _anneaux.Length; i++)
        {
            _positionAnneauxDepart[i] = _anneaux[i].transform.position;
            _rotationAnneauxDepart[i] = _anneaux[i] .transform.rotation;
        }    
    }

    public void AugmenterPointage()
    {
        _pointage += _pointsParAnneau;
        EventUpdatePointage?.Invoke(_pointage);
    }

    [ContextMenu("Reset Game")]
    public void ResetGame()
    {
        for (int i = 0; i < _anneaux.Length; i++)
        {
            _anneaux[i].transform.SetPositionAndRotation(_positionAnneauxDepart[i], _rotationAnneauxDepart[i]);
        }

        _pointage = 0;
        EventUpdatePointage?.Invoke(_pointage);
    }
}
