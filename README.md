# Big F_____ Number
Structure to help you store, represent and operate on very big numbers in Unity.

```diff
! This repo is a work in progress, expect bugs etc.
```

---
### Define "big numbers"
It's a `Double` with auxiliary `Int64` exponent.
```c#
BFN.MaxValue = Double.MaxValue * Math.Pow( 10d , Int64.MaxValue );// +1.7976931348623157 E+9223372036854776115
BFN.MinValue = Double.MinValue * Math.Pow( 10d , Int64.MinValue );// -1.7976931348623157 E-9223372036854775500
```
These numbers are large enough to make most software refuse to calculate them and just print "infinity" or "invalid input".

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

---

* "F_____" stands for "double-precision floating-point"
