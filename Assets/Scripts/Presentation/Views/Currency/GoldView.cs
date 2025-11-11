using TMPro;
using UnityEngine;

namespace Presentation.Views.Currency
{
    public class GoldView : MonoBehaviour, IGoldView
    {
        [SerializeField] private TMP_Text _gold;
        
        public void SetGoldValue(int count)
        {
            _gold.text = count.ToString();
        }
    }
}