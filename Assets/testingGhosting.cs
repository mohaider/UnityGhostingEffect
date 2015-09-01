using UnityEngine; 
using Assets.Scripts.game.sfx;


/// <summary>
/// This script should be attached to a target object with a sprite renderer. 
/// </summary>
public class testingGhosting : MonoBehaviour
{
	#region ghosting effec fields
	public Color GhostTintColor;
    private GhostingContainer _gcContainer; 
    private SpriteRenderer _srSpriteRenderer;
    public float EffectDuration;  //how long does the effect last for? 
    public float SpawnRate; //How fast do new ghosts spawn?
    public int MaxGhosts;//the maximum number of ghosts in any given time. 
	private Animator _animator;
	#endregion 

	public float Speed; //speed of the player object. 
    private Vector3 _originalpos; //the original position of the player
    void Start()
    {
        _gcContainer = GetComponent<GhostingContainer>();
        _srSpriteRenderer = GetComponent<SpriteRenderer>();
        _originalpos = transform.position;
		_animator = GetComponent<Animator> ();
		_animator.SetBool ("Ground", true);
		_animator.SetFloat ("Speed",0.2f);
    }


    // Update is called once per frame
    void Update()
    {
		 
        if (Input.GetKeyDown(KeyCode.F1))
            _gcContainer.Init(MaxGhosts, SpawnRate, _srSpriteRenderer, EffectDuration,GhostTintColor ); //initiate the ghosting routine
        if (Input.GetKeyDown(KeyCode.F2))
            _gcContainer.StopEffect();
        if (Input.GetKeyDown(KeyCode.F3))
            transform.position = _originalpos;
        transform.position += (Speed * Time.deltaTime) * Vector3.right;
    }
}
