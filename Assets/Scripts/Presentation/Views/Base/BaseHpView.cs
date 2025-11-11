using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.Views.Base
{
    public class BaseHpView : MonoBehaviour, IBaseHpView
    {
        [SerializeField] private Image _hpBar;
        [SerializeField] private Transform _container;
        [SerializeField] private Color _withoutHp;
        [SerializeField] private Color _withHp;

        private List<Image> _hpBars = new List<Image>();

        public void InitHpBar(int maxHp)
        {
            for (int i = 0; i < maxHp; i++)
            {
                var bar = Instantiate(_hpBar, _container);
                bar.color = _withHp;
                
                _hpBars.Add(bar);
            }
        }

        public void AddHp()
        {
            var bar = _hpBars.Find(x => x.color == _withoutHp);
            bar.color = _withHp;
        }

        public void ErrorAddHp(string msg)
        {
            Debug.LogError(msg);
        }

        public void RemoveHp(int count)
        {
            int removed = 0;
            for (int i = _hpBars.Count - 1; i >= 0 && removed < count; i--)
            {
                if (_hpBars[i].color == _withHp)
                {
                    _hpBars[i].color = _withoutHp;
                    removed++;
                }
            }
        }
    }
}