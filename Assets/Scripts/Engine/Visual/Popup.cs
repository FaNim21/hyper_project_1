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
        private Popup _popup;

        [Header("Wartosci")]
        public float lerpSpeed;
        public float moveYSpeedDefault;

        [Header("Debug")]
        [ReadOnly] public float moveYSpeed;
        [ReadOnly] public Vector2 movedText;
        [ReadOnly] public float timeAlive;


        public static void InitializePooling()
        {
            _pool = new ObjectPool<Popup>(() =>
            {
                return Instantiate(GameManager.Popup);
            }, popup =>
            {
                popup.moveYSpeed = popup.moveYSpeedDefault;
                popup.movedText = Vector2.zero;
                popup.timeAlive = 0f;

                popup.gameObject.SetActive(true);
            }, popup =>
            {
                popup.gameObject.SetActive(false);
            }, popup =>
            {
                Destroy(popup.gameObject);
            }, false, 50, 300);
        }

        public static void Create(Vector2 position, string amount, Color color, int fontSize = 5)
        {
            var popup = _pool.Get();
            popup.Setup(popup, position, amount, color, fontSize);
        }

        public void Setup(Popup popup, Vector2 position, string amountText, Color color, int fontSize)
        {
            if (_popup == null) _popup = popup;

            transform.position = position;
            textMesh.SetText(amountText.ToString());
            if (textMesh.color != color) textMesh.color = color;
            if (textMesh.fontSize != fontSize) textMesh.fontSize = fontSize;
        }

        private void Update()
        {
            timeAlive += Time.deltaTime;

            moveYSpeed = Mathf.Lerp(moveYSpeed, 0, lerpSpeed * Time.deltaTime);
            movedText += new Vector2(0, moveYSpeed) * Time.deltaTime;

            var animMove = new Vector2(0, 0.9f) + movedText;
            transform.position = (Vector2)transform.position + animMove * Time.deltaTime;

            if (timeAlive >= 1f)
                _pool.Release(_popup);
        }

        public static void Release(Popup popup) => _pool.Release(popup);
    }
}
