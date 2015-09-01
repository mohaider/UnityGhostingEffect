
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

            StartCoroutine(StartDissapearing(false));
        }

		public void InitSkeletonGhostShader(float dissapearTimer, float startingAlpha,int sortingOrder, Sprite sprite, int sortingId,
            Transform referencedTransform, Vector3 offset, Color desiredColor)
        {
            startingAlpha = startingAlpha;
            /*    Color color = SpriteRenderer.color;
                color.a = _startingAlpha;
                SpriteRenderer.color = color;*/
            _desiredColor = desiredColor;
            GhostMaterial.SetColor("_Color", desiredColor);

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

            StartCoroutine(StartDissapearing(true));
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
            if (!changeColor)
            {
                GhostMaterial.shader = Shader.Find("Sprites/Default");
            }
            else
            {
                GhostMaterial.shader = Shader.Find("Spine/SkeletonGhost");
            }
            while (!finishedLerping)
            {
                float timeSinceLerpStart = Time.time - startLerpTime;
                float percentComplete = timeSinceLerpStart / _dissapearTimer;

                // percentComplete = percentComplete*percentComplete;
                float newAlphaValue = Mathf.Lerp(_startingAlpha, 0, percentComplete);
                color.a = newAlphaValue;
                if (percentComplete >= 1)
                {
                    finishedLerping = true;
                     
                }
                if (!changeColor)
                {

                    GhostMaterial.color = color; 

                }
                else
                {
                    GhostMaterial.SetFloat("_TextureFade", newAlphaValue);
                    GhostMaterial.color = color; 
                }
                
                yield return null;
            }

            _canBeReused = true;
            gameObject.SetActive(false);
            _hasBeenInitiated = false;
        }

    }
}
