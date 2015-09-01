using UnityEngine; 
using Assets.Scripts.game.sfx;

public class testingGhosting : MonoBehaviour
{

    private GhostingContainer _gcContainer;

    private SpriteRenderer _srSpriteRenderer;
    public float EffectDuration; 
    public float Speed;
    public float SpawnRate;
    public int MaxGhosts;
   // public int Spacing;
    private Vector3 _originalpos; 
    void Start()
    {
        _gcContainer = GetComponent<GhostingContainer>();
        _srSpriteRenderer = GetComponent<SpriteRenderer>();
        _originalpos = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            _gcContainer.Init(MaxGhosts, SpawnRate, _srSpriteRenderer, EffectDuration );
        if (Input.GetKeyDown(KeyCode.F2))
            _gcContainer.StopEffect();
        if (Input.GetKeyDown(KeyCode.F3))
            transform.position = _originalpos;
        transform.position += (Speed * Time.deltaTime) * Vector3.right;
    }
}
