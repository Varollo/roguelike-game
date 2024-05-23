using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class WorldObject : IWorldObject, IDisposable
    {
        private readonly Dictionary<Type, IObjectComponent> _components;
        
        public delegate void WOMoveDelegate(Vector2Int endPos, Vector2Int moveDelta);
        public event WOMoveDelegate OnMove;
        
        private Vector2Int _position;
        private bool _disposedValue;

        public WorldObject(Vector2Int worldPosition = default)
        {
            var comps = SetupComponents();
            _components = comps != null ? new(comps) : new();

            SetPosition(worldPosition);
        }

        public Vector2Int Position 
        { 
            get => _position; 
            protected set => SetPosition(value); 
        }

        public virtual void OnKill() { }

        #region Components
        protected virtual IEnumerable<KeyValuePair<Type, IObjectComponent>> SetupComponents()
        {
            return null;
        } 

        public TComponent GetComponent<TComponent>() where TComponent : IObjectComponent
        {
            if (_components.TryGetValue(typeof(TComponent), out IObjectComponent component))
                return (TComponent)component;
            throw new ArgumentException($"No component with type '{typeof(TComponent)}' found on object of type '{GetType().FullName}'", nameof(TComponent));
        }
        #endregion

        #region Position
        protected virtual void SetPosition(Vector2Int value)
        {
            WorldManager.SetObjectPosition(this, value.x, value.y);
            _position = value;
        }

        public virtual void OnPositionChange(Vector2Int newPos)
        {
            Vector2Int oldPos = Position;
            Position = newPos;

            OnMove?.Invoke(newPos, newPos - oldPos);
            Debug.Log(newPos);
        } 
        #endregion

        #region IEnumerable Stuff
        IEnumerator IEnumerable.GetEnumerator() => _components.Values.GetEnumerator();
        public IEnumerator<IObjectComponent> GetEnumerator() => _components.Values.GetEnumerator();
        #endregion

        #region IDisposable Stuff
        private void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                    WorldManager.Kill(this);

                _components.Clear();
                _disposedValue = true;
            }
        }

        ~WorldObject()
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
