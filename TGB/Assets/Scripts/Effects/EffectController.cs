using UnityEngine;

[DisallowMultipleComponent]
public class EffectController : MonoBehaviour
{
    public bool isDestroy = false;
    private ParticleSystem particle;
    [HideInInspector]
    public EffectPooling parent;

    [Tooltip("infinity == 999f")]
    [SerializeField] private float isLoopDurationTime;
    /// <summary>
    /// 세팅 여부 (세팅이 되지않은상태에서 OnDisable()이 호출되는 경우 방지
    /// </summary>
    [HideInInspector] public bool isInit;
    public void Awake()
    {
        particle = this.GetComponent<ParticleSystem>();
        parent = this.GetComponentInParent<EffectPooling>();
        if (particle != null)
        {
            foreach(var childparticles in particle.GetComponentsInChildren<ParticleSystemRenderer>())
            {
                childparticles.sortingLayerName = "Effect";
            }
            var main = particle.main;
            if (isDestroy)
            {
                main.stopAction = ParticleSystemStopAction.Destroy;
            }
            else
            {                
                main.stopAction = ParticleSystemStopAction.Disable;
            }
        }
        if (isLoopDurationTime > 0.0f && isLoopDurationTime != 999f)
        {
            Invoke("FinishAnim", isLoopDurationTime);
        }
    }

    private void OnDisable()
    {
        FinishAnim();
    }
    public void FinishAnim()
    {
        if(isDestroy)
        {
            Destroy(this.gameObject);
        }
        else
        {
            if (!isInit)
                return;

            if (parent != null)
            {
                parent.ReturnObject(this.gameObject);
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }        
    }
}
