using System.Collections.Generic;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    // consider not being static (1 world p/ scene)
    public static class WorldManager
    {
        // take a look at bidirectional maps / dictionaries.
        private static readonly Dictionary<Vector2Int, List<IWorldObject>> _pos2objMap = new();
        private static readonly Dictionary<IWorldObject, Vector2Int> _obj2posMap = new();

        #region Get Object / Position
        /// <summary>
        /// Attempts to find object at position (<paramref name="x"/>, <paramref name="y"/>)
        /// </summary>
        /// <param name="obj">found object, null if fails</param>
        /// <returns>true if object found at position, false if it's free</returns>
        public static bool TryGetObjectAt(int x, int y, out IWorldObject obj)
        {
            obj = GetObjectAt(x, y);
            return obj != null;
        }

        /// <summary>
        /// Attempts to find all objects at position (<paramref name="x"/>, <paramref name="y"/>)
        /// </summary>
        /// <param name="objs">found objects, null if fails</param>
        /// <returns>true if any object found at position, false if it's free</returns>
        public static bool TryGetAllObjectsAt(int x, int y, out List<IWorldObject> objs)
        {
            objs = GetAllObjectsAt(x, y);
            return objs != null;
        }

        /// <summary>
        /// Retrieves first managed object at position (<paramref name="x"/>, <paramref name="y"/>)
        /// </summary>
        /// <returns>if position is free returns null, otherwise returns FIRST object found.</returns>
        public static IWorldObject GetObjectAt(int x, int y)
        {
            List<IWorldObject> objs = GetAllObjectsAt(x, y);
            return objs != null && objs.Count > 0 ? objs[0] : null;
        }

        /// <summary>
        /// Retrieves all managed objects at position (<paramref name="x"/>, <paramref name="y"/>)
        /// </summary>
        /// <returns>if position is free returns null, otherwise returns all objects found.</returns>
        public static List<IWorldObject> GetAllObjectsAt(int x, int y)
        {
            if (_pos2objMap.TryGetValue(new(x, y), out var objs))
                return objs;
            return null;
        }

        /// <summary>
        /// Retrieves the registered position of given <paramref name="obj"/>. Will throw if object is not being managed.
        /// </summary>
        /// <returns>registered object position</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public static Vector2Int GetPositionOf(IWorldObject obj)
        {
            if (_obj2posMap.TryGetValue(obj, out var pos))
                return pos;
            else throw new KeyNotFoundException(
                $"Object not managed by {nameof(WorldManager)} instance. No position registered for provided object type '{obj.GetType().FullName}'."
            );
        }
        #endregion

        #region Add or Remove Object
        /// <summary>
        /// Sets an object to a designated position.
        /// </summary>
        public static void SetObjectPosition(IWorldObject obj, int x, int y)
        {
            Vector2Int newPos = new(x, y);

            // if position is the same, ignore call.
            if (_obj2posMap.TryGetValue(obj, out var oldPos))
            {
                if (oldPos == newPos)
                    return;

                RemoveObjectFromPosition(obj, oldPos);
            }

            AddObjectToPosition(obj, newPos);
            RelayPositionChange(obj, newPos);
        }

        /// <summary>
        /// Stops managing given object '<paramref name="obj"/>'.
        /// </summary>
        /// <returns>true if object was removed, false if object wasn't being managed.</returns>
        public static bool Kill(IWorldObject obj)
        {
            if (RemoveObjectFromPosition(obj, _obj2posMap[obj]) && _obj2posMap.Remove(obj))
            {
                obj.OnKill();

                foreach (var comp in obj)
                    comp.Kill();

                return true;
            }
            return false;
        }

        /// <summary>
        /// Calls the <see cref="IWorldObject.OnPositionChange(Vector2Int)"/> for all object components, then the object it self.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="pos"></param>
        private static void RelayPositionChange(IWorldObject obj, Vector2Int pos)
        {
            foreach (IObjectComponent component in obj)
                component.OnPositionChange(pos);

            obj.OnPositionChange(pos);
        }

        /// <summary> Adds object to position list or creates it. </summary>
        private static void AddObjectToPosition(IWorldObject obj, Vector2Int pos)
        {
            _obj2posMap[obj] = pos;

            if (_pos2objMap.TryGetValue(pos, out var newPosObjs))
                newPosObjs.Add(obj);
            else
                _pos2objMap.Add(pos, new() { obj });
        }

        /// <summary> Removes object from position list, then checks if list is empty and removes it. </summary>
        private static bool RemoveObjectFromPosition(IWorldObject obj, Vector2Int pos)
        {
            if (_pos2objMap.TryGetValue(pos, out var posObjs))
            {
                posObjs.Remove(obj);

                if (posObjs.Count == 0)
                    _pos2objMap.Remove(pos);

                return true;
            }

            return false;
        }
        #endregion

        #region Check for Object / Position
        /// <summary>
        /// Checks if any object is at position (<paramref name="x"/>, <paramref name="y"/>).
        /// </summary>
        /// <returns>true if at least 1 object at (<paramref name="x"/>, <paramref name="y"/>), false if none.</returns>
        public static bool AnyObjectAt(int x, int y)
        {
            List<IWorldObject> objList = _pos2objMap[new(x, y)];
            return objList != null && objList.Count > 0;
        }

        /// <summary>
        /// Checks if an object '<paramref name="obj"/>' is currently being managed.
        /// </summary>
        /// <returns>true if object is managed, false if not.</returns>
        public static bool HasObject(IWorldObject obj)
        {
            return _obj2posMap.ContainsKey(obj);
        }

        /// <summary>
        /// Casts a ray, starting from <paramref name="origin"/>, in a given <paramref name="direction"/>, up to <paramref name="maxLength"/> to find managed objects.
        /// </summary>
        /// <param name="origin">starting point [exclusive]</param>
        /// <param name="direction">direction to march</param>
        /// <param name="maxLength">max positions to check</param>
        /// <param name="hitPoint">point with object detected, if it fails defaults to <paramref name="origin"/></param>
        /// <returns>object detected, if it fails retuns null instead</returns>
        public static IWorldObject Raycast(Vector2Int origin, Vector2Int direction, int maxLength, out Vector2Int hitPoint)
        {
            hitPoint = origin;

            for (int i = 1; i <= maxLength; i++)
            {
                hitPoint = origin + direction * i;

                if (TryGetObjectAt(hitPoint.x, hitPoint.y, out var obj))
                    return obj;
            }

            return null;
        }
        #endregion
    }

    public interface IPositioner
    {
        int x { get; set; }
        int y { get; set; }

        void SetPosition(int x, int y);
        int GetX();
        int GetY();
    }
}
