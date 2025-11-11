using MessagePipe;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Controller.Base
{
    public class BaseController : MonoBehaviour
    {
        [Inject] private readonly IPublisher<RequestBaseMaxHpDTO> _requestMaxHp;
        [Inject] private readonly IPublisher<RecoverHpDTO> _recoverHpPublisher;
        
        [SerializeField] private Button _recoverButton;
        
        private void Awake() => _recoverButton.onClick.AddListener(OnRecoverButtonClicked);
        private void Start() => _requestMaxHp.Publish(new RequestBaseMaxHpDTO());
        private void OnDestroy() => _recoverButton.onClick.RemoveListener(OnRecoverButtonClicked);

        public void OnRecoverButtonClicked()
        {
            _recoverHpPublisher.Publish(new RecoverHpDTO());
        }
    }
}