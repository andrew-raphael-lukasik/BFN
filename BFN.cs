// src* = https://github.com/andrew-raphael-lukasik/BFN
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public struct BFN
{
	#region fields


	public double coefficient;
	public long exponent;


	#endregion
	#region constructors


	public BFN ( double coefficient , long exponent )
	{
		this.coefficient = coefficient;
		this.exponent = exponent;
	}


	#endregion
	#region properties


	public BFN compressed { get{ var copy = this; copy.Compress(); return copy; } }


	#endregion
	#region operators


	public static explicit operator double ( BFN value ) => value.coefficient * Math.Pow(10d,value.exponent);
	public static explicit operator BFN ( double value ) => new BFN(value,0);

	public static bool operator == ( BFN a , BFN b ) => Equals( a:a , b:b );
	public static bool operator != ( BFN a , BFN b ) => !Equals( a:a , b:b );

	public static bool operator > ( BFN a , BFN b )
	{
		a.Compress();
		b.Compress();
		ToCommonExponent( ref a , ref b );
		return a.coefficient > b.coefficient;
	}

	public static bool operator < ( BFN a , BFN b )
	{
		a.Compress();
		b.Compress();
		ToCommonExponent( ref a , ref b );
		return a.coefficient < b.coefficient;
	}

	public static BFN operator + ( BFN a , BFN b )
	{
		ToCommonExponent( ref a , ref b );
		BFN result = new BFN{ coefficient = a.coefficient + b.coefficient , exponent = a.exponent }.compressed;
		// Debug.Log($"\t(+) {a.ToStringPrecise()} + {b.ToStringPrecise()}\t= {result.ToStringPrecise()}");
		return result;
	}
	public static BFN operator + ( BFN a , double b ) => ( a + new BFN(b,0).compressed ).compressed;
	public static BFN operator - ( BFN a , BFN b )
	{
		ToCommonExponent( ref a , ref b );
		return new BFN{ coefficient = a.coefficient - b.coefficient , exponent = a.exponent }.compressed;
	}
	public static BFN operator - ( BFN a , double b ) => ( a - new BFN(b,0).compressed ).compressed;
	public static BFN operator * ( BFN a , BFN b ) => new BFN{ coefficient = a.coefficient * b.coefficient , exponent = a.exponent + b.exponent }.compressed;
	public static BFN operator * ( BFN a , double b ) => ( a * new BFN(b,0).compressed ).compressed;
	public static BFN operator / ( BFN a , BFN b ) => new BFN( a.coefficient/b.coefficient , a.exponent-b.exponent ).compressed;
	public static BFN operator / ( BFN a , double b ) => ( a / new BFN(b,0).compressed ).compressed;


	#endregion
	#region method overrides


	public override string ToString () => $"{coefficient} {GetExponentName()}";
	
	public override bool Equals ( object obj ) => obj!=null ? Equals( a:this , b:(BFN)obj ) : false;

	/// <summary> I think this exponent.GetHashCode() will fully ignore numerical proximity, which is not fine is some cases, but fine for others. </summary>
	/// <remarks> Modify this method to fit your specific use case. </remarks>
	public override int GetHashCode ()
	{
		var copy = this.compressed;
		unchecked
		{
			int hash = 486187739;
			hash = hash * 16777619 + copy.coefficient.GetHashCode();
			hash = hash * 16777619 + copy.exponent.GetHashCode();
			return hash;
		}
	}


	#endregion
	#region public methods


	public string ToStringPrecise () => $"{coefficient.ToString("G17")}e{exponent.ToString("G17")}";

	public static bool Equals ( BFN a , BFN b )
	{
		a.Compress();
		b.Compress();
		bool result = a.exponent==b.exponent && Approx(a.coefficient,b.coefficient);
		// Debug.Log($"\t(Equals) {a.exponent}=={b.exponent} && Approx( {a.coefficient} , {b.coefficient} )\t = {result}");
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool Approx ( double a , double b )
	{
		const double precision = 1E-14;
		// Debug.Log($"{nameof(_Approx)}: {Math.Abs(a-b)} <= {precision}");
		return Math.Abs(a-b) <= precision;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ToCommonExponent ( ref BFN a , ref BFN b )
	{
		if( a.exponent==b.exponent ) return;
		else if( a.exponent > b.exponent )
		{
			b.coefficient /= Math.Pow( 10d , a.exponent - b.exponent );
			b.exponent = a.exponent;
		}
		else// if( b.exponent > a.exponent )
		{
			// Debug.Log($"{a.coefficient} /= {Math.Pow( 10d , b.exponent - a.exponent )}\t{a.exponent} and {b.exponent}");
			a.coefficient /= Math.Pow( 10d , b.exponent - a.exponent );
			a.exponent = b.exponent;
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	/// <summary> Attempts to pack stored value to prevent coefficient component from losing it's accuracy. </summary>
	/// <remarks> Prefers **engineering notation**. </remarks>
	public void Compress ()
	{
		long e2 = 0;
		double frac = 0;
		int sign = Math.Sign( coefficient );
		if( sign!=0 )
		{
			double log10 = Math.Log10( Math.Abs(coefficient) ) * (double) sign;
			long log10floor = (long) Math.Floor(log10);
			frac = coefficient / Math.Pow( 10d , log10floor );
			e2 = log10floor + exponent;
		}
		long e2mod3 = e2 % 3;

		exponent = e2 - e2mod3;
		coefficient = frac * Math.Pow( 10d , e2mod3 );
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


	// note: these names will vary from country to country so change these to suit your needs
	static readonly Dictionary<long,string> _exponent_names = new Dictionary<long,string>{
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
			var coefficientProperty = property.FindPropertyRelative( nameof(BFN.coefficient) );
			var exponentProperty = property.FindPropertyRelative( nameof(BFN.exponent) );

			EditorGUI.BeginProperty( position , label , property );
			{
				position = EditorGUI.PrefixLabel( position , GUIUtility.GetControlID(FocusType.Passive) , label );
				if( GUI.Button( new Rect( position.x , position.y , 21 , position.height ) , new GUIContent("▼", "Compress numerical value. For example: \"1000\" will become \"1E3\".") ) )
				{
					bigNumber.Compress();
					coefficientProperty.doubleValue = bigNumber.coefficient;
					exponentProperty.longValue = bigNumber.exponent;
					property.serializedObject.ApplyModifiedProperties();
				}
				position.x += 21;
				position.width -= 21;
				float width3 = position.width * 1f/3f;
				EditorGUI.PropertyField( new Rect( position.x , position.y , width3 , position.height ) , coefficientProperty , GUIContent.none );
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
