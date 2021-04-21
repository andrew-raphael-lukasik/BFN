# Big F_____ Number
Structure to help you store, represent and operate on very big numbers in Unity.

```diff
! This repo is a work in progress, expect bugs etc.
```

---
### Define "big numbers"
```c#
BFN.MaxValue = double.MaxValue * Math.Pow( 10d , int.MaxValue );
BFN.MinValue = double.MinValue * Math.Pow( 10d , int.MinValue );
```
TL;DR: It's a `double` with auxiliary `int` exponent. These numbers are large enough to make most software refuse to calculate them and just print "Infinity".

---
### Can I see it in the `Inspector` window serialized there?
Yes:
```csharp
using UnityEngine;
public class BFN_ExampleComponent : MonoBehaviour
{
	[SerializeField] BFN _myBigNumber = new BFN{ number=11.2423085208 , exponent=21 };
	void Start ()
	{
		Debug.Log($"my number is: {_myBigNumber}");
	}
}

```
Will produce this Inpector view:

![sdas](https://i.imgur.com/ulyUl2E.jpg)

Where ▼ button will help you simplify (compress) numbers for shorter notation. For example `( num=5000 , e=0 )` will become `( num=5 , e=3 )`.
