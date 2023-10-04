using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancerAnneau : MonoBehaviour
{
    private bool _marquePoint = false;
    private AnneauGameManager _anneauGameManager;

    private void Start()
    {
        _anneauGameManager = FindObjectOfType<AnneauGameManager>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bottle"))
        {
            _marquePoint = true;
            StopAllCoroutines();
            StartCoroutine(Delai());
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bottle"))
        {
            _marquePoint = false;
        }
    }

    private IEnumerator Delai()
    {
        yield return new WaitForSeconds(3f);
        if ( _marquePoint )
        {
            _anneauGameManager.AugmenterPointage();
        }
    }

}
