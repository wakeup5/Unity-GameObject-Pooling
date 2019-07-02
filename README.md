## Use
```
public GameObject original;
private IPool<GameObject> p;


private void Awake()
{
  p = Pool.OfGameObject(original);
  p.ActivateOne(new Vector3(0, 0, 0), Quaternion.identity);
}
```
```
public Text original;
public RectTransform parent
private IPool<Text> p;


private void Awake()
{
  p = Pool.OfBehaviour(original, 5, parent);
  p.ActivateOne();
}
```
```
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
