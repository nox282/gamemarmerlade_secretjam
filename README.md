# GameJam Guidelines - Using Unity and Wwise
1. [Git](#git)
2. [Coding (C#)](#coding-c)
3. [Unity](#unity)
4. [Unity UI](#unity-ui)
5. [Wwise](#wwise)
6. [Wwise Integration](#wwise-integration)
## Git
- One feature branch -> One Unity scene
- Three `develop` branches :
	- `develop` : developers
	- `develop_art` : graphic designers
	- `develop_wwise` : sound designers
- Make sure your Editor project settings are set to `force text` for Asset Serialization
- Use  [this](https://pastebin.com/8pkAUhfW) `.gitignore` file
- When Merging feature branch to any `develop` branch, use this procedure : 
	1. Branch `ours` into `ours_copy` 
		- `git checkout -b ours_copy`
	2. Merge `theirs` into `ours_copy` 
		- `git merge theirs`
	3. Open and build the Unity project, make sure everything is as excepted
	4. Merge `ours_copy` in `ours`, using `git merge branch_name -X theirs` if necessary
		- `git checkout ours && git merge ours_copy`
		- `git checkout ours && git merge ours_copy -X theirs`
	5. Add new prefabs into desired scene where applicable
- You can use `git merge --abort` if you need to step back from a merge
- Push a branch on remote only if necessary using `git push --set-upstream origin branch_name`
## Coding (C#)
- Every class shall extend `MonoBehaviour`, no exceptions.
- Exposing class attributes to public is totally acceptable.
- When declaring a lot of attributes, sort them in sections highlighted by comment lines
- Document what public attributes shall be initialized by what GameObjects in Unity editor
- Where applicable, Coroutines that use `WaitForSeconds(float)` should use this pattern : 
```
timer++;
yield return new WaitForSeconds(1.0f);
```
- Avoid fancy Polymorphism
- Implement the singleton pattern as follow when only one instance of an object is needed (for instance, the GameManager class) :
```
public class GameManager : Monobehaviour {
	public static GameManager instance = null;
	
	public void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
	      		Destroy(gameObject);
	}
}
```
- When refering to a singleton, use `GameObject.FindObjectOfType<T>()` in `public void Start()` to retrieve the instance, try to not rely on editor initialization (drag and dropping a GameObject reference in a public attribute) where possible
- Use `CamelCase` for **class**, **attribute**, and **methods**
- Use `camelCase` for **local variable**
- Log wisely with comments
	- Bad Example : `Debug.Log(waitingRoom.Count);`
	- Good Exemple : `Debug.Log(waitingRoom.Count + "patient(s) currently waiting in the Waiting Room.");`
- Clean your debugging logs !
## Unity
- Every element of a scene should be in a prefab, even if there is only one instance of it
- When developing a scene element, only the **top level** GameObject should require attributes initialization from other GameObjects in the scene
-  All the child GameObjects should be retrieved using the `GetComponentInChildren<T>()` method
- **Parent** GameObjects use the **Children** methods, not the other way around
- Work on a prefab when possible
- If you have to edit a scene component directly in a scene, always keep an updated prefab version of your top level scene components
	1. Drag and drop the scene GameObject in the prefab folder
	2. Delete old one
	3. Rename new one as desired
- Organize the prefab folder into subcategories folder
- When creating a GameObject, don't forget to reset its transform component
## Unity UI
- Use empty GameObjects as containers for your UI elements
- Use a `Canvas Scaler` component in the top level UI GameObject with `UI Scale Mode` set to `Scale With Screen Size` 
- Use anchors to place the container GameObject, then adjust the contained GameObjects using the center anchor
- Try to refrain from using editor initialization when setting public attributes on UI Controllers script. Use the the `GameObject.FindObject` methods instead
## Wwise
- When creating a Event that loops, make sure to create its corresponding StopEvent
## Wwise Unity Integration
- When implementing repetitive and timer dependant sounds (like footsteps) do so in a separate script file
- Use the reference of a `AK.Wwise.Event` to post event to the Sound engine
