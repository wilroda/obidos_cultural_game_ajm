using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PlayerTeam : MonoBehaviour
{
    [SerializeField] private GameObject _followerPrefab;
    [SerializeField] private Person[] _startingTeam;
    [SerializeField] private Transform _followerSpawnParent;


    [SerializeField] private List<Person> _allPersons;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _startingTeam.Length; i++)
        {
            Person p = _startingTeam[i];
            AddNewFollower(p);
            if (_allPersons.Contains(p))
            {
                _allPersons.Remove(p);
            }
        }
    }

    [Button]
    public Person AddRandomFollower()
    {
        if (_allPersons.Count == 0)
            return null;

        Person p = _allPersons[Random.Range(0, _allPersons.Count)];
        _allPersons.Remove(p);

        AddNewFollower(p);

        return p;
    }

    private void AddNewFollower(Person p)
    {
        GameObject newGo = Instantiate(_followerPrefab);
        newGo.transform.position = _followerSpawnParent.GetChild(Random.Range(0, _followerSpawnParent.childCount)).transform.position;
        newGo.GetComponent<CharacterPlanes>().Setup(p);
    }
}
