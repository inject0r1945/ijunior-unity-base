using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.UI
{
    public class VampirismView : MonoBehaviour
    {
        [SerializeField, Required] private Image _background;
        [SerializeField, Required] private Image _lock;
        [SerializeField] private Color _availableColor = Color.green;
        [SerializeField] private Color _activatedColor = Color.yellow;
        [SerializeField] private Color _recoverColor = Color.red;

        private Coroutine _unlockCoroutine;

        public void SetAvailableView()
        {
            Unlock();
            ChangeBackgroundColor(_availableColor);
        }
        public void SetActivatedView()
        {
            Unlock();
            ChangeBackgroundColor(_activatedColor);
        }

        public void SetLockViewBy(float lockTime)
        {
            Lock();
            ChangeBackgroundColor(_recoverColor);
            _unlockCoroutine = StartCoroutine(UnlockCoroutine(lockTime));
        }

        private IEnumerator UnlockCoroutine(float lockTime)
        {
            float timer = lockTime;

            while (timer > 0)
            {
                _lock.fillAmount = timer / lockTime;
                timer -= Time.deltaTime;

                yield return null;
            }

            Unlock();
        }

        private void Unlock()
        {
            StopUnlockCoroutine();

            _lock.fillAmount = 0;
            _lock.enabled = false;
        }

        private void ChangeBackgroundColor(Color color)
        {
            _background.color = color;
        }

        private void Lock()
        {
            StopUnlockCoroutine();

            _lock.fillAmount = 1;
            _lock.enabled = true;
        }

        private void StopUnlockCoroutine()
        {
            if (_unlockCoroutine != null)
                StopCoroutine(_unlockCoroutine);
        }
    }
}
