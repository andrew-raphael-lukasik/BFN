// src* = https://github.com/andrew-raphael-lukasik/BFN
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public struct BFN
{
	#region fields


	public double number;
	public int exponent;


	#endregion
	#region constructors


	public BFN ( double number , int exponent )
	{
		this.number = number;
		this.exponent = exponent;
	}


	#endregion
	#region properties


	public BFN compressed { get{ var copy = this; copy.Compress(); return copy; } }


	#endregion
	#region operators


	public static explicit operator double ( BFN value ) => value.number * Math.Pow(10d,value.exponent);
	public static explicit operator BFN ( double value ) => new BFN(value,0);

	public static bool operator == ( BFN a , BFN b ) => a.exponent==b.exponent && _Equals(a.number,b.number,1e-1000d);
	public static bool operator != ( BFN a , BFN b ) => !(a==b);

	public static BFN operator + ( BFN a , BFN b )
	{
		ToCommonExponent( ref a , ref b );
		return new BFN{ number = a.number + b.number , exponent = a.exponent }.compressed;
	}
	public static BFN operator + ( BFN a , double b ) => ( a + new BFN(b,0).compressed ).compressed;
	public static BFN operator - ( BFN a , BFN b )
	{
		ToCommonExponent( ref a , ref b );
		return new BFN{ number = a.number - b.number , exponent = a.exponent }.compressed;
	}
	public static BFN operator - ( BFN a , double b ) => ( a - new BFN(b,0).compressed ).compressed;
	public static BFN operator * ( BFN a , BFN b ) => new BFN{ number = a.number * b.number , exponent = a.exponent * b.exponent }.compressed;
	public static BFN operator * ( BFN a , double b ) => ( a * new BFN(b,0).compressed ).compressed;
	public static BFN operator / ( BFN a , BFN b ) => new BFN( a.number/b.number , a.exponent-b.exponent ).compressed;
	public static BFN operator / ( BFN a , double b ) => ( a / new BFN(b,0).compressed ).compressed;


	#endregion
	#region method overrides


	public override string ToString () => $"{number} {GetExponentName()}";
	
	public override bool Equals ( object obj ) => obj!=null ? this==(BFN) obj : false;

	/// <summary> I think this exponent.GetHashCode() will fully ignore numerical proximity, which is not fine is some cases, but fine for others. </summary>
	/// <remarks> Modify this method to fit your specific use case. </remarks>
	public override int GetHashCode ()
	{
		unchecked
		{
			int hash = 486187739;
			hash = hash * 16777619 + number.GetHashCode();
			hash = hash * 16777619 + exponent.GetHashCode();
			return hash;
		}
	}


	#endregion
	#region private methods


	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static bool _Equals ( double a , double b , double precision ) => Math.Abs(a-b) <= precision;

	static void ToCommonExponent ( ref BFN a , ref BFN b )
	{
		if( a.exponent > b.exponent )
			b.number /= Math.Pow( 10d , a.exponent - b.exponent );
		else if( b.exponent > a.exponent )
			a.number /= Math.Pow( 10d , b.exponent - a.exponent );
	}


	#endregion
	#region public methods


	/// <summary> Attempts to pack stored value to prevent number component from losing it's accuracy. </summary>
	public void Compress ()
	{
		#if UNITY_ASSERTIONS
		Assert.IsFalse( double.IsNaN(exponent) , $"{nameof(exponent)} is NaN, on {nameof(Compress)} start" );
		Assert.IsFalse( double.IsNaN(number) , $"{nameof(number)} is NaN, on {nameof(Compress)} start" );
		#endif

		double log10 = number!=0 ? Math.Log10( number ) : 0;
		int log10floor = (int) Math.Floor(log10);
		var frac = number / Math.Pow( 10d , log10floor );
		int e2 = log10floor + exponent;
		int e2mod3 = e2 % 3;

		exponent = e2 - e2mod3;
		number = frac * Math.Pow( 10d , e2mod3 );

		#if UNITY_ASSERTIONS
		Assert.IsFalse( double.IsNaN(frac) , $"{nameof(frac)} is NaN \t( log10floor: {log10floor}, log10: {log10} )" );
		Assert.IsFalse( double.IsNaN(exponent) , $"{nameof(exponent)} is NaN, on {nameof(Compress)} end" );
		Assert.IsFalse( double.IsNaN(number) , $"{nameof(number)} is NaN, on {nameof(Compress)} end" );
		#endif
	}

	public string GetExponentName ()
	{
		_exponent_names.TryGetValue( exponent , out string result );
		return result;
	}
	public bool GetExponentName ( out string result )
	{
		if( !_exponent_names.TryGetValue( exponent , out result ) )
		{
			result = $"E{exponent}";
			return false;
		}
		return true;
	}


	#endregion
	#region lookup tables


	static readonly Dictionary<int,string> _exponent_names = new Dictionary<int,string>{
		{	-9		,	"Billionth"				} ,
		{	-6		,	"Millionth"				} ,
		{	-3		,	"Thousandth"			} ,
		{	3		,	"Thousands"				} ,
		{	6		,	"Millions"				} ,
		{	9		,	"Billions"				} ,
		{	12		,	"Trillions"				} ,
		{	15		,	"Quadrillions"			} ,
		{	18		,	"Quintillion"			} ,
		{	21		,	"Sextillions"			} ,
		{	24		,	"Septillions"			} ,
		{	27		,	"Octillions"			} ,
		{	30		,	"Nonillions"			} ,
		{	33		,	"Decillions"			} ,
		{	36		,	"Undecillions"			} ,
		{	39		,	"Duodecillions"			} ,
		{	42		,	"Tredecillions"			} ,
		{	45		,	"Quattuordecillions"	} ,
		{	48		,	"Quindecillions"		} ,
		{	51		,	"Sexdecillions"			} ,
		{	54		,	"Septendecillions"		} ,
		{	57		,	"Octodecillions"		} ,
		{	60		,	"Novemdecillions"		} ,
		{	63		,	"Vigintillions"			} ,
	};

	public static BFN Thousand => new BFN( 1 , 3 );
	public static BFN Million => new BFN( 1 , 6 );
	public static BFN Billion => new BFN( 1 , 9 );
	public static BFN Trillion => new BFN( 1 , 12 );


	#endregion
	#region property drawer


	#if UNITY_EDITOR
	[CustomPropertyDrawer(typeof(BFN))]
	public class MyPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI ( Rect position , SerializedProperty property , GUIContent label )
		{
			BFN bigNumber = (BFN) fieldInfo.GetValue(property.serializedObject.targetObject);
			var numberProperty = property.FindPropertyRelative( nameof(BFN.number) );
			var exponentProperty = property.FindPropertyRelative( nameof(BFN.exponent) );

			EditorGUI.BeginProperty( position , label , property );
			{
				position = EditorGUI.PrefixLabel( position , GUIUtility.GetControlID(FocusType.Passive) , label );
				if( GUI.Button( new Rect( position.x , position.y , 21 , position.height ) , new GUIContent("▼", "Compress numerical value. For example: \"1000\" will become \"1E3\".") ) )
				{
					bigNumber.Compress();
					numberProperty.doubleValue = bigNumber.number;
					exponentProperty.intValue = bigNumber.exponent;
					property.serializedObject.ApplyModifiedProperties();
				}
				position.x += 21;
				position.width -= 21;
				float width3 = position.width * 1f/3f;
				EditorGUI.PropertyField( new Rect( position.x , position.y , width3 , position.height ) , numberProperty , GUIContent.none );
				EditorGUI.LabelField( new Rect( position.x+width3 , position.y , width3 , position.height ) , $" [{bigNumber.GetExponentName()}]" );
				EditorGUI.LabelField( new Rect( position.x+width3*2f , position.y , 21 , position.height ) , new GUIContent("E", "Exponent. For example: \"3\" means \"1000\" (also \"1E3\").") );
				EditorGUI.PropertyField( new Rect( position.x+width3*2f+21 , position.y , width3-21 , position.height ) , exponentProperty , GUIContent.none );
			}
			EditorGUI.EndProperty();
		}
	}
	#endif


	#endregion
}
