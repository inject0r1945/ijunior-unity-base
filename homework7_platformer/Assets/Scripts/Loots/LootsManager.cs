using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootsManager : MonoBehaviour
{
    [Header("Fruits")]
    [SerializeField] private AudioSource _fruitCollectSound;
    [SerializeField] private TMP_Text _fruitsStatisticText;

    private List<Loot> _loots = new List<Loot>();
    private List<Fruit> _fruits = new List<Fruit>();

    public int TotalLootsCount => _loots.Count;

    public int TotalFruitsCount => _fruits.Count;

    public int CollectedFruitsCount { get; private set; }

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Loot currentLoot = transform.GetChild(i).GetComponent<Loot>();

            if (currentLoot)
            {
                _loots.Add(currentLoot);

                Fruit currentFruit = currentLoot as Fruit;

                if (currentFruit)
                    _fruits.Add(currentFruit);
            }   
        }
    }

    private void OnEnable()
    {
        foreach (Fruit currentFruit in _fruits)
        {
            currentFruit.Took += ProcessCollectFruit;
        }  
    }

    private void OnDisable()
    {
        foreach (Fruit currentFruit in _fruits)
        {
            currentFruit.Took -= ProcessCollectFruit;
        }
    }

    private void Update()
    {
        if (_fruitsStatisticText)
            _fruitsStatisticText.text = $"{CollectedFruitsCount} / {TotalFruitsCount}";
    }

    private void ProcessCollectFruit()
    {
        _fruitCollectSound?.Play();
        CollectedFruitsCount++;
    }
}
