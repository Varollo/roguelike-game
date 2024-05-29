using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class Tile : ITile, IDisposable
    {
        private readonly Dictionary<Type, ITileComponent> _components;

        private Vector2Int _position;
        private bool _disposedValue;

        #region Constructors
        public Tile(params ITileComponent[] components) : this(Vector2Int.zero, components)
        {
        }

        public Tile(Vector2Int tilePosition) : this(tilePosition, null)
        {
        }

        public Tile(Vector2Int tilePosition, params ITileComponent[] components)
        {
            var compsToAdd = components != null && components.Length != 0
                ? components.Concat(SetupComponents())
                : SetupComponents();

            _components = new();

            if (compsToAdd != null)
                foreach (var component in compsToAdd)
                    _components.TryAdd(component.GetType(), component);

            SetPosition(tilePosition);
        } 
        #endregion

        #region Position
        public Vector2Int Position
        {
            get => _position;
            protected set => SetPosition(value);
        }

        public virtual void SetPosition(Vector2Int value)
        {
            TileGridManager.SetTile(this, value.x, value.y);
            _position = value;
        }

        public virtual void OnPositionMove(Vector2Int newPos)
        {
            Position = newPos;
        }
        #endregion

        #region Components
        protected virtual IEnumerable<ITileComponent> SetupComponents()
        {
            return null;
        }

        protected virtual IEnumerable<ITileComponent> CombineComponents(IEnumerable<ITileComponent> mainComponents, params ITileComponent[] extraComponents)
        {
            if (extraComponents == null || extraComponents.Length == 0)
                return mainComponents;            

            else if (mainComponents == null)
                return extraComponents;

            return mainComponents.Concat(extraComponents);
        }

        public TComponent GetComponent<TComponent>() where TComponent : ITileComponent
        {
            if (_components.TryGetValue(typeof(TComponent), out ITileComponent component))
                return (TComponent)component;

            TComponent firstMatch = _components.Values.OfType<TComponent>().FirstOrDefault();
            if (firstMatch != null)
                return firstMatch;

            return default;
            //throw new ArgumentException($"No component with type '{typeof(TComponent)}' found on object of type '{GetType().FullName}'", nameof(TComponent));
        }
        #endregion

        #region Empty Virtual Methods
        public virtual void OnDestroy() { }
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
