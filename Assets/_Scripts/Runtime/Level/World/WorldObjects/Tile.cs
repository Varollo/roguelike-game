using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class Tile : ITile, IDisposable
    {
        private readonly Dictionary<Type, ITileComponent> _components;
                
        private Vector2Int _position;
        private bool _disposedValue;

        public Tile(Vector2Int tilePosition = default)
        {
            var comps = SetupComponents();
            _components = comps != null ? new(comps) : new();

            SetPosition(tilePosition);
        }

        public Vector2Int Position 
        { 
            get => _position; 
            protected set => SetPosition(value); 
        }

        public virtual void OnDestroy() { }

        #region Components
        protected virtual IEnumerable<KeyValuePair<Type, ITileComponent>> SetupComponents()
        {
            return null;
        } 

        public TComponent GetComponent<TComponent>() where TComponent : ITileComponent
        {
            if (_components.TryGetValue(typeof(TComponent), out ITileComponent component))
                return (TComponent)component;
            throw new ArgumentException($"No component with type '{typeof(TComponent)}' found on object of type '{GetType().FullName}'", nameof(TComponent));
        }
        #endregion

        #region Position
        protected virtual void SetPosition(Vector2Int value)
        {
            TileGridManager.SetTile(this, value.x, value.y);
            _position = value;
        }

        public virtual void OnPositionMove(Vector2Int newPos)
        {
            Position = newPos;
            Debug.Log(newPos);
        } 
        #endregion

        #region IEnumerable Stuff
        IEnumerator IEnumerable.GetEnumerator() => _components.Values.GetEnumerator();
        public IEnumerator<ITileComponent> GetEnumerator() => _components.Values.GetEnumerator();
        #endregion

        #region IDisposable Stuff
        private void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                    TileGridManager.DestroyTile(this);

                _components.Clear();
                _disposedValue = true;
            }
        }

        ~Tile()
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
