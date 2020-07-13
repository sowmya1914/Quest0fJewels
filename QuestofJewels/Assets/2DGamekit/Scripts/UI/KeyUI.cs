using UnityEngine;
using UnityEngine.UI;

namespace Gamekit2D
{
    public class KeyUI : MonoBehaviour
    {
        public static KeyUI Instance { get; protected set; }

        public GameObject keyIconPrefab;
        public string[] keyNames;

        protected Animator[] m_KeyIconAnimators;

        protected readonly int m_HashActivePara = Animator.StringToHash("Active");
        protected const float k_KeyIconAnchorWidth = 0.041f;

        public const int MaxJewel = 3;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            SetInitialKeyCount();
        }

        public void SetInitialKeyCount()
        {
            if (m_KeyIconAnimators != null && m_KeyIconAnimators.Length == keyNames.Length)
                return;

            m_KeyIconAnimators = new Animator[keyNames.Length];

            for (int i = 0; i < m_KeyIconAnimators.Length; i++)
            {
                GameObject healthIcon = Instantiate(keyIconPrefab);
                healthIcon.transform.SetParent(transform);
                RectTransform healthIconRect = healthIcon.transform as RectTransform;
                healthIconRect.anchoredPosition = Vector2.zero;
                healthIconRect.sizeDelta = Vector2.zero;
                healthIconRect.anchorMin -= new Vector2(k_KeyIconAnchorWidth, 0f) * i;
                healthIconRect.anchorMax -= new Vector2(k_KeyIconAnchorWidth, 0f) * i;
                m_KeyIconAnimators[i] = healthIcon.GetComponent<Animator>();
            }

            
        }

        public void ChangeKeyUI(InventoryController controller)
        {
            for (int i = 0; i < keyNames.Length; i++)
            {
                m_KeyIconAnimators[i].SetBool(m_HashActivePara, controller.HasItem(keyNames[i]));
            }
        }

        public void AddJewel(InventoryController controller)
        {
            int count = controller.GetJewelCount();
            for (int i = 0; i < MaxJewel; i++)
            {
                m_KeyIconAnimators[i].SetBool(m_HashActivePara, count>i? true : false);
            }
        }
    }
}