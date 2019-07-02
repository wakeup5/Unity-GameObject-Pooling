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
public Poolable original;
private IPool<Poolable> p;


private void Awake()
{
  p = Pool.OfPoolable(original);
  p.ActivateOne(new Vector3(0, 0, 0), Quaternion.identity);
}
```
