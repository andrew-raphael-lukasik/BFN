# Big F_____ Number
Structure to help store and represent very big numbers in Unity

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

Where ▼ button will help you simplify numbers for shorter notation. For example `( num=5000 , e=0 )` will become `( num=5 , e=3 )`.
