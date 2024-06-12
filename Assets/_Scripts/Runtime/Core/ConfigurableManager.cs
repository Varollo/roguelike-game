using System;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public abstract class ConfigurableManager<TSettingsSO> : IManager, IDisposable where TSettingsSO : ScriptableObject
    {
        private bool _isDisposed;

        private TSettingsSO _settingsSO;

        public abstract string SettingsResourcePath { get; }

        protected TSettingsSO GetSettings()
        {
            if (!_settingsSO)
                _settingsSO = LoadSettings(SettingsResourcePath);

            return _settingsSO;
        }

        protected virtual TSettingsSO LoadSettings(string resourcePath)
        {
            var settings = Resources.Load<TSettingsSO>(resourcePath);
            return settings ? settings : ScriptableObject.CreateInstance<TSettingsSO>();
        }

        #region Manager/Messager Callbacks
        public virtual void OnInit() { }
        public virtual void OnDestroy() { } 
        #endregion

        #region IDisposible Methods
        protected virtual void OnDispose() { }
        protected virtual void FreeResources()
        {            
            _settingsSO = null;
        }

        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                    OnDispose();

                FreeResources();

                _isDisposed = true;
            }
        }

        ~ConfigurableManager()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
