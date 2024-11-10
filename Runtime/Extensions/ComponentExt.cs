using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameFramework
{
	public static class ComponentExt
	{
		public static void SetActiveIfNotNull(this Component self, bool isActive)
		{
			if (self == null) return;
			self.SetActive(isActive);
		}


		public static bool HasBeenDestroyed(this Component self)
		{
			return self == null || self.gameObject == null;
		}


		public static T GetOrAddComponent<T>(this Component self) where T : Component
		{
			return self.GetComponent<T>() ?? self.gameObject.AddComponent<T>();
		}


		public static T AddComponent<T>(this Component self) where T : Component
		{
			return self.gameObject.AddComponent<T>();
		}


		public static GameObject[] GetChildren(this Component self, bool includeInactive = false)
		{
			return self
					.GetComponentsInChildren<Transform>(includeInactive)
					.Where(c => c != self.transform)
					.Select(c => c.gameObject)
					.ToArray()
				;
		}


		public static T[] GetComponentsInChildrenWithoutSelf<T>(this Component self) where T : Component
		{
			return self.GetComponentsInChildren<T>()
					.Where(c => self.gameObject != c.gameObject)
					.ToArray()
				;
		}


		public static void RemoveComponent<T>(this Component self) where T : Component
		{
			GameObject.Destroy(self.GetComponent<T>());
		}


		public static void RemoveComponents<T>(this Component self) where T : Component
		{
			foreach (Component component in self.GetComponents<T>())
			{
				GameObject.Destroy(component);
			}
		}


		public static void RemoveComponentImmediate<T>(this Component self) where T : Component
		{
			GameObject.DestroyImmediate(self.GetComponent<T>());
		}


		public static void RemoveComponentsImmediate<T>(this Component self) where T : Component
		{
			foreach (Component component in self.GetComponents<T>())
			{
				GameObject.DestroyImmediate(component);
			}
		}


		public static bool HasComponent<T>(this Component self) where T : Component
		{
			return self.GetComponent<T>() != null;
		}


		public static void SetActive(this Component self, bool value) 
		{
			self.gameObject.SetActive(value);
		}


		public static Transform Find(this Component self, string name)
		{
			return self.transform.Find(name);
		}


		public static GameObject FindGameObject(this Component self, string name)
		{
			var result = self.transform.Find(name);
			return result != null ? result.gameObject : null;
		}


		public static GameObject FindDeep(this Component self, string name, bool includeInactive = false)
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


		public static void ResetPosition(this Component self)
		{
			self.transform.position = Vector3.zero;
		}


		public static Vector3 GetPosition(this Component self)
		{
			return self.transform.position;
		}


		public static float GetPositionX(this Component self)
		{
			return self.transform.position.x;
		}


		public static float GetPositionY(this Component self)
		{
			return self.transform.position.y;
		}


		public static float GetPositionZ(this Component self)
		{
			return self.transform.position.z;
		}


		public static void SetPositionX(this Component self, float x)
		{
			self.transform.position = new Vector3
			(
				x,
				self.transform.position.y,
				self.transform.position.z
			);
		}


		public static void SetPositionY(this Component self, float y)
		{
			self.transform.position = new Vector3
			(
				self.transform.position.x,
				y,
				self.transform.position.z
			);
		}


		public static void SetPositionZ(this Component self, float z)
		{
			self.transform.position = new Vector3
			(
				self.transform.position.x,
				self.transform.position.y,
				z
			);
		}


		public static void SetPosition(this Component self, Vector3 v)
		{
			self.transform.position = v;
		}


		public static void SetPosition(this Component self, Vector2 v)
		{
			self.transform.position = new Vector3
			(
				v.x,
				v.y,
				self.transform.position.z
			);
		}


		public static void SetPosition(this Component self, float x, float y)
		{
			self.transform.position = new Vector3
			(
				x,
				y,
				self.transform.position.z
			);
		}


		public static void SetPosition(this Component self, float x, float y, float z)
		{
			self.transform.position = new Vector3
			(
				x,
				y,
				z
			);
		}


		public static void AddPositionX(this Component self, float x)
		{
			self.transform.position = new Vector3
			(
				self.transform.position.x + x,
				self.transform.position.y,
				self.transform.position.z
			);
		}


		public static void AddPositionY(this Component self, float y)
		{
			self.transform.position = new Vector3
			(
				self.transform.position.x,
				self.transform.position.y + y,
				self.transform.position.z
			);
		}


		public static void AddPositionZ(this Component self, float z)
		{
			self.transform.position = new Vector3
			(
				self.transform.position.x,
				self.transform.position.y,
				self.transform.position.z + z
			);
		}


		public static void AddPosition(this Component self, Vector3 v)
		{
			self.transform.position += v;
		}


		public static void AddPosition(this Component self, Vector2 v)
		{
			self.transform.position = new Vector3
			(
				self.transform.position.x + v.x,
				self.transform.position.y + v.y,
				self.transform.position.z
			);
		}


		public static void AddPosition(this Component self, float x, float y)
		{
			self.transform.position = new Vector3
			(
				self.transform.position.x + x,
				self.transform.position.y + y,
				self.transform.position.z
			);
		}


		public static void AddPosition(this Component self, float x, float y, float z)
		{
			self.transform.position = new Vector3
			(
				self.transform.position.x + x,
				self.transform.position.y + y,
				self.transform.position.z + z
			);
		}


		public static void ResetLocalPosition(this Component self)
		{
			self.transform.localPosition = Vector3.zero;
		}


		public static Vector3 GetLocalPosition(this Component self)
		{
			return self.transform.localPosition;
		}


		public static float GetLocalPositionX(this Component self)
		{
			return self.transform.localPosition.x;
		}


		public static float GetLocalPositionY(this Component self)
		{
			return self.transform.localPosition.y;
		}


		public static float GetLocalPositionZ(this Component self)
		{
			return self.transform.localPosition.z;
		}


		public static void SetLocalPositionX(this Component self, float x)
		{
			self.transform.localPosition = new Vector3
			(
				x,
				self.transform.localPosition.y,
				self.transform.localPosition.z
			);
		}


		public static void SetLocalPositionXIfNotNull(this Component self, float x)
		{
			if (self == null) return;
			self.SetLocalPositionX(x);
		}


		public static void SetLocalPositionY(this Component self, float y)
		{
			self.transform.localPosition = new Vector3
			(
				self.transform.localPosition.x,
				y,
				self.transform.localPosition.z
			);
		}


		public static void SetLocalPositionZ(this Component self, float z)
		{
			self.transform.localPosition = new Vector3
			(
				self.transform.localPosition.x,
				self.transform.localPosition.y,
				z
			);
		}


		public static void SetLocalPosition(this Component self, Vector3 v)
		{
			self.transform.localPosition = v;
		}


		public static void SetLocalPosition(this Component self, Vector2 v)
		{
			self.transform.localPosition = new Vector3
			(
				v.x,
				v.y,
				self.transform.localPosition.z
			);
		}


		public static void SetLocalPosition(this Component self, float x, float y)
		{
			self.transform.localPosition = new Vector3
			(
				x,
				y,
				self.transform.localPosition.z
			);
		}


		public static void SetLocalPosition(this Component self, float x, float y, float z)
		{
			self.transform.localPosition = new Vector3
			(
				x,
				y,
				z
			);
		}


		public static void AddLocalPositionX(this Component self, float x)
		{
			self.transform.localPosition = new Vector3
			(
				self.transform.localPosition.x + x,
				self.transform.localPosition.y,
				self.transform.localPosition.z
			);
		}


		public static void AddLocalPositionY(this Component self, float y)
		{
			self.transform.localPosition = new Vector3
			(
				self.transform.localPosition.x,
				self.transform.localPosition.y + y,
				self.transform.localPosition.z
			);
		}


		public static void AddLocalPositionZ(this Component self, float z)
		{
			self.transform.localPosition = new Vector3
			(
				self.transform.localPosition.x,
				self.transform.localPosition.y,
				self.transform.localPosition.z + z
			);
		}


		public static void AddLocalPosition(this Component self, Vector3 v)
		{
			self.transform.localPosition += v;
		}


		public static void AddLocalPosition(this Component self, Vector2 v)
		{
			self.transform.localPosition = new Vector3
			(
				self.transform.localPosition.x + v.x,
				self.transform.localPosition.y + v.y,
				self.transform.localPosition.z
			);
		}


		public static void AddLocalPosition(this Component self, float x, float y)
		{
			self.transform.localPosition = new Vector3
			(
				self.transform.localPosition.x + x,
				self.transform.localPosition.y + y,
				self.transform.localPosition.z
			);
		}


		public static void AddLocalPosition(this Component self, float x, float y, float z)
		{
			self.transform.localPosition = new Vector3
			(
				self.transform.localPosition.x + x,
				self.transform.localPosition.y + y,
				self.transform.localPosition.z + z
			);
		}


		public static void ResetLocalScale(this Component self)
		{
			self.transform.localScale = Vector3.one;
		}


		public static Vector3 GetLocalScale(this Component self)
		{
			return self.transform.localScale;
		}


		public static float GetLocalScaleX(this Component self)
		{
			return self.transform.localScale.x;
		}


		public static float GetLocalScaleY(this Component self)
		{
			return self.transform.localScale.y;
		}


		public static float GetLocalScaleZ(this Component self)
		{
			return self.transform.localScale.z;
		}


		public static void SetLocalScaleX(this Component self, float x)
		{
			self.transform.localScale = new Vector3
			(
				x,
				self.transform.localScale.y,
				self.transform.localScale.z
			);
		}


		public static void SetLocalScaleY(this Component self, float y)
		{
			self.transform.localScale = new Vector3
			(
				self.transform.localScale.x,
				y,
				self.transform.localScale.z
			);
		}


		public static void SetLocalScaleZ(this Component self, float z)
		{
			self.transform.localScale = new Vector3
			(
				self.transform.localScale.x,
				self.transform.localScale.y,
				z
			);
		}


		public static void SetLocalScale(this Component self, Vector3 v)
		{
			self.transform.localScale = v;
		}


		public static void SetLocalScale(this Component self, Vector2 v)
		{
			self.transform.localScale = new Vector3
			(
				v.x,
				v.y,
				self.transform.localScale.z
			);
		}


		public static void SetLocalScale(this Component self, float x, float y)
		{
			self.transform.localScale = new Vector3
			(
				x,
				y,
				self.transform.localScale.z
			);
		}


		public static void SetLocalScale(this Component self, float x, float y, float z)
		{
			self.transform.localScale = new Vector3
			(
				x,
				y,
				z
			);
		}


		public static void AddLocalScaleX(this Component self, float x)
		{
			self.transform.localScale = new Vector3
			(
				self.transform.localScale.x + x,
				self.transform.localScale.y,
				self.transform.localScale.z
			);
		}


		public static void AddLocalScaleY(this Component self, float y)
		{
			self.transform.localScale = new Vector3
			(
				self.transform.localScale.x,
				self.transform.localScale.y + y,
				self.transform.localScale.z
			);
		}


		public static void AddLocalScaleZ(this Component self, float z)
		{
			self.transform.localScale = new Vector3
			(
				self.transform.localScale.x,
				self.transform.localScale.y,
				self.transform.localScale.z + z
			);
		}


		public static void AddLocalScale(this Component self, Vector3 v)
		{
			self.transform.localScale += v;
		}


		public static void AddLocalScale(this Component self, Vector2 v)
		{
			self.transform.localScale = new Vector3
			(
				self.transform.localScale.x + v.x,
				self.transform.localScale.y + v.y,
				self.transform.localScale.z
			);
		}


		public static void AddLocalScale(this Component self, float x, float y)
		{
			self.transform.localScale = new Vector3
			(
				self.transform.localScale.x + x,
				self.transform.localScale.y + y,
				self.transform.localScale.z
			);
		}


		public static void AddLocalScale(this Component self, float x, float y, float z)
		{
			self.transform.localScale = new Vector3
			(
				self.transform.localScale.x + x,
				self.transform.localScale.y + y,
				self.transform.localScale.z + z
			);
		}


		public static void ResetEulerAngles(this Component self)
		{
			self.transform.eulerAngles = Vector3.zero;
		}


		public static Vector3 GetEulerAngles(this Component self)
		{
			return self.transform.eulerAngles;
		}


		public static float GetEulerAngleX(this Component self)
		{
			return self.transform.eulerAngles.x;
		}


		public static float GetEulerAngleY(this Component self)
		{
			return self.transform.eulerAngles.y;
		}


		public static float GetEulerAngleZ(this Component self)
		{
			return self.transform.eulerAngles.z;
		}


		public static void SetEulerAngles(this Component self, Vector3 v)
		{
			self.transform.eulerAngles = v;
		}


		public static void SetEulerAngleX(this Component self, float x)
		{
			self.transform.eulerAngles = new Vector3
			(
				x,
				self.transform.eulerAngles.y,
				self.transform.eulerAngles.z
			);
		}


		public static void SetEulerAngleY(this Component self, float y)
		{
			self.transform.eulerAngles = new Vector3
			(
				self.transform.eulerAngles.x,
				y,
				self.transform.eulerAngles.z
			);
		}


		public static void SetEulerAngleZ(this Component self, float z)
		{
			self.transform.eulerAngles = new Vector3
			(
				self.transform.eulerAngles.x,
				self.transform.eulerAngles.y,
				z
			);
		}


		public static void AddEulerAngleX(this Component self, float x)
		{
			self.transform.Rotate(x, 0, 0, Space.World);
		}


		public static void AddEulerAngleY(this Component self, float y)
		{
			self.transform.Rotate(0, y, 0, Space.World);
		}


		public static void AddEulerAngleZ(this Component self, float z)
		{
			self.transform.Rotate(0, 0, z, Space.World);
		}


		public static void ResetLocalEulerAngles(this Component self)
		{
			self.transform.localEulerAngles = Vector3.zero;
		}


		public static Vector3 GetLocalEulerAngles(this Component self)
		{
			return self.transform.localEulerAngles;
		}


		public static float GetLocalEulerAngleX(this Component self)
		{
			return self.transform.localEulerAngles.x;
		}


		public static float GetLocalEulerAngleY(this Component self)
		{
			return self.transform.localEulerAngles.y;
		}


		public static float GetLocalEulerAngleZ(this Component self)
		{
			return self.transform.localEulerAngles.z;
		}


		public static void SetLocalEulerAngles(this Component self, Vector3 v)
		{
			self.transform.localEulerAngles = v;
		}


		public static void SetLocalEulerAngleX(this Component self, float x)
		{
			self.transform.localEulerAngles = new Vector3
			(
				x,
				self.transform.localEulerAngles.y,
				self.transform.localEulerAngles.z
			);
		}


		public static void SetLocalEulerAngleY(this Component self, float y)
		{
			self.transform.localEulerAngles = new Vector3
			(
				self.transform.localEulerAngles.x,
				y,
				self.transform.localEulerAngles.z
			);
		}


		public static void SetLocalEulerAngleZ(this Component self, float z)
		{
			self.transform.localEulerAngles = new Vector3
			(
				self.transform.localEulerAngles.x,
				self.transform.localEulerAngles.y,
				z
			);
		}


		public static void AddLocalEulerAngleX(this Component self, float x)
		{
			self.transform.Rotate(x, 0, 0, Space.Self);
		}


		public static void AddLocalEulerAngleY(this Component self, float y)
		{
			self.transform.Rotate(0, y, 0, Space.Self);
		}


		public static void AddLocalEulerAngleZ(this Component self, float z)
		{
			self.transform.Rotate(0, 0, z, Space.Self);
		}


		public static bool HasParent(this Component self)
		{
			return self.transform.parent != null;
		}


		public static void SetParent(this Component self, Component parent)
		{
			self.transform.SetParent(parent != null ? parent.transform : null);
		}


		public static void SetParent(this Component self, Component parent, bool worldPositionStays)
		{
			self.transform.SetParent(parent != null ? parent.transform : null, worldPositionStays);
		}


		public static void SetParent(this Component self, GameObject parent)
		{
			self.transform.SetParent(parent != null ? parent.transform : null);
		}


		public static void SetParent(this Component self, GameObject parent, bool worldPositionStays)
		{
			self.transform.SetParent(parent != null ? parent.transform : null, worldPositionStays);
		}


		public static void SafeSetParent(this Component self, Component parent)
		{
			SafeSetParent(self, parent.gameObject);
		}


		public static void SafeSetParent(this Component self, GameObject parent)
		{
			var t = self.transform;
			var localPosition = t.localPosition;
			var localRotation = t.localRotation;
			var localScale = t.localScale;
			t.parent = parent.transform;
			t.localPosition = localPosition;
			t.localRotation = localRotation;
			t.localScale = localScale;
			self.gameObject.layer = parent.layer;
		}


		public static void LookAt(this Component self, GameObject target)
		{
			self.transform.LookAt(target.transform);
		}


		public static void LookAt(this Component self, Transform target)
		{
			self.transform.LookAt(target);
		}


		public static void LookAt(this Component self, Vector3 worldPosition)
		{
			self.transform.LookAt(worldPosition);
		}


		public static void LookAt(this Component self, GameObject target, Vector3 worldUp)
		{
			self.transform.LookAt(target.transform, worldUp);
		}


		public static void LookAt(this Component self, Transform target, Vector3 worldUp)
		{
			self.transform.LookAt(target, worldUp);
		}


		public static void LookAt(this Component self, Vector3 worldPosition, Vector3 worldUp)
		{
			self.transform.LookAt(worldPosition, worldUp);
		}


		public static bool HasChild(this Component self)
		{
			return 0 < self.transform.childCount;
		}


		public static Transform GetChild(this Component self, int index)
		{
			return self.transform.GetChild(index);
		}


		public static Transform GetParent(this Component self)
		{
			return self.transform.parent;
		}


		public static GameObject GetRoot(this Component self)
		{
			var root = self.transform.root;
			return root != null ? root.gameObject : null;
		}


		public static int GetLayer(this Component self)
		{
			return self.gameObject.layer;
		}


		public static void SetLayer(this Component self, int layer)
		{
			self.gameObject.layer = layer;
		}


		public static void SetLayer(this Component self, string layerName)
		{
			self.gameObject.layer = LayerMask.NameToLayer(layerName);
		}


		public static void SetLayerRecursively(this Component self, int layer)
		{
			self.gameObject.layer = layer;

			foreach (Transform n in self.gameObject.transform)
			{
				SetLayerRecursively(n, layer);
			}
		}


		public static void SetLayerRecursively(this Component self, string layerName)
		{
			self.SetLayerRecursively(LayerMask.NameToLayer(layerName));
		}


		public static float GetGlobalScaleX(this Component self)
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


		public static float GetGlobalScaleY(this Component self)
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


		public static float GetGlobalScaleZ(this Component self)
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


		public static Vector3 GetGlobalScale(this Component self)
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


		public static T GetComponentInterface<T>(this Component self) where T : class
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


		public static T[] GetComponentInterfaces<T>(this Component self) where T : class
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


		public static T GetComponentInterfaceInChildren<T>(this Component self) where T : class
		{
			return self.GetComponentInterfaceInChildren<T>(false);
		}


		public static T GetComponentInterfaceInChildren<T>(this Component self, bool includeInactive) where T : class
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


		public static T[] GetComponentInterfacesInChildren<T>(this Component self) where T : class
		{
			return self.GetComponentInterfacesInChildren<T>(false);
		}


		public static T[] GetComponentInterfacesInChildren<T>(this Component self, bool includeInactive) where T : class
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


		public static bool HasMissingScript(this Component self)
		{
			return self
					.GetComponents<Component>()
					.Any(c => c == null)
				;
		}


		public static void ToggleActive(this Component self, bool isActive)
		{
			self.SetActive(!isActive);
			self.SetActive(isActive);
		}


		public static IEnumerable<Transform> GetAllParent(this Component self)
		{
			for (var parent = self.transform.parent; null != parent; parent = parent.parent)
			{
				yield return parent;
			}
		}


		public static string GetRootPath(this Component self)
		{
			var gameObject = self.gameObject;
			var path = gameObject.name;
			var parent = gameObject.transform.parent;

			while (parent != null)
			{
				path = parent.name + "/" + path;
				parent = parent.parent;
			}

			return path;
		}


		public static Vector3 GetGlobalPosition(this Component self)
		{
			var result = Vector3.zero;
			while (self != null)
			{
				var t = self.transform;
				result += t.localPosition;
				self = t.parent;
			}

			return result;
		}


		public static void RoundLocalPosition(this Component self)
		{
			var v = self.transform.localPosition;
			v.x = Mathf.Round(v.x);
			v.y = Mathf.Round(v.y);
			v.z = Mathf.Round(v.z);
			self.transform.localPosition = v;
		}


		public static void RoundLocalEulerAngles(this Component self)
		{
			var v = self.transform.localEulerAngles;
			v.x = Mathf.Round(v.x);
			v.y = Mathf.Round(v.y);
			v.z = Mathf.Round(v.z);
			self.transform.localEulerAngles = v;
		}


		public static void RoundLocalScale(this Component self)
		{
			var v = self.transform.localScale;
			v.x = Mathf.Round(v.x);
			v.y = Mathf.Round(v.y);
			v.z = Mathf.Round(v.z);
			self.transform.localScale = v;
		}


		public static void Round(this Component self)
		{
			self.RoundLocalPosition();
			self.RoundLocalEulerAngles();
			self.RoundLocalScale();
		}
	}
}