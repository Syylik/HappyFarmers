using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new AreaData", menuName = "ScriptableObjects/AreaData")]
public class AreaData : ScriptableObject
{
    public AreaCropData cropData;

    [Header("Worker Settings")]
    public float cropCollectTime;
    
    public AreaUpgradeData areaUpgradeData; 
}

[Serializable]
public class AreaCropData
{
    [Header("Crop settings")]
    public Sprite[] cropStages;
    public float cropGrowTime = 2f;
    
    [SerializeField] private Sprite _areaCropSprite;

    public Sprite areaCropSprite => _areaCropSprite;
}

[Serializable]
public class AreaUpgradeData
{
    [Header("Upgrades")]
    [SerializeField] private Sprite[] _hoesSprites;
    private int _currentHoeId;

    public Sprite GetHoeSprite() {return _hoesSprites[_currentHoeId]; }
    public bool TryUpgradeHoe()
    {
        if(_currentHoeId < _hoesSprites.Length - 1) _currentHoeId++;
        return _currentHoeId == _hoesSprites.Length - 1;
    }
}