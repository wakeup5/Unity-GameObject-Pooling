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

  bool isActive = true;
  p.One(new Vector3(0, 0, 0), Quaternion.identity, isActive);
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
  p.One();
}
```
```
// for Poolable Component.
public class Bullet : Poolable<Bullet>
{
  private void OnHit()
  {
    Pool.One()
  }
}

public Bullet original;
private IPool<Bullet> p;


private void Awake()
{
  p = Pool.OfPoolable(original);
  var instance = p.One(new Vector3(0, 0, 0), Quaternion.identity);
  var original = instance.Original; // Original prefab of instance
  var pool = instance.Pool // Pool of instance
}
```
