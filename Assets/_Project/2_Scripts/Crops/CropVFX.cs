using UnityEngine;

public class CropVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem _collectParticle;
    // [SerializeField] private AudioSource _collectSound;

    [Zenject.Inject]
    private void Construct(AreaCropData areaCropData)
    {
        _collectParticle.textureSheetAnimation.SetSprite(0, areaCropData.areaCropSprite);   
    }

    public void OnCollectVFX() 
    {
        _collectParticle.Play();
        // Play Audio
    } 
}
