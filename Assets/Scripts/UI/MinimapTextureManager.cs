using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MinimapTextureManager : MonoBehaviour
    {
        public RawImage rawImage;
        public RenderTexture renderTexture;
        public Texture disabled;

        private void Start()
        {
            if (rawImage == null) rawImage = GetComponent<RawImage>();
        }

        public void Refresh(string sceneName)
        {
            if (!sceneName.ToLower().Trim().Contains("island")) rawImage.texture = disabled;
            else rawImage.texture = renderTexture;
        }
    }
}