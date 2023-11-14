using Unity.VisualScripting;
using UnityEngine;

[DisallowMultipleComponent]
public class EffectController : MonoBehaviour
{
    public bool isDestroy = false;
    private ParticleSystem particle;
    [HideInInspector]
    public EffectPooling parent;
    public SpriteRenderer SR
    {
        get
        {
            return GetComponent<SpriteRenderer>();
        }
    }

    public Animator animator
    {
        get => this.GetComponent<Animator>();
    }
    [Tooltip("infinity == 999f")]
    [SerializeField] private float isLoopDurationTime;
    public Vector3 size
    {
        get => _size;
        set
        {
            if(isDrawModeTiled)
            {
                if (SR != null)
                {
                    SR.drawMode = SpriteDrawMode.Tiled;
                    _size = value;
                    SR.size = _size;
                }
            }
            else
            {
                _size = value;
                this.transform.localScale = _size;
            }
        }
    }
    private Vector3 _size;
    /// <summary>
    /// 세팅 여부 (세팅이 되지않은상태에서 OnDisable()이 호출되는 경우 방지
    /// </summary>
    [HideInInspector] public bool isInit;
    public bool isDrawModeTiled = false;

    public void Awake()
    {
        particle = this.GetComponent<ParticleSystem>();
        parent = this.GetComponentInParent<EffectPooling>();
        if (particle != null)
        {
            foreach (var childparticles in particle.GetComponentsInChildren<ParticleSystemRenderer>())
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
        Init();
    }

    public void Init()
    {        
        if (isLoopDurationTime > 0.0f && isLoopDurationTime != 999f)
        {
            Invoke("FinishAnim", isLoopDurationTime);
        }
    }
    private void OnDisable()
    {
        if (animator != null)
            return;

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
