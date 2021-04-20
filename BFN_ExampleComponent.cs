// src* = https://github.com/andrew-raphael-lukasik/BFN
using UnityEngine;
public class BFN_ExampleComponent : MonoBehaviour
{
	[SerializeField] BFN _myBigNumber = new BFN{ number=11.2423085208 , exponent=21 };
	void Start ()
	{
		Debug.Log($"my number is: {_myBigNumber}");

		var a = BFN.Trillion;
		var b = new BFN( 1234 , 6 );
		var adivc = a / b;
		Debug.Log($"{BFN.Trillion} divided by {b} is {adivc}");
	}
}
