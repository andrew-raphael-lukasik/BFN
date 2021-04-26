// src* = https://github.com/andrew-raphael-lukasik/BFN
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace BFN_Tests
{

	public class Equals
	{
		[Test] public void _default__default () => Assert.AreEqual( expected:default(BFN) , actual:default(BFN) );
		[Test] public void _1e3__1000e0 () => Assert.AreEqual( expected:new BFN(1,3) , actual:new BFN(1000,0) );
	}

	public class GetHashCode
	{
		[Test] public void _default__default () => Assert.AreEqual( expected:default(BFN).GetHashCode() , actual:default(BFN).GetHashCode() );
		[Test] public void _1e3__1000e0 () => Assert.AreEqual( expected:new BFN(1,3).GetHashCode() , actual:new BFN(1000,0).GetHashCode() );
		[Test] public void _12345e12345__12345e12345 () => Assert.AreEqual( expected:new BFN(12345,12345).GetHashCode() , actual:new BFN(12345,12345).GetHashCode() );
		[Test] public void _123e123__123e123 () => Assert.AreEqual( expected:new BFN(123,123).GetHashCode() , actual:new BFN(123,123).GetHashCode() );
		[Test] public void _n123en123__n123en123 () => Assert.AreEqual( expected:new BFN(-123,-123).GetHashCode() , actual:new BFN(-123,-123).GetHashCode() );
		[Test] public void _123en123__123en123 () => Assert.AreEqual( expected:new BFN(123,-123).GetHashCode() , actual:new BFN(123,-123).GetHashCode() );
		[Test] public void _n123e123__n123e123 () => Assert.AreEqual( expected:new BFN(-123,123).GetHashCode() , actual:new BFN(-123,123).GetHashCode() );
	}

	public class Approx
	{
		[Test] public void _0__0 () => Assert.IsTrue( BFN.Approx( 0 , 0 ) );
		[Test] public void _1__1 () => Assert.IsTrue( BFN.Approx( 1 , 1 ) );
		[Test] public void _n1__n1 () => Assert.IsTrue( BFN.Approx( -1 , -1 ) );
		[Test] public void _1__1dotEpsilon () => Assert.IsTrue( BFN.Approx( 1d , 1d+double.Epsilon ) );
		// TODO: test precision boundaries
	}

	public class ToCommonExponent
	{
		[Test] public void _1e1__2e2 ()
		{
			BFN a = new BFN( 1 , 1 ), original_a = a;
			BFN b = new BFN( 2 , 2 ), original_b = b;
			BFN.ToCommonExponent( ref a , ref b );
			Assert.AreEqual( expected:original_b , actual:b );// no change
			Assert.AreEqual( expected:new BFN( 0.1d , 2 ) , actual:a );
		}
		[Test] public void _1e15__1e2 ()
		{
			BFN a = new BFN( 1 , 15 ), original_a = a;
			BFN b = new BFN( 1 , 2 ), original_b = b;
			BFN.ToCommonExponent( ref a , ref b );
			Assert.AreEqual( expected:original_a , actual:a );// no change
			Assert.AreEqual( expected:new BFN( 1e-13 , 15 ) , actual:b );
		}
		[Test] public void _1en1__n1e1 ()
		{
			BFN a = new BFN( 1 , -1 ), original_a = a;// 1e-1	(0.1)	| 
			BFN b = new BFN( -1 , 1 ), original_b = b;// -1e1	(-10)	|
			BFN.ToCommonExponent( ref a , ref b );
			Assert.AreEqual( expected:original_b , actual:b );// no change
			Assert.AreEqual( expected:new BFN( 0.01 , 1 ) , actual:a );
		}
	}

	public class Compress
	{
		[Test] public void _positive_exponent_1 () => Assert.AreEqual( expected:new BFN(12.345678910111213141,18) , actual:new BFN(12345678910111213141,0).compressed );
		[Test] public void _positive_exponent_2 () => Assert.AreEqual( expected:new BFN(9.999999999999999999,18) , actual:new BFN(9999999999999999999,0).compressed );
		[Test] public void _negative_exponent_1 () => Assert.AreEqual( expected:new BFN(0.01,-3) , actual:new BFN(0.00001,0).compressed );
		[Test] public void _0e0 () => Assert.AreEqual( expected:new BFN(0,0) , actual:new BFN(0,0).compressed );
		[Test] public void _123e123 () => Assert.AreEqual( expected:new BFN(123,123) , actual:new BFN(123,123).compressed );
		[Test] public void _default () => Assert.AreEqual( expected:default(BFN) , actual:default(BFN).compressed );
		[Test] public void _1e0 () => Assert.AreEqual( expected:new BFN(1,0) , actual:new BFN(1,0).compressed );
	}
	
	public class addition
	{
		[Test] public void _11e0__200e0 () => _Add( expected:new BFN(210,0) , new BFN(10,0) , new BFN(200,0) );
		[Test] public void _1e1__2e2 () => _Add( expected:new BFN(210,0) , new BFN(1,1) , new BFN(2,2) );
		[Test] public void _2e2__1e1 () => _Add( expected:new BFN(210,0) , new BFN(2,2) , new BFN(1,1) );
		[Test] public void _1e4__2e3 () => _Add( expected:new BFN(12,3) , new BFN(1,4) , new BFN(2,3) );
		[Test] public void _2dot3e0__1dot2e0 () => _Add( expected:new BFN(3.5,0) , new BFN(2.3,0) , new BFN(1.2,0) );
		[Test] public void _9dot87654321e33__1dot23456789e14 () => _Add( expected:new BFN(9.87654321,33) , new BFN(9.87654321,33) , new BFN(1.23456789,14) );
		[Test] public void _3e10__4e3 () => _Add( expected:new BFN(30.000004,9) , new BFN(3,10) , new BFN(4,3) );
		[Test] public void _1e14__0e13 () => _Add( expected:new BFN(1,14) , new BFN(1,14) , new BFN(0,13) );
		[Test] public void _1e14__0en13 () => _Add( expected:new BFN(1,14) , new BFN(1,14) , new BFN(0,-13) );
		[Test] public void _n1en14__0en13 () => _Add( expected:new BFN(-1,-14).compressed , new BFN(-1,-14) , new BFN(0,-13) );
		[Test] public void _0e0__0e0 () => _Add( expected:new BFN(0,0) , new BFN(0,0) , new BFN(0,0) );
		[Test] public void _1en1__n1e1 () => _Add( expected:new BFN(-9.9,0) , new BFN(1,-1) , new BFN(-1,1) );//0.1 + -10
		[Test] public void _1e4__1 () => _Add( expected:new BFN(10.001,3) , new BFN(1,4) , 1 );
		// [Test] public void _G17 () => Debug.Log($"{0.1234567890_1234567890_1234567890_1234567890_1234567890d:G17}{double.MaxValue}");

		void _Add ( BFN expected , BFN L , BFN R )
		{
			Debug.Log($"\tTesting: {L.ToStringPrecise()} + {R.ToStringPrecise()}\texpecting:{expected.ToStringPrecise()}");
			Assert.AreEqual( expected:expected , actual: L + R );
		}
		void _Add ( BFN expected , BFN L , double R )
		{
			Debug.Log($"\tTesting: {L.ToStringPrecise()} + {R.ToString("G17")}\texpecting:{expected.ToStringPrecise()}");
			Assert.AreEqual( expected:expected , actual: L + R );
		}
	}
	
	public class subtraction
	{
		[Test] public void _1e0__1e0 () => _Subtract( expected:new BFN(0,0) , new BFN(1,0) , new BFN(1,0) );
		[Test] public void _1e0__2e0 () => _Subtract( expected:new BFN(-1,0) , new BFN(1,0) , new BFN(2,0) );
		[Test] public void _3e10__4e3 () => _Subtract( expected:new BFN(29.999996,9) , new BFN(3,10) , new BFN(4,3) );
		[Test] public void _1e100__1e70 () => _Subtract( expected:new BFN(1,100) , new BFN(1,100) , new BFN(1,70) );
		[Test] public void _1e10__0e10 () => _Subtract( expected:new BFN(1,10) , new BFN(1,10) , new BFN(0,10) );
		[Test] public void _0e0__0e0 () => _Subtract( expected:new BFN(0,0) , new BFN(0,0) , new BFN(0,0) );

		[Test] public void _1e4__1 () => _Subtract( expected:new BFN(9.999,3) , new BFN(1,4) , 1 );

		void _Subtract ( BFN expected , BFN L , BFN R )
		{
			Debug.Log($"\tTesting: {L.ToStringPrecise()} - {R.ToStringPrecise()}");
			Assert.AreEqual( expected:expected , actual: L - R );
		}
		void _Subtract ( BFN expected , BFN L , double R )
		{
			Debug.Log($"\tTesting: {L.ToStringPrecise()} - {R.ToString("G17")}");
			Assert.AreEqual( expected:expected , actual: L - R );
		}
	}
	
	public class multiplication
	{
		[Test] public void _2e1__3e1 () => _Multiply( expected:new BFN(600,0).compressed , new BFN(2,1) , new BFN(3,1) );
		[Test] public void _3e10__4e3 () => _Multiply( expected:new BFN(1.2,14).compressed , new BFN(3,10) , new BFN(4,3) );
		[Test] public void _0e0__0e0 () => _Multiply( expected:new BFN(0,0) , new BFN(0,0) , new BFN(0,0) );

		[Test] public void _1e4__1 () => _Multiply( expected:new BFN(2,4) , new BFN(1,4) , 2 );
		[Test] public void _1e4__10 () => _Multiply( expected:new BFN(1,5) , new BFN(1,4) , 10 );
		[Test] public void _1e4__10000 () => _Multiply( expected:new BFN(1,8) , new BFN(1,4) , 10000 );

		void _Multiply ( BFN expected , BFN L , BFN R )
		{
			Debug.Log($"\tTesting: {L.ToStringPrecise()} * {R.ToStringPrecise()}");
			Assert.AreEqual( expected:expected , actual: L * R );
		}
		void _Multiply ( BFN expected , BFN L , double R )
		{
			Debug.Log($"\tTesting: {L.ToStringPrecise()} * {R.ToString("G17")}");
			Assert.AreEqual( expected:expected , actual: L * R );
		}
	}
	
	public class division
	{
		[Test] public void _1e3__2e1 () => Assert.AreEqual( expected:new BFN(50,0) , actual:new BFN(1,3) / new BFN(2,1) );
		[Test] public void _3e10__4e3 () => Assert.AreEqual( expected:new BFN(7.5,6) , actual:new BFN(3,10) / new BFN(4,3) );

		[Test] public void _1e3__2 () => Assert.AreEqual( expected:new BFN(500,0) , actual:new BFN(1,3) / 2 );
		[Test] public void _1e4__2 () => Assert.AreEqual( expected:new BFN(5,3) , actual:new BFN(1,4) / 2 );

		void _Divide ( BFN expected , BFN L , BFN R )
		{
			Debug.Log($"\tTesting: {L.ToStringPrecise()} / {R.ToStringPrecise()}");
			Assert.AreEqual( expected:expected , actual: L / R );
		}
	}


	public class greater_than
	{
		[Test] public void _0e0__0e0 () => _Greater( expected:false , new BFN(0,0) , new BFN(0,0) );
		[Test] public void _1e0__1e0 () => _Greater( expected:false , new BFN(1,0) , new BFN(1,0) );
		[Test] public void _n1e0__n1e0 () => _Greater( expected:false , new BFN(-1,0) , new BFN(-1,0) );
		[Test] public void _n1e0__1e0 () => _Greater( expected:false , new BFN(-1,0) , new BFN(1,0) );
		[Test] public void _1e0__n1e0 () => _Greater( expected:true , new BFN(1,0) , new BFN(-1,0) );
		[Test] public void _n1e0__0e0 () => _Greater( expected:false , new BFN(-1,0) , new BFN(0,0) );

		[Test] public void _5e3__1e4 () => _Greater( expected:false , new BFN(5,3) , new BFN(1,4) );
		[Test] public void _1e4__5e3 () => _Greater( expected:true , new BFN(1,4) , new BFN(5,3) );

		void _Greater ( bool expected , BFN L , BFN R )
		{
			Debug.Log($"\tTesting: {L.ToStringPrecise()} > {R.ToStringPrecise()}");
			Assert.AreEqual( expected:expected , actual: L > R );
		}
	}

	public class less_than
	{
		[Test] public void _0e0__0e0 () => _Less( expected:false , new BFN(0,0) , new BFN(0,0) );
		[Test] public void _1e0__1e0 () => _Less( expected:false , new BFN(1,0) , new BFN(1,0) );
		[Test] public void _n1e0__n1e0 () => _Less( expected:false , new BFN(-1,0) , new BFN(-1,0) );
		[Test] public void _n1e0__1e0 () => _Less( expected:true , new BFN(-1,0) , new BFN(1,0) );
		[Test] public void _1e0__n1e0 () => _Less( expected:false , new BFN(1,0) , new BFN(-1,0) );
		[Test] public void _n1e0__0e0 () => _Less( expected:true , new BFN(-1,0) , new BFN(0,0) );

		[Test] public void _5e3__1e4 () => _Less( expected:true , new BFN(5,3) , new BFN(1,4) );
		[Test] public void _1e4__5e3 () => _Less( expected:false , new BFN(1,4) , new BFN(5,3) );

		void _Less ( bool expected , BFN L , BFN R )
		{
			Debug.Log($"\tTesting: {L.ToStringPrecise()} < {R.ToStringPrecise()}");
			Assert.AreEqual( expected:expected , actual: L < R );
		}
	}

}
