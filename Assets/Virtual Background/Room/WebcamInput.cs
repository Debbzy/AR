using UnityEngine;
using System.Collections;

namespace NNCam
{

    public sealed class WebcamInput : MonoBehaviour
    {
        #region Editable attributes

        [SerializeField] string _deviceName = "";

        #endregion

        #region Internal objects

        WebCamTexture _webcam;
        RenderTexture _buffer;

        #endregion

        #region Public properties

        public Texture Texture => _buffer;

        #endregion

        #region MonoBehaviour implementation

        void Start()
        {
            _webcam = new WebCamTexture(_deviceName);
            _buffer = new RenderTexture(1080, 720, 0);
            Debug.Log($"System Info: {SystemInfo.deviceModel}");
            Debug.Log($"GPU: {SystemInfo.graphicsDeviceName}");
            Debug.Log($"Compute Shader Support: {SystemInfo.supportsComputeShaders}");
            WebCamDevice[] devices = WebCamTexture.devices;
            Debug.Log($"Detected cameras: {devices.Length}");
            Debug.Log($"Webcam initialized: {_webcam.width}x{_webcam.height}");
            foreach (WebCamDevice device in devices)
            {
                Debug.Log($"Camera: {device.name} (isFrontFacing: {device.isFrontFacing})");
            }
            _webcam.Play();
            StartCoroutine(WaitForWebcam());
        }
        private IEnumerator WaitForWebcam()
        {
            while (_webcam.width <= 16)
            {
                Debug.Log($"Waiting for webcam... Current size: {_webcam.width}x{_webcam.height}");
                yield return new WaitForSeconds(0.1f);
            }
            Debug.Log($"Webcam initialized: {_webcam.width}x{_webcam.height}");
            _webcam.Play();
        }

        void OnDestroy()
        {
            Destroy(_webcam);
            Destroy(_buffer);
        }

        void Update()
        {
            if (!_webcam.didUpdateThisFrame) return;
            var vflip = _webcam.videoVerticallyMirrored;
            var scale = new Vector2(1, vflip ? -1 : 1);
            var offset = new Vector2(0, vflip ? 1 : 0);
            Graphics.Blit(_webcam, _buffer, scale, offset);
        }


        #endregion
    }

} // namespace NNCam
