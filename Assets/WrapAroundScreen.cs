using UnityEngine; 
/// <summary>
/// code inspired from the algorithm presented in http://gamedevelopment.tutsplus.com/articles/create-an-asteroids-like-screen-wrapping-effect-with-unity--gamedev-15055
/// </summary>
public class WrapAroundScreen : MonoBehaviour
{

    private float _offset = 0.5f;
    private Renderer[] _renderers;
	// Use this for initialization
	void Start ()
	{
	    _renderers = GetComponentsInChildren<Renderer>();
	}
	
	// Update is called once per frame
	void Update ()
	{
     
	    bool isVisible = IsVisible();
	    bool wrappingX=false, wrappingY =false;
        if (isVisible)
        {
             
            wrappingX = wrappingY = false;
            return;
        }
	    if (wrappingX && wrappingY)
	    {
           
	        return;
	    }
        Vector2 pos = transform.position;

        Vector2 viewPortPos = Camera.main.WorldToViewportPoint(pos);
        if (!wrappingX && (viewPortPos.x > 1 || viewPortPos.x < 0))
        {
           
            pos.x = -pos.x;

            wrappingX = true;
        }
        if (!wrappingY && (viewPortPos.y > 1 || viewPortPos.y < 0))
        {
            pos.y = -pos.y;
         
            wrappingY = true;
        }
        transform.position = pos;
	    //left edge of the screen?

	}

    bool IsVisible()
    {
        foreach (Renderer  r in _renderers)
        {
            if (r.isVisible)
            {
                return true;
            }
        }
        return false;
    }
}
