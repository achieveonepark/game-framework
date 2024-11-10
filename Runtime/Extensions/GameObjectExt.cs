using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameFramework
{

	public static class GameObjectExt
	{

		public static void SetActiveIfNotNull(this GameObject self, bool isActive)
		{
			if (self == null) return;
			self.SetActive(isActive);
		}


		public static T GetOrAddComponent<T>(this GameObject self) where T : Component
		{
			return self.GetComponent<T>() ?? self.AddComponent<T>();
		}


		public static Component GetOrAddComponent(this GameObject self, Type type)
		{
			return self.GetComponent(type) ?? self.AddComponent(type);
		}


		public static GameObject[] GetChildren(this GameObject self, bool includeInactive = false)
		{
			return self
					.GetComponentsInChildren<Transform>(includeInactive)
					.Where(c => c != self.transform)
					.Select(c => c.gameObject)
					.ToArray()
				;
		}


		public static T GetComponentInChildrenWithoutSelf<T>(this GameObject self) where T : Component
		{
			return self.GetComponentsInChildrenWithoutSelf<T>().FirstOrDefault();
		}


		public static T[] GetComponentsInChildrenWithoutSelf<T>(this GameObject self) where T : Component
		{
			return self.GetComponentsInChildren<T>().Where(c => self != c.gameObject).ToArray();
		}


		public static T[] GetComponentsInChildrenWithoutSelf<T>(this GameObject self, bool includeInactive)
			where T : Component
		{
			return self.GetComponentsInChildren<T>(includeInactive).Where(c => self != c.gameObject).ToArray();
		}


		public static void RemoveComponent<T>(this GameObject self) where T : Component
		{
			GameObject.Destroy(self.GetComponent<T>());
		}


		public static void RemoveComponentImmediate<T>(this GameObject self) where T : Component
		{
			GameObject.DestroyImmediate(self.GetComponent<T>());
		}


		public static void RemoveComponents<T>(this GameObject self) where T : Component
		{
			foreach (var component in self.GetComponents<T>())
			{
				GameObject.Destroy(component);
			}
		}


		public static bool HasComponent<T>(this GameObject self) where T : Component
		{
			return self.GetComponent<T>() != null;
		}


		public static GameObject FindDeep(this GameObject self, string name, bool includeInactive = false)
		{
			var children = self.GetComponentsInChildren<Transform>(includeInactive);
			foreach (var transform in children)
			{
				if (transform.name == name)
				{
					return transform.gameObject;
				}
			}

			return null;
		}


		public static void ResetPosition(this GameObject self)
		{
			self.transform.position = Vector3.zero;
		}


		public static Vector3 GetPosition(this GameObject self)
		{
			return self.transform.position;
		}


		public static float GetPositionX(this GameObject self)
		{
			return self.transform.position.x;
		}


		public static float GetPositionY(this GameObject self)
		{
			return self.transform.position.y;
		}


		public static float GetPositionZ(this GameObject self)
		{
			return self.transform.position.z;
		}


		public static void SetPositionX(this GameObject self, float x)
		{
			self.transform.position = new Vector3
			(
				x,
				self.transform.position.y,
				self.transform.position.z
			);
		}


		public static void SetPositionY(this GameObject self, float y)
		{
			self.transform.position = new Vector3
			(
				self.transform.position.x,
				y,
				self.transform.position.z
			);
		}


		public static void SetPositionZ(this GameObject self, float z)
		{
			self.transform.position = new Vector3
			(
				self.transform.position.x,
				self.transform.position.y,
				z
			);
		}


		public static void SetPosition(this GameObject self, Vector3 v)
		{
			self.transform.position = v;
		}


		public static void SetPosition(this GameObject self, Vector2 v)
		{
			self.transform.position = new Vector3
			(
				v.x,
				v.y,
				self.transform.position.z
			);
		}


		public static void SetPosition(this GameObject self, float x, float y)
		{
			self.transform.position = new Vector3
			(
				x,
				y,
				self.transform.position.z
			);
		}


		public static void SetPosition(this GameObject self, float x, float y, float z)
		{
			self.transform.position = new Vector3
			(
				x,
				y,
				z
			);
		}


		public static void AddPositionX(this GameObject self, float x)
		{
			self.transform.position = new Vector3
			(
				self.transform.position.x + x,
				self.transform.position.y,
				self.transform.position.z
			);
		}


		public static void AddPositionY(this GameObject self, float y)
		{
			self.transform.position = new Vector3
			(
				self.transform.position.x,
				self.transform.position.y + y,
				self.transform.position.z
			);
		}


		public static void AddPositionZ(this GameObject self, float z)
		{
			self.transform.position = new Vector3
			(
				self.transform.position.x,
				self.transform.position.y,
				self.transform.position.z + z
			);
		}


		public static void AddPosition(this GameObject self, Vector3 v)
		{
			self.transform.position += v;
		}


		public static void AddPosition(this GameObject self, Vector2 v)
		{
			self.transform.position = new Vector3
			(
				self.transform.position.x + v.x,
				self.transform.position.y + v.y,
				self.transform.position.z
			);
		}


		public static void AddPosition(this GameObject self, float x, float y)
		{
			self.transform.position = new Vector3
			(
				self.transform.position.x + x,
				self.transform.position.y + y,
				self.transform.position.z
			);
		}


		public static void AddPosition(this GameObject self, float x, float y, float z)
		{
			self.transform.position = new Vector3
			(
				self.transform.position.x + x,
				self.transform.position.y + y,
				self.transform.position.z + z
			);
		}


		public static void ResetLocalPosition(this GameObject self)
		{
			self.transform.localPosition = Vector3.zero;
		}


		public static Vector3 GetLocalPosition(this GameObject self)
		{
			return self.transform.localPosition;
		}


		public static float GetLocalPositionX(this GameObject self)
		{
			return self.transform.localPosition.x;
		}


		public static float GetLocalPositionY(this GameObject self)
		{
			return self.transform.localPosition.y;
		}


		public static float GetLocalPositionZ(this GameObject self)
		{
			return self.transform.localPosition.z;
		}


		public static void SetLocalPositionX(this GameObject self, float x)
		{
			self.transform.localPosition = new Vector3
			(
				x,
				self.transform.localPosition.y,
				self.transform.localPosition.z
			);
		}


		public static void SetLocalPositionY(this GameObject self, float y)
		{
			self.transform.localPosition = new Vector3
			(
				self.transform.localPosition.x,
				y,
				self.transform.localPosition.z
			);
		}


		public static void SetLocalPositionZ(this GameObject self, float z)
		{
			self.transform.localPosition = new Vector3
			(
				self.transform.localPosition.x,
				self.transform.localPosition.y,
				z
			);
		}


		public static void SetLocalPosition(this GameObject self, Vector3 v)
		{
			self.transform.localPosition = v;
		}


		public static void SetLocalPosition(this GameObject self, Vector2 v)
		{
			self.transform.localPosition = new Vector3
			(
				v.x,
				v.y,
				self.transform.localPosition.z
			);
		}


		public static void SetLocalPosition(this GameObject self, float x, float y)
		{
			self.transform.localPosition = new Vector3
			(
				x,
				y,
				self.transform.localPosition.z
			);
		}


		public static void SetLocalPosition(this GameObject self, float x, float y, float z)
		{
			self.transform.localPosition = new Vector3
			(
				x,
				y,
				z
			);
		}


		public static void AddLocalPositionX(this GameObject self, float x)
		{
			self.transform.localPosition = new Vector3
			(
				self.transform.localPosition.x + x,
				self.transform.localPosition.y,
				self.transform.localPosition.z
			);
		}


		public static void AddLocalPositionY(this GameObject self, float y)
		{
			self.transform.localPosition = new Vector3
			(
				self.transform.localPosition.x,
				self.transform.localPosition.y + y,
				self.transform.localPosition.z
			);
		}


		public static void AddLocalPositionZ(this GameObject self, float z)
		{
			self.transform.localPosition = new Vector3
			(
				self.transform.localPosition.x,
				self.transform.localPosition.y,
				self.transform.localPosition.z + z
			);
		}


		public static void AddLocalPosition(this GameObject self, Vector3 v)
		{
			self.transform.localPosition += v;
		}


		public static void AddLocalPosition(this GameObject self, Vector2 v)
		{
			self.transform.localPosition = new Vector3
			(
				self.transform.localPosition.x + v.x,
				self.transform.localPosition.y + v.y,
				self.transform.localPosition.z
			);
		}


		public static void AddLocalPosition(this GameObject self, float x, float y)
		{
			self.transform.localPosition = new Vector3
			(
				self.transform.localPosition.x + x,
				self.transform.localPosition.y + y,
				self.transform.localPosition.z
			);
		}


		public static void AddLocalPosition(this GameObject self, float x, float y, float z)
		{
			self.transform.localPosition = new Vector3
			(
				self.transform.localPosition.x + x,
				self.transform.localPosition.y + y,
				self.transform.localPosition.z + z
			);
		}


		public static void ResetLocalScale(this GameObject self)
		{
			self.transform.localScale = Vector3.one;
		}


		public static Vector3 GetLocalScale(this GameObject self)
		{
			return self.transform.localScale;
		}


		public static float GetLocalScaleX(this GameObject self)
		{
			return self.transform.localScale.x;
		}


		public static float GetLocalScaleY(this GameObject self)
		{
			return self.transform.localScale.y;
		}


		public static float GetLocalScaleZ(this GameObject self)
		{
			return self.transform.localScale.z;
		}


		public static void SetLocalScaleX(this GameObject self, float x)
		{
			self.transform.localScale = new Vector3
			(
				x,
				self.transform.localScale.y,
				self.transform.localScale.z
			);
		}


		public static void SetLocalScaleY(this GameObject self, float y)
		{
			self.transform.localScale = new Vector3
			(
				self.transform.localScale.x,
				y,
				self.transform.localScale.z
			);
		}


		public static void SetLocalScaleZ(this GameObject self, float z)
		{
			self.transform.localScale = new Vector3
			(
				self.transform.localScale.x,
				self.transform.localScale.y,
				z
			);
		}


		public static void SetLocalScale(this GameObject self, Vector3 v)
		{
			self.transform.localScale = v;
		}


		public static void SetLocalScale(this GameObject self, Vector2 v)
		{
			self.transform.localScale = new Vector3
			(
				v.x,
				v.y,
				self.transform.localScale.z
			);
		}


		public static void SetLocalScale(this GameObject self, float x, float y)
		{
			self.transform.localScale = new Vector3
			(
				x,
				y,
				self.transform.localScale.z
			);
		}


		public static void SetLocalScale(this GameObject self, float x, float y, float z)
		{
			self.transform.localScale = new Vector3
			(
				x,
				y,
				z
			);
		}


		public static void AddLocalScaleX(this GameObject self, float x)
		{
			self.transform.localScale = new Vector3
			(
				self.transform.localScale.x + x,
				self.transform.localScale.y,
				self.transform.localScale.z
			);
		}


		public static void AddLocalScaleY(this GameObject self, float y)
		{
			self.transform.localScale = new Vector3
			(
				self.transform.localScale.x,
				self.transform.localScale.y + y,
				self.transform.localScale.z
			);
		}


		public static void AddLocalScaleZ(this GameObject self, float z)
		{
			self.transform.localScale = new Vector3
			(
				self.transform.localScale.x,
				self.transform.localScale.y,
				self.transform.localScale.z + z
			);
		}


		public static void AddLocalScale(this GameObject self, Vector3 v)
		{
			self.transform.localScale += v;
		}


		public static void AddLocalScale(this GameObject self, Vector2 v)
		{
			self.transform.localScale = new Vector3
			(
				self.transform.localScale.x + v.x,
				self.transform.localScale.y + v.y,
				self.transform.localScale.z
			);
		}


		public static void AddLocalScale(this GameObject self, float x, float y)
		{
			self.transform.localScale = new Vector3
			(
				self.transform.localScale.x + x,
				self.transform.localScale.y + y,
				self.transform.localScale.z
			);
		}


		public static void AddLocalScale(this GameObject self, float x, float y, float z)
		{
			self.transform.localScale = new Vector3
			(
				self.transform.localScale.x + x,
				self.transform.localScale.y + y,
				self.transform.localScale.z + z
			);
		}


		public static void ResetEulerAngles(this GameObject self)
		{
			self.transform.eulerAngles = Vector3.zero;
		}


		public static Vector3 GetEulerAngles(this GameObject self)
		{
			return self.transform.eulerAngles;
		}


		public static float GetEulerAngleX(this GameObject self)
		{
			return self.transform.eulerAngles.x;
		}


		public static float GetEulerAngleY(this GameObject self)
		{
			return self.transform.eulerAngles.y;
		}


		public static float GetEulerAngleZ(this GameObject self)
		{
			return self.transform.eulerAngles.z;
		}


		public static void SetEulerAngles(this GameObject self, Vector3 v)
		{
			self.transform.eulerAngles = v;
		}


		public static void SetEulerAngleX(this GameObject self, float x)
		{
			self.transform.eulerAngles = new Vector3
			(
				x,
				self.transform.eulerAngles.y,
				self.transform.eulerAngles.z
			);
		}


		public static void SetEulerAngleY(this GameObject self, float y)
		{
			self.transform.eulerAngles = new Vector3
			(
				self.transform.eulerAngles.x,
				y,
				self.transform.eulerAngles.z
			);
		}


		public static void SetEulerAngleZ(this GameObject self, float z)
		{
			self.transform.eulerAngles = new Vector3
			(
				self.transform.eulerAngles.x,
				self.transform.eulerAngles.y,
				z
			);
		}


		public static void AddEulerAngleX(this GameObject self, float x)
		{
			self.transform.Rotate(x, 0, 0, Space.World);
		}


		public static void AddEulerAngleY(this GameObject self, float y)
		{
			self.transform.Rotate(0, y, 0, Space.World);
		}


		public static void AddEulerAngleZ(this GameObject self, float z)
		{
			self.transform.Rotate(0, 0, z, Space.World);
		}


		public static void ResetLocalEulerAngles(this GameObject self)
		{
			self.transform.localEulerAngles = Vector3.zero;
		}


		public static Vector3 GetLocalEulerAngles(this GameObject self)
		{
			return self.transform.localEulerAngles;
		}


		public static float GetLocalEulerAngleX(this GameObject self)
		{
			return self.transform.localEulerAngles.x;
		}


		public static float GetLocalEulerAngleY(this GameObject self)
		{
			return self.transform.localEulerAngles.y;
		}


		public static float GetLocalEulerAngleZ(this GameObject self)
		{
			return self.transform.localEulerAngles.z;
		}


		public static void SetLocalEulerAngle(this GameObject self, Vector3 v)
		{
			self.transform.localEulerAngles = v;
		}


		public static void SetLocalEulerAngleX(this GameObject self, float x)
		{
			self.transform.localEulerAngles = new Vector3
			(
				x,
				self.transform.localEulerAngles.y,
				self.transform.localEulerAngles.z
			);
		}


		public static void SetLocalEulerAngleY(this GameObject self, float y)
		{
			self.transform.localEulerAngles = new Vector3
			(
				self.transform.localEulerAngles.x,
				y,
				self.transform.localEulerAngles.z
			);
		}


		public static void SetLocalEulerAngleZ(this GameObject self, float z)
		{
			self.transform.localEulerAngles = new Vector3
			(
				self.transform.localEulerAngles.x,
				self.transform.localEulerAngles.y,
				z
			);
		}


		public static void AddLocalEulerAngleX(this GameObject self, float x)
		{
			self.transform.Rotate(x, 0, 0, Space.Self);
		}


		public static void AddLocalEulerAngleY(this GameObject self, float y)
		{
			self.transform.Rotate(0, y, 0, Space.Self);
		}


		public static void AddLocalEulerAngleZ(this GameObject self, float z)
		{
			self.transform.Rotate(0, 0, z, Space.Self);
		}


		public static bool HasParent(this GameObject self)
		{
			return self.transform.parent != null;
		}


		public static void SetParent(this GameObject self, Component parent)
		{
			self.transform.SetParent(parent != null ? parent.transform : null);
		}


		public static void SetParent(this GameObject self, GameObject parent)
		{
			self.transform.SetParent(parent != null ? parent.transform : null);
		}


		public static void SetParent(this GameObject self, Component parent, bool worldPositionStays)
		{
			self.transform.SetParent(parent != null ? parent.transform : null, worldPositionStays);
		}


		public static void SetParent(this GameObject self, GameObject parent, bool worldPositionStays)
		{
			self.transform.SetParent(parent != null ? parent.transform : null, worldPositionStays);
		}


		public static void SafeSetParent(this GameObject self, Component parent)
		{
			SafeSetParent(self, parent.gameObject);
		}


		public static void SafeSetParent(this GameObject self, GameObject parent)
		{
			var t = self.transform;
			var localPosition = t.localPosition;
			var localRotation = t.localRotation;
			var localScale = t.localScale;
			t.parent = parent.transform;
			t.localPosition = localPosition;
			t.localRotation = localRotation;
			t.localScale = localScale;
			self.layer = parent.layer;
		}


		public static void LookAt(this GameObject self, GameObject target)
		{
			self.transform.LookAt(target.transform);
		}


		public static void LookAt(this GameObject self, Transform target)
		{
			self.transform.LookAt(target);
		}


		public static void LookAt(this GameObject self, Vector3 worldPosition)
		{
			self.transform.LookAt(worldPosition);
		}


		public static void LookAt(this GameObject self, GameObject target, Vector3 worldUp)
		{
			self.transform.LookAt(target.transform, worldUp);
		}


		public static void LookAt(this GameObject self, Transform target, Vector3 worldUp)
		{
			self.transform.LookAt(target, worldUp);
		}


		public static void LookAt(this GameObject self, Vector3 worldPosition, Vector3 worldUp)
		{
			self.transform.LookAt(worldPosition, worldUp);
		}


		public static bool HasChild(this GameObject self)
		{
			return 0 < self.transform.childCount;
		}


		public static Transform GetChild(this GameObject self, int index)
		{
			return self.transform.GetChild(index);
		}


		public static Transform GetParent(this GameObject self)
		{
			return self.transform.parent;
		}


		public static GameObject GetRoot(this GameObject self)
		{
			var root = self.transform.root;
			return root != null ? root.gameObject : null;
		}


		public static void SetLayer(this GameObject self, string layerName)
		{
			self.layer = LayerMask.NameToLayer(layerName);
		}


		public static void SetLayerRecursively(this GameObject self, int layer)
		{
			self.layer = layer;

			foreach (Transform n in self.transform)
			{
				SetLayerRecursively(n.gameObject, layer);
			}
		}


		public static void SetLayerRecursively(this GameObject self, string layerName)
		{
			self.SetLayerRecursively(LayerMask.NameToLayer(layerName));
		}


		public static float GetGlobalScaleX(this GameObject self)
		{
			var t = self.transform;
			var x = 1f;
			while (t != null)
			{
				x *= t.localScale.x;
				t = t.parent;
			}

			return x;
		}


		public static float GetGlobalScaleY(this GameObject self)
		{
			var t = self.transform;
			var y = 1f;
			while (t != null)
			{
				y *= t.localScale.y;
				t = t.parent;
			}

			return y;
		}


		public static float GetGlobalScaleZ(this GameObject self)
		{
			var t = self.transform;
			var z = 1f;
			while (t != null)
			{
				z *= t.localScale.z;
				t = t.parent;
			}

			return z;
		}


		public static Vector3 GetGlobalScale(this GameObject self)
		{
			var t = self.transform;
			var scale = Vector3.one;
			while (t != null)
			{
				scale.x *= t.localScale.x;
				scale.y *= t.localScale.y;
				scale.z *= t.localScale.z;
				t = t.parent;
			}

			return scale;
		}


		public static bool IsNullOrInactive(this GameObject self)
		{
			return self == null || !self.activeInHierarchy || !self.activeSelf;
		}


		public static bool IsNotNullOrInactive(this GameObject self)
		{
			return !self.IsNullOrInactive();
		}


		public static void DontDestroyOnLoad(this GameObject self)
		{
			GameObject.DontDestroyOnLoad(self);
		}


		public static GameObject[] GetAllParent(this GameObject self)
		{
			var result = new List<GameObject>();
			for (var parent = self.transform.parent; parent != null; parent = parent.parent)
			{
				result.Add(parent.gameObject);
			}

			return result.ToArray();
		}


		public static string GetRootPath(this GameObject self)
		{
			var path = self.name;
			var parent = self.transform.parent;

			while (parent != null)
			{
				path = parent.name + "/" + path;
				parent = parent.parent;
			}

			return path;
		}


		public static T GetComponentInterface<T>(this GameObject self) where T : class
		{
			foreach (var n in self.GetComponents<Component>())
			{
				var component = n as T;
				if (component != null)
				{
					return component;
				}
			}

			return null;
		}


		public static T[] GetComponentInterfaces<T>(this GameObject self) where T : class
		{
			var result = new List<T>();
			foreach (var n in self.GetComponents<Component>())
			{
				var component = n as T;
				if (component != null)
				{
					result.Add(component);
				}
			}

			return result.ToArray();
		}


		public static T GetComponentInterfaceInChildren<T>(this GameObject self) where T : class
		{
			return self.GetComponentInterfaceInChildren<T>(false);
		}


		public static T GetComponentInterfaceInChildren<T>(this GameObject self, bool includeInactive) where T : class
		{
			foreach (var n in self.GetComponentsInChildren<Component>(includeInactive))
			{
				var component = n as T;
				if (component != null)
				{
					return component;
				}
			}

			return null;
		}


		public static T[] GetComponentInterfacesInChildren<T>(this GameObject self) where T : class
		{
			return self.GetComponentInterfacesInChildren<T>(false);
		}


		public static T[] GetComponentInterfacesInChildren<T>(this GameObject self, bool includeInactive)
			where T : class
		{
			var result = new List<T>();
			foreach (var n in self.GetComponentsInChildren<Component>(includeInactive))
			{
				var component = n as T;
				if (component != null)
				{
					result.Add(component);
				}
			}

			return result.ToArray();
		}


		public static bool HasMissingScript(this GameObject self)
		{
			return self
					.GetComponents<Component>()
					.Any(c => c == null)
				;
		}


		public static void ToggleActive(this GameObject self, bool isActive)
		{
			self.SetActive(!isActive);
			self.SetActive(isActive);
		}
	}
}