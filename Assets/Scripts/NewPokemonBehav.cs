using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPokemonBehav : MonoBehaviour
{
    private static NewPokemonBehav _instance;

    [SerializeField] private CharacterPlanes _facePlaces;
    [SerializeField] private Animator _anim;

    [Space]
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _newPokemonSound;
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public static void AnnounceNewPokemon(Person person)
    {
        _instance._facePlaces.Setup(person);
        _instance.gameObject.SetActive(true);

        CoroutineHelper.PerformAfterSeconds(5.1f, () =>
        {
            _instance.gameObject.SetActive(false);
        });
        _instance._source.PlayOneShot(_instance._newPokemonSound);
    }
}
