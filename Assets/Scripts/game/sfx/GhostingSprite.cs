
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.game.sfx
{
    /// <summary>
    /// This class sets a sprites's alpha to zero over a period of time. 
    /// </summary>
    public class GhostingSprite : MonoBehaviour
    {
        private const string GhostMaterialPath = "Art/Materials/GhostMaterial";
        private Material _material;
        private float _dissapearTimer;
        private Sprite _sprite;
        private SpriteRenderer _renderer;
        private float _startingAlpha;
        private Vector3 _offset;
        private Vector3 _originalScaling;
        private Vector3 _originalPosition;

        private bool _hasBeenInitiated;
        private bool _canBeReused;
		private IEnumerator _startDissapearing; //reference to the routine in case it needs to be stopped
		private bool _useTint;

        public Material GhostMaterial
        {
            get
            {
                if (_material == null)
                {
                    Material m = UnityEngine.Resources.Load<Material>(GhostMaterialPath);
                    _material = new Material(m);
                    SpriteRenderer.material = _material;
                }
                return _material;
            }

        }
        private SpriteRenderer SpriteRenderer
        {
            get
            {
                if (_renderer == null)
                {
                    _renderer = GetComponent<SpriteRenderer>();
                    if (_renderer == null)
                    {
                        _renderer = gameObject.AddComponent<SpriteRenderer>();
                    }
                }
                return _renderer;
            }


        }

        public void Init(float dissapearTimer, float startingAlpha, Sprite sprite, int sortingId, int sortingOrder, Transform referencedTransform, Vector3 offset)
        {
            _startingAlpha = startingAlpha;
              Color color = SpriteRenderer.color;
                color.a = _startingAlpha;
                SpriteRenderer.color = color; 

            _dissapearTimer = dissapearTimer;
            SpriteRenderer.sortingLayerID = sortingId;
			SpriteRenderer.sortingOrder = sortingOrder;
            _sprite = sprite;
            SpriteRenderer.sprite = sprite;

            _offset = offset;
            _originalPosition = referencedTransform.position;
            transform.position = _originalPosition;
            /*_originalScaling = Vector3.one;
            _originalScaling.x = Mathf.Sign(referencedTransform.localScale.x);*/
            _originalScaling = referencedTransform.localScale;
            transform.localScale = _originalScaling;
            _hasBeenInitiated = true;

            gameObject.SetActive(true);
			_useTint = false;
			_startDissapearing = StartDissapearing (_useTint);
			StartCoroutine(_startDissapearing);
        }

		public void Init(float dissapearTimer, float startingAlpha, Sprite sprite,int sortingId ,int  sortingOrder   ,
            Transform referencedTransform, Vector3 offset, Color desiredColor)
        {
            startingAlpha = startingAlpha;
            /*    Color color = SpriteRenderer.color;
                color.a = _startingAlpha;
                SpriteRenderer.color = color;*/
            _desiredColor = desiredColor;
			_desiredColor.a = _startingAlpha;
			GhostMaterial.SetColor("_Color", _desiredColor);
            
            SpriteRenderer.color = desiredColor;
            _dissapearTimer = dissapearTimer;
            SpriteRenderer.sortingLayerID = sortingId;
			SpriteRenderer.sortingOrder = sortingOrder;
            _sprite = sprite;
            SpriteRenderer.sprite = sprite;

            _offset = offset;
            _originalPosition = referencedTransform.position;
            transform.position = _originalPosition;
          
            _originalScaling = referencedTransform.localScale;
            transform.localScale = _originalScaling;
            _hasBeenInitiated = true;

            gameObject.SetActive(true);
			_useTint = true;
			_startDissapearing = StartDissapearing (_useTint);
			StartCoroutine(_startDissapearing);
        }

        public Color _desiredColor;
        public bool CanBeReused()
        {
            return _canBeReused;
        }
        void Update()
        {
            if (_hasBeenInitiated)
            {
                transform.position = _originalPosition + _offset; //this prevents it from moving with its parent
                //GhostMaterial.SetColor("_Color", _desiredColor);
                transform.localScale = _originalScaling;
            }
        }

	
        /// <summary>
        /// From the class field, dissapear timer, slowly disspear the sprite renderer . Pass in a bool to use the provided ghost shader which 
        /// behaves 
        /// </summary>
        /// <returns></returns>
        private IEnumerator StartDissapearing(bool changeColor)
        {
            bool finishedLerping = false;
            float startLerpTime = Time.time;
            Color color = SpriteRenderer.color;
			Color blackWithZeroAlpha = Color.black;
			Color startColor = GhostMaterial.color;
			blackWithZeroAlpha.a = 0;
            if (!changeColor)
            {
                GhostMaterial.shader = Shader.Find("Sprites/Default");
            }
            else
            {
                GhostMaterial.shader = Shader.Find("Spine/SkeletonGhost");
				GhostMaterial.SetFloat("_TextureFade",0.25f);
				 
            }
            while (!finishedLerping)
            {
                float timeSinceLerpStart = Time.time - startLerpTime;
                float percentComplete = timeSinceLerpStart / _dissapearTimer; 
                if (percentComplete >= 1)
                {
                    finishedLerping = true; 
					if (!changeColor)
					{ 
						color.a = 0;
						GhostMaterial.color = color; 
					}
					else
					{  
						GhostMaterial.SetColor("_Color", blackWithZeroAlpha); 
                    }
                    
                }
                if (!changeColor)
                {
					float newAlphaValue = Mathf.Lerp(_startingAlpha, 0, percentComplete);
					color.a = newAlphaValue;
                    GhostMaterial.color = color; 

                }
                else
                { 
					Color newColor = Color.Lerp(startColor,blackWithZeroAlpha,percentComplete);
					GhostMaterial.SetColor("_Color", newColor); 
                }
                
                yield return null;
            }

            _canBeReused = true;
            gameObject.SetActive(false);
            _hasBeenInitiated = false;
        }

//		public void KillAnimationAndSpeedUpDissapearing()
//		{
//			StopCoroutine (_startDissapearing);
//			if (!_useTint)
//			{
//				Color color =  SpriteRenderer.color; 
//				color.a = 0;
//				GhostMaterial.color = color;  
//			}
//			else
//			{ 
//				Color newColor = Color.Lerp(startColor,blackWithZeroAlpha,percentComplete);
//				GhostMaterial.SetColor("_Color", newColor); 
//            }
//            
//            _dissapearTimer /= 4f;
//            _startDissapearing = StartDissapearing (_useTint);
//            StartCoroutine(_startDissapearing);
//    	}
}
}