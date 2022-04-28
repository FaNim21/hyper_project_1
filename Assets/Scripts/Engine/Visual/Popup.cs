using System;
using UnityEngine;
using UnityEngine.Pool;
using TMPro;

namespace HyperRPG.Engine.Visual
{
    public class Popup : MonoBehaviour
    {
        private static ObjectPool<Popup> _pool;

        public TextMeshPro textMesh;
        [SerializeField] private Popup _popup;

        [Header("Wartosci")]
        public float lerpSpeed;
        public float moveYSpeedDefault;

        [Header("Debug")]
        [ReadOnly] public float moveYSpeed;
        [ReadOnly] public Vector3 movedText;
        [ReadOnly] public float timeAlive;

        [Header("On Create")]
        public static Vector3 position;
        public static string amount;
        public static Color color;
        public static Transform parent;
        public static int fontSize;


        public static void InitializePooling()
        {
            _pool = new ObjectPool<Popup>(() =>
            {
                return Instantiate(GameManager.Popup);
            }, popup =>
            {
                popup.moveYSpeed = popup.moveYSpeedDefault;
                popup.movedText = Vector3.zero;
                popup.timeAlive = 0f;

                popup.transform.position = position;
                popup.transform.SetParent(parent, false);
                popup.Setup(popup, amount, color, fontSize);
                popup.gameObject.SetActive(true);
            }, popup =>
            {
                popup.gameObject.SetActive(false);
            }, popup =>
            {
                Destroy(popup.gameObject);
            }, false, 50, 300);
        }

        public static void Create(Vector3 position, string amount, Color color, Transform parent, int fontSize = 5)
        {
            Popup.position = position;
            Popup.amount = amount;
            Popup.color = color;
            Popup.parent = parent;
            Popup.fontSize = fontSize;

            var popup = _pool.Get();
            popup.Setup(popup, amount, color, fontSize);
        }

        public void Setup(Popup popup, string amountText, Color color, int fontSize)
        {
            if (_popup == null) _popup = popup;

            textMesh.SetText(amountText.ToString());
            if (textMesh.color != color) textMesh.color = color;
            if (textMesh.fontSize != fontSize) textMesh.fontSize = fontSize;
        }

        private void Update()
        {
            timeAlive += Time.deltaTime;

            moveYSpeed = Mathf.Lerp(moveYSpeed, 0, lerpSpeed * Time.deltaTime);
            movedText += new Vector3(0, moveYSpeed) * Time.deltaTime;

            transform.localPosition = Vector3.zero + new Vector3(0, 0.5f, 0) + movedText;

            if (timeAlive >= 1f)
                _pool.Release(_popup);
        }

        public static void Release(Popup popup) => _pool.Release(popup);
    }
}
