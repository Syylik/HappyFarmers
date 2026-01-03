using System;
using Random = UnityEngine.Random;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(CropVFX))]
public class Crop : MonoBehaviour, ICollectable
{
    private AreaCropData _areaCropData;
    private Sprite[] _cropStages;
    private float _cropGrowTime {get { return _areaCropData.cropGrowTime; } }

    [SerializeField] private SpriteRenderer _cropRenderer1;
    [SerializeField] private SpriteRenderer _cropRenderer2;

    private BoxCollider _cropCollider;
    private CropVFX _cropVFX;

    private Action<Crop> OnGrow;

    private bool _isGrowed = false;

    [Zenject.Inject]
    public void Construct(AreaCropData data, WorkerManager workersManager)
    {
        _areaCropData = data;
        _cropStages = (Sprite[])data.cropStages.Clone();
        OnGrow += workersManager.AddTaskToCollect;
    }

    private void Start() 
    {
        SetCropSprite(_cropStages[0]);
        StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
        WaitForSeconds waiter = new WaitForSeconds(_cropGrowTime + Random.Range(-0.5f, 0.5f));
        for(int i  = 0; i < _cropStages.Length; i++)
        {
            yield return waiter;
            SetCropSprite(_cropStages[i]);
        }
        _isGrowed = true;
        OnGrow?.Invoke(this);
        _cropCollider.enabled = _isGrowed;
    }

    public void Collect()
    {
        if(_isGrowed)
        {
            SetCropSprite(null);
            StartCoroutine(ReGrow());
            _cropVFX.OnCollectVFX();
        }
    }

    private IEnumerator ReGrow()
    {
        _isGrowed = false;
        _cropCollider.enabled = _isGrowed;

        yield return new WaitForSeconds(Random.Range(0.75f, 4f));
        
        SetCropSprite(_cropStages[0]);
        StartCoroutine(Grow());
    }

    private void SetCropSprite(Sprite sprite) { _cropRenderer1.sprite = _cropRenderer2.sprite = sprite; }

    private void OnValidate() 
    { 
        if(_cropCollider == null) _cropCollider ??= GetComponent<BoxCollider>(); 
        _cropVFX ??= GetComponent<CropVFX>(); 
    }
}
