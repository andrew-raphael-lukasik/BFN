# Big F_____ Number
Structure to help you store, represent and operate on very big numbers in Unity. This one separates coefficient from it's exponent (and prefers engineering notation at that).

---
### Define "big numbers"
It's a `Double` with auxiliary `Int64` exponent.
```c#
BFN.MaxValue = Double.MaxValue * Math.Pow( 10d , Int64.MaxValue );// +1.7976931348623157 E+9223372036854776115
BFN.MinValue = Double.MinValue * Math.Pow( 10d , Int64.MinValue );// -1.7976931348623157 E-9223372036854775500
```
These numbers are large enough to make most software refuse to calculate them and just print "infinity" or "invalid input".

---
### Limitations
Coefficient is a `Double` so this is a lossy format. If you need lossless data type then you probably may want to go with [System.Numerics.BigInteger](https://docs.microsoft.com/en-us/dotnet/api/system.numerics.biginteger).

---
### Can I see it in the `Inspector` window serialized there?
Yes:
```csharp
using UnityEngine;
public class BFN_ExampleComponent : MonoBehaviour
{
	[SerializeField] BFN _myBigNumber = new BFN( 11.2423085208 , 21 );
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
