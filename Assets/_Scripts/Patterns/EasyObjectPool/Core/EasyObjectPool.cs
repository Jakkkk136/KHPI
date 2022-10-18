/* 
 * Unless otherwise licensed, this file cannot be copied or redistributed in any format without the explicit consent of the author.
 * (c) Preet Kamal Singh Minhas, http://marchingbytes.com
 * contact@marchingbytes.com
 */

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.Patterns.Core {
	
	[System.Serializable]
	public class PoolInfo {
		public ePoolName poolId;
		public PooledObject prefab;
		public int poolSize;
		public bool fixedSize;
		public Transform parentForPoolParent;
	}

	sealed class Pool {
		public Queue<PooledObject> availableObjQueue = new Queue<PooledObject>();

		private Vector3 poolObjectsPos = new Vector3(100f, 100f, 100f);
		private bool fixedSize;
		private PooledObject poolObjectPrefab;
		private int poolSize;
		private ePoolName poolName;
		private Transform parentForParent;
		Transform parent;

		public Pool(ePoolName poolName, PooledObject poolObjectPrefab, int initialCount, bool fixedSize, Transform poolParentForParent)
		{
			this.poolName = poolName;
			this.poolObjectPrefab = poolObjectPrefab;
			this.poolSize = initialCount;
			this.fixedSize = fixedSize;
			this.parentForParent = poolParentForParent;
			
			//populate the pool
			for(int index = 0; index < initialCount; index++) {
				AddObjectToPool(NewObjectInstance());
			}
		}

		//o(1)
		private void AddObjectToPool(PooledObject po) {
			//add to pool

			po.transform.parent = parent;
			
			po.gameObject.SetActive(false);
			po.transform.position = poolObjectsPos;
			availableObjQueue.Enqueue(po);
			po.isPooled = true;
		}
		
		private PooledObject NewObjectInstance() {
			PooledObject pooledObject = Object.Instantiate(poolObjectPrefab);

			pooledObject.poolName = poolName;
			
			//create parent
			if (parent == null)
			{
				parent = new GameObject(poolName + "_Pool").transform;
				parent.parent = parentForParent != null ? parentForParent : null;
			}

			pooledObject.transform.parent = parent;
			
			return pooledObject;
		}

		//o(1)
		public PooledObject NextAvailableObject(Vector3 position, Quaternion rotation) {
			PooledObject po = null;
			if(availableObjQueue.Count > 0)
			{
				po = availableObjQueue.Dequeue();
			} 
			else if(fixedSize == false) 
			{
				//increment size var, this is for info purpose only
				poolSize++;
				po = NewObjectInstance();
			} 
			else return null;
			
			PooledObject result = null;

			po.isPooled = false;
			result = po;
			
			result.transform.position = position;
			result.transform.rotation = rotation == Quaternion.identity ? result.transform.rotation : rotation;
			
			result.gameObject.SetActive(true);
			
			return result;
		} 
		
		//o(1)
		public void ReturnObjectToPool(PooledObject po) {
			
			if(poolName.Equals(po.poolName)) 
			{
				/* we could have used availableObjStack.Contains(po) to check if this object is in pool.
				 * While that would have been more robust, it would have made this method O(n) 
				 */
				if(po.isPooled) 
				{
					Debug.LogWarning(po.gameObject.name + " is already in pool. Why are you trying to return it again? Check usage.");	
				} 
				else 
				{
					AddObjectToPool(po);
				}
			}
		}
	}

	/// <summary>
	/// Easy object pool.
	/// </summary>
	public sealed class EasyObjectPool : MonoBehaviour
	{
		public static EasyObjectPool instance;
		[Header("Editing Pool Info value at runtime has no effect")]
		public PoolInfo[] poolInfo;
		
		//mapping of pool name vs list
		private Dictionary<ePoolName, Pool> poolDictionary  = new Dictionary<ePoolName, Pool>();
		
		// Use this for initialization
		void Awake () {
			//set instance
			instance = this;
			//check for duplicate names
			CheckForDuplicatePoolNames();
			
		}

		private void Start()
		{
			//create pools
			CreatePools();
		}

		private void CheckForDuplicatePoolNames() 
		{
			for (int index = 0; index < poolInfo.Length; index++) {
				ePoolName poolId = poolInfo[index].poolId;
				if(poolId == ePoolName.none) {
					Debug.LogError(string.Format("Pool {0} does not have a name!",index));
				}
				for (int internalIndex = index + 1; internalIndex < poolInfo.Length; internalIndex++) {
					if(poolId.Equals(poolInfo[internalIndex].poolId)) {
						Debug.LogError(string.Format("Pool {0} & {1} have the same name. Assign different names.", index, internalIndex));
					}
				}
			}
		}

		private void CreatePools() {
			foreach (PoolInfo currentPoolInfo in poolInfo)
			{
				Pool pool;
				
				pool = new Pool(currentPoolInfo.poolId, currentPoolInfo.prefab, 
					currentPoolInfo.poolSize, currentPoolInfo.fixedSize, currentPoolInfo.parentForPoolParent);
				
				//add to mapping dict
				poolDictionary[currentPoolInfo.poolId] = pool;
			}
		}


		/* Returns an available object from the pool 
		OR 
		null in case the pool does not have any object available & can grow size is false.
		*/
		public PooledObject GetObjectFromPool<T>(ePoolName poolName, Vector3 position, Quaternion rotation) where T : PooledObject
		{
			if(poolDictionary.ContainsKey(poolName)) 
			{
				return poolDictionary[poolName].NextAvailableObject(position, rotation);
			} 
			
			Debug.LogError("Invalid pool name specified: " + poolName);
			return null;
		}

		public List<GameObject> GetAllObjectsInPool(ePoolName poolName)
		{
			if(poolDictionary.ContainsKey(poolName))
			{
				return poolDictionary[poolName].availableObjQueue.Select(p => p.gameObject).ToList();
			}
			
			return null;
		}

		public void ReturnObjectToPool(PooledObject poolObject, bool objectPassed)
		{
			PooledObject po = poolObject;
			
			if(poolDictionary.ContainsKey(po.poolName)) {
				Pool pool = poolDictionary[po.poolName];
				pool.ReturnObjectToPool(po);
			}
			else
			{
				Debug.LogError("Object is not presented in pool dictionary:" + po.name);
			}
		}
	}
}
