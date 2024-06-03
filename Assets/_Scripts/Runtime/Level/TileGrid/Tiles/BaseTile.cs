using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class BaseTile : ITile, IDisposable
    {
        public delegate void TilePositionDelegate(Vector2Int position);
        public event TilePositionDelegate TilePositionChangeEvent;

        private readonly Dictionary<Type, ITileComponent> _componentDictionary;

        private Vector2Int _position;
        private bool _disposedValue;

        #region Constructors
        public BaseTile(params ITileComponent[] components) : this(Vector2Int.zero, components)
        {
        }

        public BaseTile(Vector2Int tilePosition) : this(tilePosition, null)
        {
        }

        public BaseTile(Vector2Int tilePosition, params ITileComponent[] components)
        {
            IEnumerable<ITileComponent> defaultComps = GetDefaultComponents();

            var compsToAdd = components != null && components.Length != 0
                ? components.Concat(defaultComps)
                : defaultComps;

            _componentDictionary = new();

            if (compsToAdd != null)
                foreach (var component in compsToAdd)
                    _componentDictionary.TryAdd(component.GetType(), component);

            Position = tilePosition;
        }
        #endregion

        public bool Enabled { get; protected set; } = true;

        #region Position
        public Vector2Int Position
        {
            get => _position;
            protected set
            {
                TileGridManager.SetTile(this, value.x, value.y);
                _position = value;

                OnSetPosition(value);
            }
        }


        public void OnPositionMove(Vector2Int newPos)
        {
            Position = newPos;

            if (!Enabled) 
                return;

            foreach (ITileComponent component in this)
                component.OnTilePositionMove(newPos);

            TilePositionChangeEvent?.Invoke(newPos);
        }

        protected virtual void OnSetPosition(Vector2Int position) { }
        #endregion

        #region Components
        /// <summary>
        /// Default Components to add on new <see cref="BaseTile"/> instances.
        /// </summary>
        protected virtual List<ITileComponent> GetDefaultComponents() => new();

        public TComponent GetComponent<TComponent>() where TComponent : ITileComponent
        {
            if (_componentDictionary.TryGetValue(typeof(TComponent), out ITileComponent component))
                return (TComponent)component;

            TComponent firstMatch = _componentDictionary.Values.OfType<TComponent>().FirstOrDefault();
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
        IEnumerator IEnumerable.GetEnumerator() => _componentDictionary.Values.GetEnumerator();
        public IEnumerator<ITileComponent> GetEnumerator() => _componentDictionary.Values.GetEnumerator();
        #endregion

        #region IDisposable Stuff
        private void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                    TileGridManager.DestroyTile(this);

                _componentDictionary.Clear();
                _disposedValue = true;
            }
        }

        ~BaseTile()
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
