using MessagePipe;
using UnityEngine;
using VContainer;

namespace Controller.Currency
{
    public class GoldController : MonoBehaviour
    {
        [Inject] private readonly IPublisher<RequestCurrencyDTO> _requestCurrensy;
        
        private void Awake()
        {
            GetGoldValue();
        }
        
        private void GetGoldValue()
        {
            _requestCurrensy.Publish(new RequestCurrencyDTO());
        }
    }
}