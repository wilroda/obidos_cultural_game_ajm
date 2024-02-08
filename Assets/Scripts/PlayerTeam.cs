using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PlayerTeam : MonoBehaviour
{
    [SerializeField] private Person _lead;
    [SerializeField] private GameObject _followerPrefab;
    [SerializeField] private Person[] _startingTeam;
    [SerializeField] private Transform _followerSpawnParent;


    [SerializeField] private List<Person> _allPersons;

    private List<Person> _followers;

    public List<Person> Followers => _followers;

    public Person Lead => _lead;


    private void Awake()
    {
        _followers = new List<Person>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _allPersons.Remove(_lead);
        for (int i = 0; i < _startingTeam.Length; i++)
        {
            Person p = _startingTeam[i];
            AddNewFollower(p, true);
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

    private void AddNewFollower(Person p, bool supressAnim = false)
    {
        GameObject newGo = Instantiate(_followerPrefab);
        newGo.transform.position = _followerSpawnParent.GetChild(Random.Range(0, _followerSpawnParent.childCount)).transform.position;
        newGo.GetComponent<CharacterPlanes>().Setup(p);
        _followers.Add(p);

        if (!supressAnim)
        {
            NewPokemonBehav.AnnounceNewPokemon(p);
        }
    }
}
