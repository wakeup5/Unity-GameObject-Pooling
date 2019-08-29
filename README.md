## Install
1. Window > Package Manager
2. Click '+' button > Add package from git URL...
3. Copy and paste `https://github.com/wakeup5/Unity-GameObject-Pooling.git`
4. Done.

## Use
```
// for GameObject
public GameObject original;
private IPool<GameObject> p;


private void Awake()
{
  p = Pool.OfGameObject(original);
  p.ActivateOne(new Vector3(0, 0, 0), Quaternion.identity);
}
```
```
// for UI Element.
public Text original;
public RectTransform parent
private IPool<Text> p;


private void Awake()
{
  p = Pool.OfBehaviour(original, 5, parent); // need UI's RectTransform parent.
  p.ActivateOne();
}
```
```
// for Poolable Component.
public class Bullet : Poolable<Bullet>
{
  private void OnHit()
  {
    Pool.ActivateOne()
  }
}

public Bullet original;
private IPool<Bullet> p;


private void Awake()
{
  p = Pool.OfPoolable(original);
  var instance = p.ActivateOne(new Vector3(0, 0, 0), Quaternion.identity);
  var original = instance.Original; // Original prefab of instance
  var pool = instance.Pool // Pool of instance
}
```
